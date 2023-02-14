using invoice.Data;
using invoice.Models;
using Newtonsoft.Json;

namespace invoice.Services;

class NumberComparer : IComparer<(ulong, int)>
{
    public int Compare((ulong, int) a, (ulong, int) b)
    {
        return a.CompareTo(b);
    }
}

public class ReportService
{
    public SalesReport Report { get; set; } = new SalesReport();
    private readonly ApplicationDbContext _context;

    public ReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SalesReport> PrepareSalesReportAsync()
    {
        try
        {
            Report.BestCompany = await GetBestCompanies();
            SaveBestCompany();
            Report.LastInvoice = await GetLastInvoice();
            Report.InvoicesGenerated = _context.Invoices.Count();
            Report.BestSellingItem = await GetBestSellingItem();

        }
        catch (Exception ex)
        {
            Error.initializeError("ParseBestCompany", "100", ex.Message);
            Error.logError();
        }
        return Report;
    }

    private async Task<string> GetBestSellingItem()
    {
        try
        {
            var topItems = _context.ItemsSold
                                .GroupBy(p => p.Name)
                                .Select(g => new { id = g.Key, count = g.Count() })
                                .ToList();
            string topItem = topItems.Max(t => t.id);
            if (topItem != null) { return topItem; }
        }
        catch (Exception ex)
        {
            Error.initializeError("GetBestSellingItem", "100", ex.Message);
            Error.logError();
        }
        return string.Empty;
    }

    private async Task<Invoice> GetLastInvoice()
    {
        try
        {
            var invoiceList = _context.Invoices.OrderByDescending(a => a.IssuedDate).ToList();
            //foreach (var item in invoiceList)
            //{
            //    var company = await _context.Companies.FindAsync(item.CustomerId);
            //    if (company != null) { Report.Companies.Add(company); };
            //}
            Invoice lastInvoice = invoiceList.FirstOrDefault();
            lastInvoice.Customer = await _context.Companies.FindAsync(lastInvoice.CustomerId);
            if (lastInvoice != null) return lastInvoice;
        }
        catch (Exception ex)
        {
            Error.initializeError("GetLastInvoice", "100", ex.Message);
            Error.logError();
        }
        return null;
    }

    private async Task<Company> GetBestCompanies()
    {
        try
        {
            var topCustomers = _context.Invoices
                .GroupBy(p => p.CustomerId)
                .Select(g => new { id = g.Key, count = g.Count() })
                .ToList();
            topCustomers = topCustomers.GetRange(0, 3);
            ulong bestCustomerId = (ulong)topCustomers.Max(t => t.id);
            Report.BestCompany = await _context.Companies.FindAsync(bestCustomerId);
            var query = from customer in topCustomers
                        orderby customer.count descending
                        select customer;
            topCustomers = query.ToList();
            foreach (var item in topCustomers)
            {
                var Company = await _context.Companies.FindAsync(item.id);
                if (Company != null) Report.Companies.Add(Company);
            }
            if (Report.BestCompany != null) return Report.BestCompany;
            return null;
        }
        catch (Exception ex)
        {
            Error.initializeError("GetBestCompanies", "100", ex.Message);
            Error.logError();
        }
        return null;
    }
    public void SaveBestCompany()
    {
        try
        {
            Report.BestCompanyJson = System.Text.Json.JsonSerializer.Serialize(Report.BestCompany);
        }
        catch (Exception ex)
        {
            Error.initializeError("SaveBestCompany Serialize", "100", ex.Message);
            Error.logError();
        }
    }
    public string ParseBestCompany(string BestCompanyJson)
    {
        try
        {
            Report.BestCompany = JsonConvert.DeserializeObject<Company>(BestCompanyJson);
            if (Report.BestCompany != null) return Report.BestCompanyJson;
        }
        catch (Exception ex)
        {
            Error.initializeError("ParseBestCompany", "100", ex.Message);
            Error.logError();
        }
        return string.Empty;
    }
    public void ParseLastInvoice(string LastInvoiceJson)
    {
        try
        {
            Report.BestCompany = JsonConvert.DeserializeObject<Company>(LastInvoiceJson);
        }
        catch (Exception ex)
        {
            Error.initializeError("ParseLastInvoice", "100", ex.Message);
            Error.logError();
        }
    }

}

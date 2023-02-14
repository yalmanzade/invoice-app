using invoice.Data;
using Microsoft.EntityFrameworkCore;

namespace invoice.Models
{
    public class SeedData
    {
        //public static List<Company> GetCompanies(companyContext companyContext)
        //{
        //    //Make this an Async Method
        //    companyContext.Database.EnsureCreated();
        //    List<Company> companies = companyContext.Company.ToList();
        //    return companies;
        //}
        //public static void Initialize(IServiceProvider serviceProvider)
        //{
        //    var invoiceContext = new invoiceContext(serviceProvider.GetRequiredService<DbContextOptions<invoiceContext>>());
        //    var context = new companyContext(serviceProvider.GetRequiredService<DbContextOptions<companyContext>>());
        //    if (invoiceContext.Invoice.Any() && context.Company.Any())
        //    {
        //        invoiceContext = null;
        //        context = null;
        //        return;
        //    }
        //    using (invoiceContext)
        //    {
        //        if (invoiceContext.Invoice.Any())
        //        {
        //            invoiceContext.SaveChanges();
        //        }
        //        else
        //        {
        //            invoiceContext.Invoice.AddRange(new Invoice
        //            {
        //                DueDate = DateTime.Now
        //            }, new Invoice
        //            {
        //                DueDate = DateTime.Now
        //            });
        //            invoiceContext.SaveChanges();
        //        }
        //    }
        //    using (context)
        //    {
        //        if (context.Company.Any())
        //        {
        //            context.SaveChanges();
        //        }
        //        else
        //        {
        //            context.Company.AddRange(new Company
        //            {
        //                Name = "Yosep Industries",
        //                Address = "143 New Harmony Ln, Evansvile, IN",
        //                Phone = "(555) 555-5555"

        //            });
        //            context.SaveChanges();
        //        }
        //    }
        //}
    }
}

using invoice.Data;
using invoice.Models;
using invoice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace invoice.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            ViewBag.CompanyList = await _context.Companies.ToListAsync();
            return _context.Invoices != null ?
                        View(await _context.Invoices.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Invoices'  is null.");
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(ulong? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public async Task<IActionResult> Create()
        {
            await LoadCompanies();
            await LoadItems();
            await LoadFees();
            return View();
        }

        private async Task LoadItems()
        {
            List<Company> companyList = new();
            ViewBag.ItemList = await _context.Items.ToListAsync();
        }

        private async Task LoadCompanies()
        {
            ViewBag.CompanyList = await _context.Companies.ToListAsync();
        }
        private async Task LoadFees()
        {
            ViewBag.Fees = await _context.Fees.ToListAsync();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int[] quantity, ulong[] itemList,
                                                ulong customerId, string[] fees,
                                                [Bind("CustomerId,DueDate")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                Company company = new()
                {
                    Id = 0,
                    Name = "Amazing Cookies",
                    Phone = "5555555555",
                    Address = "123 Cookie Dr, Evansville IN"
                };
                invoice.Issuer = company;
                invoice.CustomerId = customerId;
                if (fees.Length > 1)
                {
                    for (int i = 1; i < fees.Length; i++)
                    {
                        ulong feeId = (ulong)Convert.ToDouble(fees[i]);
                        var fee = await _context.Fees
                                       .FirstOrDefaultAsync(m => m.Id == feeId);
                        if (fee != null) invoice.Fees.Add(fee);
                    }
                    invoice.SaveFeeList();
                }
                await _context.AddAsync(invoice);
                await _context.SaveChangesAsync();
                var id = invoice.InvoiceId;
                ulong ItemInvoiceId = (ulong)Convert.ToDouble(id);
                if (itemList.Length > 0)
                {
                    invoice.Items = await GetItemsSold(itemList, quantity, ItemInvoiceId);
                    await _context.ItemsSold.AddRangeAsync(invoice.Items);
                    invoice.SaveItemList();
                }
            }
            _context.Update(invoice);
            await _context.SaveChangesAsync();
            await LoadCompanies();
            await LoadItems();
            await LoadFees();
            return RedirectToAction("Index");
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(ulong? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ulong id, [Bind("InvoiceId,CustomerId,UserId,DueDate")] Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.InvoiceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        // POST: Invoices/Download/5
        [HttpPost, ActionName("Download")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadConfirmed(ulong? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }
            Company company = new()
            {
                Id = 0,
                Name = "Amazing Cookies",
                Phone = "5555555555",
                Address = "123 Cookie Dr, Evansville IN"
            };
            invoice.Issuer = company;
            invoice.Customer = await _context.Companies
                               .FirstOrDefaultAsync(m => m.Id == invoice.CustomerId);
            InvoiceService invoiceService = new(invoice);
            await invoice.ParseItemList();
            await invoice.ParseFeeList();
            invoiceService.GenerateInvoice();
            byte[] filebytes = System.IO.File.ReadAllBytes(invoiceService.GetFilePath(invoice.InvoiceId));
            string contentType = "application/pdf";
            return File(filebytes, contentType);
            //return View(invoice);
        }
        // GET: Invoices/Download/5
        public async Task<IActionResult> Download(ulong? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }
        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(ulong? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ulong id)
        {
            if (_context.Invoices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Invoices'  is null.");
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                await _context.ItemsSold.Where(x => x.InvoiceId.Equals(invoice.InvoiceId)).ExecuteDeleteAsync();
                _context.Invoices.Remove(invoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(ulong id)
        {
            return (_context.Invoices?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
        }
        private async Task<List<ItemSold>> ListItems(int quantity0, int quantity1, int quantity2,
                              ulong itemList0, ulong itemList1, ulong itemList2)
        {
            List<ItemSold> itemsSold = new();
            if (quantity0 > 0)
            {
                var item = await _context.Items
                                .FirstOrDefaultAsync(m => m.Id == itemList0);
                itemsSold.Add(new ItemSold
                {
                    Name = item.Name,
                    Amount = item.Price,
                    Quantity = quantity0,
                }); ;
            }
            else if (quantity1 > 0)
            {
                var item = await _context.Items
                            .FirstOrDefaultAsync(m => m.Id == itemList1);
                itemsSold.Add(new ItemSold
                {
                    Name = item.Name,
                    Amount = item.Price,
                    Quantity = quantity1,
                }); ;
            }
            else if (quantity2 > 0)
            {
                var item = await _context.Items
                           .FirstOrDefaultAsync(m => m.Id == itemList2);
                itemsSold.Add(new ItemSold
                {
                    Name = item.Name,
                    Amount = item.Price,
                    Quantity = quantity2,
                }); ;
            }
            return itemsSold;
        }
        private async Task<List<ItemSold>> GetItemsSold(ulong[] items, int[] quantity, ulong invoiceId)
        {
            List<ItemSold> itemsSold = new();
            if (items.Length > 0)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    var item = await _context.Items
                                      .FirstOrDefaultAsync(m => m.Id == items[i]);
                    if (item != null)
                    {
                        ItemSold itemSold = new()
                        {
                            Name = item.Name,
                            Amount = item.Price,
                            Quantity = quantity[i],
                            InvoiceId = invoiceId
                        };
                        itemsSold.Add(itemSold);
                    };
                }
            }
            return itemsSold;
        }
    }
}

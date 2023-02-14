using invoice.Models;
using invoice.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace invoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //ItemSold item = new()
            //{
            //    Name= "Home",
            //    Amount= 1.00M,
            //    Quantity = 2
            //};
            //List<ItemSold> items = new();
            //items.Add(item);
            //Invoice invoice = new()
            //{
            //    InvoiceId = 666,
            //    Items = items
            //};
            //InvoiceService invoiceService = new(invoice);
            //invoiceService.GenerateInvoice();
            //byte[] filebytes = System.IO.File.ReadAllBytes(invoiceService.GetFilePath(invoice.InvoiceId));
            //string contentType = "application/pdf";
            //return File(filebytes, contentType);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
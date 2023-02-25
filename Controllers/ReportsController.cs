using invoice.Data;
using invoice.Models;
using invoice.Services;
using Microsoft.AspNetCore.Mvc;

namespace invoice.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ReportService reportService = new(_context);
            SalesReport salesReport = new();
            salesReport = await reportService.PrepareSalesReportAsync();
            return View(salesReport);
        }
    }
}

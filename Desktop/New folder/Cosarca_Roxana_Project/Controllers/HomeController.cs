using Cosarca_Roxana_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficeModel.Data;
using Cosarca_Roxana_Project.Models.OfficeViewModels;

namespace Cosarca_Roxana_Project.Controllers
{
    public class HomeController : Controller
    {

        private readonly OfficeContext _context;
        public HomeController(OfficeContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Statistics()
        {
            IQueryable<AppointmentGroup> data =
            from appointment in _context.Appointments
            group appointment by appointment.AppointmentDate into dateGroup
            select new AppointmentGroup()
            {
                AppointmentDate = dateGroup.Key,
                ServiceCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Index()
        {
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
        public IActionResult Chat()
        {
            return View();
        }
    }
}

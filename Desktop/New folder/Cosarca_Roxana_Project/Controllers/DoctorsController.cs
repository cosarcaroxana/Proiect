using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cosarca_Roxana_Project.Data;
using OfficeModel.Models;
using Cosarca_Roxana_Project.Models.OfficeViewModels;
using OfficeModel.Data;

namespace Cosarca_Roxana_Project.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly OfficeContext _context;

        public object DoctorServices { get; private set; }
        

        public DoctorsController(OfficeContext context)
        {
            _context = context;
        }

        // GET: Doctors
        public async Task<IActionResult> Index(int? id, int? serviceID)
        {
            var viewModel = new DoctorIndexData();
            viewModel.Doctors = await _context.Doctors
            .Include(i => i.DoctorServices)
            .ThenInclude(i => i.Service)
            .ThenInclude(i => i.Appointments)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.DoctorName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["Doctorzid"] = id.Value;
                Doctor doctor = viewModel.Doctors.Where(
                i => i.ID == id.Value).Single();
                viewModel.Services = doctor.DoctorServices.Select(s => s.Service);
            }
            if (serviceID != null)
            {
                ViewData["ServiceID"] = serviceID.Value;
                viewModel.Appointments = viewModel.Services.Where(
                x => x.ID == serviceID).Single().Appointments;
            }
            return View(viewModel);
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DoctorName,Specializare")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var doctor = await _context.Doctors
            .Include(i => i.DoctorServices).ThenInclude(i => i.Service)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (doctor == null)
            {
                return NotFound();
            }
            PopulateDoctoredServiceData(doctor);
            return View(doctor);

        }
        private void PopulateDoctoredServiceData(Doctor doctor)
        {
            var allServices = _context.Services;
            var doctorServices = new HashSet<int>(doctor.DoctorServices.Select(c => c.ServiceID));
            var viewModel = new List<DoctoredServiceData>();
            foreach (var service in allServices)
            {
                viewModel.Add(new DoctoredServiceData
                {
                    ServiceID = service.ID,
                    Title = service.Title,
                    IsDoctored = doctorServices.Contains(service.ID)
                });
            }
            ViewData["Services"] = viewModel;
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedServices)
        {
            if (id == null)
            {
                return NotFound();
            }
            var doctorToUpdate = await _context.Doctors
            .Include(i => i.DoctorServices)
            .ThenInclude(i => i.Service)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Doctor>(
            doctorToUpdate,
            "",
            i => i.DoctorName, i => i.Specializare))
            {
                UpdateDoctoredServices(selectedServices, doctorToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateDoctoredServices(selectedServices, doctorToUpdate);
            PopulateDoctoredServiceData(doctorToUpdate);
            return View(doctorToUpdate);
        }
        private void UpdateDoctoredServices(string[] selectedServices, Doctor doctorToUpdate)
        {
            if (selectedServices == null)
            {
                doctorToUpdate.DoctorServices = new List<DoctorService>();
                return;
            }
            var selectedServicesHS = new HashSet<string>(selectedServices);
            var doctorServices = new HashSet<int>
            (doctorToUpdate.DoctorServices.Select(c => c.Service.ID));
            foreach (var service in _context.Services)
            {
                if (selectedServicesHS.Contains(service.ID.ToString()))
                {
                    if (!doctorServices.Contains(service.ID))
                    {
                        doctorToUpdate.DoctorServices.Add(new DoctorService
                        {
                            DoctorID = doctorToUpdate.ID,
                            ServiceID = service.ID
                        });
                    }
                }
                else
                {
              
                    if (doctorServices.Contains(service.ID))
                    {
                        DoctorService serviceToRemove = doctorToUpdate.DoctorServices.FirstOrDefault(i
                       => i.ServiceID == service.ID);
                        _context.Remove(serviceToRemove);
                    }
                }
            }
        }
        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.ID == id);
        }
    }

    
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResPAL_MVC.Data;
using ResPAL_MVC.Models.ResPALModels;

namespace ResPAL_MVC.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ResPALContext _context;

        public PatientsController(ResPALContext context)
        {
            _context = context;    
        }

        // GET: Patients
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["RESIDSortParm"] = sortOrder == "resid" ? "resid_desc" : "RESID";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var patients = from p in _context.Patients
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(p => p.LastName.Contains(searchString)
                                       || p.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    patients = patients.OrderByDescending(s => s.LastName);
                    break;
                case "resid":
                    patients = patients.OrderBy(p => p.RESID);
                    break;
                case "resid_desc":
                    patients = patients.OrderByDescending(p => p.RESID);
                    break;
                default:
                   patients = patients.OrderBy(p => p.LastName);
                    break;
            }
            int pageSize = 15;
            return View(await PaginatedList<Patient>.CreateAsync(patients.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var patient = await _context.Patients
                .Include(p=>p.Diseases)
               .Include(p=>p.Azoles)
                .SingleOrDefaultAsync(m => m.PatientID == id);
            if (patient == null)

            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientID,RESID,RM2Number,LastName,FirstName,Gender,DOB,PatientStatus")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.SingleOrDefaultAsync(m => m.PatientID == id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientID,RESID,RM2Number,LastName,FirstName,Gender,DOB,PatientStatus")] Patient patient)
        {
            if (id != patient.PatientID)
            {
                return NotFound();
            }


            var patientToUpdate = await _context.Patients.SingleOrDefaultAsync(p => p.PatientID == id);
            if (await TryUpdateModelAsync<Patient>(
                patientToUpdate,
                "",
                p => p.RESID, p => p.FirstName, p => p.LastName, p => p.Gender, p => p.DOB, p=>p.PatientStatus))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(patientToUpdate);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .SingleOrDefaultAsync(m => m.PatientID == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patients.SingleOrDefaultAsync(m => m.PatientID == id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.PatientID == id);
        }
    }
}

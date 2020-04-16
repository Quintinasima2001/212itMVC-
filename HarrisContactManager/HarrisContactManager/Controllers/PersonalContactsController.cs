using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HarrisContactManager.Data;
using HarrisContactManager.Models;

namespace HarrisContactManager.Controllers
{
    public class PersonalContactsController : Controller
    {
        private readonly HarrisDbContext _context;

        public PersonalContactsController(HarrisDbContext context)
        {
            _context = context;
        }

        // GET: PersonalContacts
        public async Task<IActionResult> Index()
        {
            return View(await _context.PersonalContacts.ToListAsync());
        }

        // GET: PersonalContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalContacts = await _context.PersonalContacts
                .FirstOrDefaultAsync(m => m.PersonalContactsID == id);
            if (personalContacts == null)
            {
                return NotFound();
            }

            return View(personalContacts);
        }

        // GET: PersonalContacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonalContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonalContactsID,PersonalFname,PersonalLname,PersonalEmial,PersonalTel,PersoanlAddress1,PersoanlAddress2,PersonalCity,PersoanlPostCode")] PersonalContacts personalContacts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personalContacts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personalContacts);
        }

        // GET: PersonalContacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalContacts = await _context.PersonalContacts.FindAsync(id);
            if (personalContacts == null)
            {
                return NotFound();
            }
            return View(personalContacts);
        }

        // POST: PersonalContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonalContactsID,PersonalFname,PersonalLname,PersonalEmial,PersonalTel,PersoanlAddress1,PersoanlAddress2,PersonalCity,PersoanlPostCode")] PersonalContacts personalContacts)
        {
            if (id != personalContacts.PersonalContactsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personalContacts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalContactsExists(personalContacts.PersonalContactsID))
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
            return View(personalContacts);
        }

        // GET: PersonalContacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalContacts = await _context.PersonalContacts
                .FirstOrDefaultAsync(m => m.PersonalContactsID == id);
            if (personalContacts == null)
            {
                return NotFound();
            }

            return View(personalContacts);
        }

        // POST: PersonalContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personalContacts = await _context.PersonalContacts.FindAsync(id);
            _context.PersonalContacts.Remove(personalContacts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalContactsExists(int id)
        {
            return _context.PersonalContacts.Any(e => e.PersonalContactsID == id);
        }
    }
}

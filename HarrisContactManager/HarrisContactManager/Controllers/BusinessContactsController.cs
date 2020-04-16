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
    public class BusinessContactsController : Controller
    {
        private readonly HarrisDbContext _context;

        public BusinessContactsController(HarrisDbContext context)
        {
            _context = context;
        }

        // GET: BusinessContacts
        public async Task<IActionResult> Index()
        {
            return View(await _context.BusinessContacts.ToListAsync());
        }

        // GET: BusinessContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessContacts = await _context.BusinessContacts
                .FirstOrDefaultAsync(m => m.BusinessContactsID == id);
            if (businessContacts == null)
            {
                return NotFound();
            }

            return View(businessContacts);
        }

        // GET: BusinessContacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BusinessContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessContactsID,BusinessFname,BusinessLname,BusinessEmial,BusinessTel,BusinessAddress1,BusinessAddress2,BusinessCity,BusinessPostCode")] BusinessContacts businessContacts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businessContacts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(businessContacts);
        }

        // GET: BusinessContacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessContacts = await _context.BusinessContacts.FindAsync(id);
            if (businessContacts == null)
            {
                return NotFound();
            }
            return View(businessContacts);
        }

        // POST: BusinessContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessContactsID,BusinessFname,BusinessLname,BusinessEmial,BusinessTel,BusinessAddress1,BusinessAddress2,BusinessCity,BusinessPostCode")] BusinessContacts businessContacts)
        {
            if (id != businessContacts.BusinessContactsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessContacts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessContactsExists(businessContacts.BusinessContactsID))
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
            return View(businessContacts);
        }

        // GET: BusinessContacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessContacts = await _context.BusinessContacts
                .FirstOrDefaultAsync(m => m.BusinessContactsID == id);
            if (businessContacts == null)
            {
                return NotFound();
            }

            return View(businessContacts);
        }

        // POST: BusinessContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var businessContacts = await _context.BusinessContacts.FindAsync(id);
            _context.BusinessContacts.Remove(businessContacts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessContactsExists(int id)
        {
            return _context.BusinessContacts.Any(e => e.BusinessContactsID == id);
        }
    }
}

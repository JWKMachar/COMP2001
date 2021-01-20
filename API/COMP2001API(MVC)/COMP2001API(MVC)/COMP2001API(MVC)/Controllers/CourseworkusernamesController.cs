using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using COMP2001API_MVC_.Models;

namespace COMP2001API_MVC_.Controllers
{
    public class CourseworkusernamesController : Controller
    {
        private readonly COMP2001_JMacharContext _context;

        public CourseworkusernamesController(COMP2001_JMacharContext context)
        {
            _context = context;
        }

        // GET: Courseworkusernames
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courseworkusernames.ToListAsync());
        }

        // GET: Courseworkusernames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseworkusername = await _context.Courseworkusernames
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (courseworkusername == null)
            {
                return NotFound();
            }

            return View(courseworkusername);
        }

        // GET: Courseworkusernames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courseworkusernames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,EmailAddress,CurrentPassword")] Courseworkusername courseworkusername)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseworkusername);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseworkusername);
        }

        // GET: Courseworkusernames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseworkusername = await _context.Courseworkusernames.FindAsync(id);
            if (courseworkusername == null)
            {
                return NotFound();
            }
            return View(courseworkusername);
        }

        // POST: Courseworkusernames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,FirstName,LastName,EmailAddress,CurrentPassword")] Courseworkusername courseworkusername)
        {
            if (id != courseworkusername.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseworkusername);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseworkusernameExists(courseworkusername.UserId))
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
            return View(courseworkusername);
        }

        // GET: Courseworkusernames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseworkusername = await _context.Courseworkusernames
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (courseworkusername == null)
            {
                return NotFound();
            }

            return View(courseworkusername);
        }

        // POST: Courseworkusernames/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var courseworkusername = await _context.Courseworkusernames.FindAsync(id);
        //    _context.Courseworkusernames.Remove(courseworkusername);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool CourseworkusernameExists(int id)
        {
            return _context.Courseworkusernames.Any(e => e.UserId == id);
        }

        [HttpPost, ActionName("DeleteUser")]
        public IActionResult DeleteUser(DeleteUser deleteUser)
        {
            var rowsaffected = _context.Database.ExecuteSqlRaw("EXEC DeleteUser @userID", 
                new SqlParameter("@userID", deleteUser.UserID.ToString()));

            ViewBag.Success = rowsaffected;

            return RedirectToAction(nameof(Index));
        }
    }
}

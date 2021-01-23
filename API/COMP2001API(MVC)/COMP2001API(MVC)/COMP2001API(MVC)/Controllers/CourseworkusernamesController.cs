using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using COMP2001API_MVC_.Models;
using System.Text;

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

        //Calls Stored Procedure to register a given user
        [HttpPost, ActionName("Register")]
        public IActionResult Register(Register register)
        {
            string  salt = SaltHash(5);
            var rowsaffected = _context.Database.ExecuteSqlRaw("EXEC Register @firstName, @lastName, @emailAddress, @currentPassword",
                new SqlParameter("@firstName", register.firstName.ToString()),
                new SqlParameter("@lastName", register.lastName.ToString()),
                new SqlParameter("@emailAddress", register.emailAddress.ToString()),
                new SqlParameter("@currentPassword", HashGenerator(register.currentPassword.ToString(), salt)));

            ViewBag.Success = rowsaffected;

            return RedirectToAction(nameof(Index));
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


        //Calls Stored Procedure to update a given user
        [HttpPost, ActionName("UpdateUser")]
        public IActionResult UpdateUser(UpdateUser updateUser)
        {

            string salt = SaltHash(5);
            var rowsaffected = _context.Database.ExecuteSqlRaw("EXEC UpdateUser @FirstName, @LastName, @EmailAddress, @CurrentPassword, @UserID",
                new SqlParameter("@FirstName", updateUser.FirstName.ToString()),
                new SqlParameter("@LastName", updateUser.LastName.ToString()),
                new SqlParameter("@EmailAddress", updateUser.EmailAddress.ToString()),
                new SqlParameter("@CurrentPassword", HashGenerator(updateUser.CurrentPassword.ToString(), salt)),
                new SqlParameter("@UserID", updateUser.UserID.ToString()));

            ViewBag.Success = rowsaffected;

            return RedirectToAction(nameof(Index));
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

        private bool CourseworkusernameExists(int id)
        {
            return _context.Courseworkusernames.Any(e => e.UserId == id);
        }

        //Calls Stored Procedure to delete a given user
        [HttpPost, ActionName("DeleteUser")]
        public IActionResult DeleteUser(DeleteUser deleteUser)
        {
            var rowsaffected = _context.Database.ExecuteSqlRaw("EXEC DeleteUser @userID",
                new SqlParameter("@userID", deleteUser.UserID.ToString()));

            ViewBag.Success = rowsaffected;

            return RedirectToAction(nameof(Index));
        }
        // GET: Courseworkusernames/Validate/5
        public async Task<IActionResult> Validate(int? id)
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
        //Calls Stored Procedure to validate a given user
        [HttpPost, ActionName("ValidateUser")]
        public IActionResult ValidateUser(ValidateUser validateUser)
        {
            var rowsaffected = _context.Database.ExecuteSqlRaw("EXEC ValidateUser @EmailAddress, @CurrentPassword",
                new SqlParameter("@EmailAddress", validateUser.EmailAddress.ToString()),
                new SqlParameter("@CurrentPassword", validateUser.CurrentPassword.ToString()));


            ViewBag.Success = rowsaffected;

            return RedirectToAction(nameof(Index));
        }
        
        //Converts Hex to a String
        public static string HexConverter(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        //Creates the Salt Hash
        public string SaltHash(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new Byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        //Takes Password and Salt Hash and returns the Hashed finished hash value
        public string HashGenerator(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return HexConverter(hash);
        }
    }
}

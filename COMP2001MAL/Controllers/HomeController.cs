using COMP2001MAL.Models;
using COMP2001MAL.ViewModels;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.Text;

namespace COMP2001MAL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Comp2001malMsahifassanibinmohamadContext _context;

        public HomeController(ILogger<HomeController> logger, Comp2001malMsahifassanibinmohamadContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM user)
        {
            // Check if the username already exists
            var existingUsername = _context.Users.Any(u => u.Username == user.Username);
            if (existingUsername)
            {
                ModelState.AddModelError("Username", "Username already exists");
            }

            // Check if the email already exists
            var existingEmail = _context.Users.Any(u => u.Email == user.Email);
            if (existingEmail)
            {
                ModelState.AddModelError("Email", "Email already exists");
            }

            // If there are model state errors, return to the view with errors
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            // Execute the stored procedure
            _context.Database.ExecuteSqlRaw("EXEC dbo.InsertUserProfile @Username, @Password, @Email, @ArchiveStatus",
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@ArchiveStatus", user.ArchiveStatus));

            // Fetch the inserted user
            var insertedUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);

            if (insertedUser == null)
            {
                ModelState.AddModelError(string.Empty, "Sign-in failed");
                return View(user);
            }

            HttpContext.Session.SetString("uname", user.Username!);
            return RedirectToAction("Profile", "Home");
        }



        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginRequest)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u =>
                u.Username == loginRequest.Username && u.Password == loginRequest.Password && u.ArchiveStatus == "Active");

            if (user == null)
            {
                ModelState.AddModelError("Username", "Wrong Username");
                ModelState.AddModelError("Password", "Wrong Password");
                return View(user);
            }

            HttpContext.Session.SetString("uname", loginRequest.Username!);
            HttpContext.Session.SetInt32("userid", user.UserId);
            return RedirectToAction("Profile", "Home");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile()
        {
            string username = HttpContext.Session.GetString("uname")!;

            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            return View(user);
        }

        public IActionResult DeleteProfile()
        {
            string username = HttpContext.Session.GetString("uname")!;

            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            _context.Database.ExecuteSqlRaw("EXEC dbo.DeleteUserProfile @UserID", new SqlParameter("@UserID", user.UserId));

            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            string username = HttpContext.Session.GetString("uname")!;

            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProfile(User updatedUser)
        {
            string username = HttpContext.Session.GetString("uname")!;

            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            // Update the user's information using the stored procedure
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", user.UserId),
                new SqlParameter("@FullName", (object)updatedUser.FullName ?? DBNull.Value),
                new SqlParameter("@DateOfBirth", (object)updatedUser.DateOfBirth ?? DBNull.Value),
                new SqlParameter("@ProfileImage", (object)updatedUser.ProfileImage ?? DBNull.Value),
                new SqlParameter("@Location", (object)updatedUser.Location ?? DBNull.Value),
                new SqlParameter("@ArchiveStatus", user.ArchiveStatus)
            };

            _context.Database.ExecuteSqlRaw("EXEC dbo.UpdateUserProfile @UserID, @FullName, @DateOfBirth, @ProfileImage, @Location, @ArchiveStatus", parameters);

            return RedirectToAction("Profile", "Home");
        }
    }
}

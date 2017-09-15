using DocumentStorage.Models;
using DocumentStorage.Models.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DocumentStorage.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController (AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Login user)
        {
            if (ModelState.IsValid)
            {
                if (user.IsValid(user.Email, user.Password, _context))
                {
                    return RedirectToAction("Dashboard", new { UserID = 1 });
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect, please try again!");
                }
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(Models.CreateAccount account)
        {
            if (ModelState.IsValid)
            {
                if (account.IsValid(account.Email, _context))
                {
                    User user = new User
                    {
                        FirstName = account.FirstName,
                        LastName = account.LastName,
                        Email = account.Email,
                        PasswordHash = account.SecurePassword(account.Password)
                    };

                    _context.Users.Add(user);

                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "An account already exist with that email!");
                }
            }
            return View(account);
        }

        public IActionResult AddFiles()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Dashboard(int userId)
        {
            Dashboard dashboard = new Dashboard
            {
                UserID = userId
            };

            List<File> files= dashboard.GatherFiles(userId, _context);
            return View(files);
        }

        //public ActionResult DashSort(string sortOrder)
        //{
        //    /*https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application*/

        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewBag.DateSortParm = sortOrder == "Created" ? "created_desc" : "Created";
        //    ViewBag.DateSortParm = sortOrder == "Downloads" ? "downloads_desc" : "Downloads";

        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            break;
        //        case "downloads_desc":
        //            break;
        //        case "created_desc":
        //            break;
        //        default:
        //            break;
        //    }

        //    return View();
        //}

        public IActionResult Error()
        {
            return View();
        }
    }
}

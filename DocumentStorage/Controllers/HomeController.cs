using DocumentStorage.Models;
using DocumentStorage.Models.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace DocumentStorage.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _loginManager;

        public HomeController (AppDbContext context, UserManager<User> secMgr, SignInManager<User> loginManager)
        {

            _context = context;
            _userManager = secMgr;
            _loginManager = loginManager;


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
        public async Task<IActionResult> Login(Models.Login user)
        {
            if (ModelState.IsValid)
            {
                var userx = await _userManager.FindByEmailAsync(user.Email.ToUpper());
                if (userx == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login");
                    return View();
                }
                var passwordSignInResult = await _loginManager.PasswordSignInAsync(userx, user.Password, isPersistent: false, lockoutOnFailure: false);
                if (!passwordSignInResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login");
                    return View();
                }

                else 
                {

                    return RedirectToAction("Dashboard", new { UserID = 1 });
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
        public async Task<IActionResult> CreateAccount(Models.CreateAccount account)
        {
            if (ModelState.IsValid)
            {
                if (account.IsValid(account.Email, _context))
                {
                    User user = new User
                    {
                        FirstName = account.FirstName,
                        LastName = account.LastName,
                        Email = account.Email.ToLower(),
                        PasswordHash = account.Password,
                        UserName = account.Email.ToLower(),
                        SecurityStamp = Guid.NewGuid().ToString(), //THIS IS WHAT I NEEDED,
                    

                    };

                    var result = await _userManager.CreateAsync(user, account.Password);

                    if (result.Succeeded)
                    {

                        await _loginManager.SignInAsync(user, isPersistent: false);


                        return RedirectToAction("Login", "Home");

                    }

                }

                else
                {
                    ModelState.AddModelError("", "An account already exist with that email!");
                }
            }
            return View(account);
        }



        public IActionResult AddFile()
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

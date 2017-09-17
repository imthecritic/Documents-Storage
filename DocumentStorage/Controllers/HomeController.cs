using DocumentStorage.Models;
using DocumentStorage.Models.DB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace DocumentStorage.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _loginManager;
        private IHostingEnvironment _environment;


        public HomeController (AppDbContext context, UserManager<User> secMgr, SignInManager<User> loginManager, IHostingEnvironment environment)
        {

            _context = context;
            _userManager = secMgr;
            _loginManager = loginManager;
            _environment = environment;


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
                    ModelState.AddModelError(string.Empty, "Incorrect email please try again!");
                    return View();
                }
                var passwordSignInResult = await _loginManager.PasswordSignInAsync(userx, user.Password, isPersistent: false, lockoutOnFailure: false);
                if (!passwordSignInResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password please try again!");
                    return View();
                }

                else 
                {
                    return RedirectToAction("Dashboard", "Home");
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
        public async Task<IActionResult> CreateAccount(Models.AccountInfo account)
        {
            if (ModelState.IsValid)
            {
                if (account.AccountNotFound(account.Email, _context))
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


                        return RedirectToAction("Index", "Home");

                    }

                }

                else
                {
                    ModelState.AddModelError("", "An account already exist with that email!");
                }
            }
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(ICollection<IFormFile> files)
        {
            string email = User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(email.ToUpper());
            string userdir = "uploads\\" + user.Id.ToString();
            var uploads = Path.Combine(_environment.WebRootPath, userdir);
            if (!Directory.Exists(uploads))
            { // if it doesn't exist, create

                System.IO.Directory.CreateDirectory(uploads);
            }
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                         await file.CopyToAsync(fileStream);
                        Models.DB.File newFile = new Models.DB.File { FileName = file.FileName, Created = DateTime.Now, Active = true, Downloads = 0, FilePath = fileStream.Name };
                        _context.Files.Add(newFile);
                        _context.SaveChanges();
                        int fileId = newFile.FileID;
                        _context.UsersFiles.Add(new UserFile { UserID = user.Id, FileID = fileId });
                        _context.SaveChanges();
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddFile()
        {
            return View();
        }

        public async Task<IActionResult> DownloadFile(string filename)
        {
            string email = User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(email.ToUpper());
            string userdir = "uploads\\" + user.Id.ToString();
            var uploads = Path.Combine(_environment.WebRootPath, userdir);

            var path = Path.Combine(
                            uploads, filename);

            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(filename, out contentType);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            //return new FileStreamResult(memory, contentType);
            return File(memory, contentType);
                
           }

        public IActionResult DeleteFile(int fileID)
        {
            var file = new Models.DB.File() { FileID =fileID, Active = false};

            _context.Files.Attach(file);
            _context.Entry(file).Property(x => x.Active).IsModified = true;
            _context.SaveChanges();

            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {

            string email = User.Identity.Name;
            User user = await _userManager.FindByEmailAsync(email.ToUpper()); 
            AccountInfo accountInfo = new AccountInfo
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return View(accountInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountInfo model)
        {
            string email = User.Identity.Name;
            User user = await _userManager.FindByEmailAsync(email.ToUpper());
            if (!ModelState.IsValid)
            {
                // there were validation errors => redisplay the view
                return View(model);
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            //user.PasswordHash = AppUserManager.passwordHasher.HashPassword(user, model.Password); //does not work

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Account not updated, try again!");
            }

                return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _loginManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Dashboard(int userId)
        {

            string email = User.Identity.Name;
            var userx = await _userManager.FindByEmailAsync(email.ToUpper());

            userId = userx.Id;

            Dashboard dashboard = new Dashboard{ UserID = userId};

            List<Models.DB.File> files= dashboard.GatherFiles(userId, _context);
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

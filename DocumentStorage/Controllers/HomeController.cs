using DocumentStorage.Models.DB;
using Microsoft.AspNetCore.Mvc;


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
                    return RedirectToAction("CreateAccount", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
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
                        PasswordHash = account.Password
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

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

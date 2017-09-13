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

        public IActionResult CreateAccount()
        {
            return View();
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

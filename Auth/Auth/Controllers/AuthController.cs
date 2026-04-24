using Auth.Auth.DTOs;
using Auth.EF;
using Auth.EF.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Auth.Controllers
{
    public class AuthController : Controller
    {
        AuthBSp26Context db;
        public AuthController(AuthBSp26Context db)
        {
            this.db = db;
        }

        [Logged]
        public IActionResult Dashboard()
        {
            ViewBag.Uname = HttpContext.Session.GetString("Uname");
            ViewBag.Type = HttpContext.Session.GetInt32("UType");
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Uname, string Pass)
        {
            var u = (from user in db.Users where user.Username.Equals(Uname) && user.Password.Equals(Pass) select user).SingleOrDefault();
            if(u != null)
            {
                HttpContext.Session.SetString("Uname", u.Username);
                HttpContext.Session.SetInt32("Pass", u.Type);

                if(u.Type == 1)
                {
                    return RedirectToAction("Dashboard", "Admin");
                }

                if(u.Type == 2)
                {
                    return RedirectToAction("Dashboard", "Teacher");
                }

                if(u.Type == 3)
                {
                    return RedirectToAction("Dashboard", "Student");
                }

                return RedirectToAction("Dashboard");

            }
            TempData["Class"] = "danger";
            TempData["Msg"] = "Invalid Username and Password";
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View(new RegDto{ });
        }

        [HttpPost]
        public IActionResult Registration(RegDto obj)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Name = obj.Name,
                    Email = obj.Email,
                    Username = obj.Username,
                    Password = obj.Password,
                    Type = 2
                };

                db.Users.Add(user);
                db.SaveChanges();
                TempData["Class"] = "success";
                TempData["Msg"] = "Registration Successfull";
                return RedirectToAction("Login");
            }

            return View(obj);
        }
    }
}

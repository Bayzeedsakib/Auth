using Auth.Auth.DTOs;
using Auth.EF;
using Auth.EF.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Auth.Controllers
{
    public class AdminController : Controller
    {
        AuthBSp26Context db;
        public AdminController(AuthBSp26Context db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var types = db.UserTypes.ToList();
            ViewBag.Types = types;
            return View(new RegDto { });
        }

        [HttpPost]
        public IActionResult Create(RegDto obj)
        {
            Console.WriteLine(obj);
            if (ModelState.IsValid)
            {
                var u = new User()
                {
                    Name = obj.Name,
                    Email = obj.Email,
                    Password = obj.Password,
                    Username = obj.Username,
                    Type = obj.Type,
                };
                db.Users.Add(u);
                db.SaveChanges();
            }

            var types = db.UserTypes.ToList();
            ViewBag.Types = types;
            return View(obj);
        }
    }
}

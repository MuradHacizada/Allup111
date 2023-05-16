using Allup111.DAL;
using Allup111.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Allup111.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {

            return View(await _db.Categories.Where(x=>x.isMain).ToListAsync());
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
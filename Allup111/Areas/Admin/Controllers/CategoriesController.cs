using Allup111.DAL;
using Allup111.Helper;
using Allup111.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allup111.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public CategoriesController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _db.Categories.Include(x=>x.Children).Include(x=>x.Parent).OrderByDescending(x=>x.isMain).ToListAsync();
            return View(categories);
        }
        public async Task<IActionResult> Create()
        {
            
            ViewBag.MainCategories=await _db.Categories.Where(x=>x.isMain).ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category,int? mainCatId)
        {

            ViewBag.MainCategories = await _db.Categories.Where(x => x.isMain).ToListAsync();
            if (category.isMain)
            {
                bool isExist=await _db.Categories.AnyAsync(x=>x.Name==category.Name);
                if(isExist) 
                {
                    ModelState.AddModelError("Name", "This Category is exist");
                    return View();
                }
                #region Save Image
                if (category.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Image can not be null");
                    return View();
                }
                if (!category.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Pls select image type");
                    return View();
                }
                if (category.Photo.IsOlderMb())
                {
                    ModelState.AddModelError("Photo", "max 1 mb");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "assets", "images");
                category.Image = await category.Photo.SaveFileAsync(folder);
                #endregion
            }
            else
            {
                category.ParentId = mainCatId;
            }
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

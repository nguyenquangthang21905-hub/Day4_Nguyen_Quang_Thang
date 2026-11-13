using Day4.Data;
using Day4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day4.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View(SeedData.Categories);
        }

        public IActionResult Details(int id)
        {
            var category = FindCategory(id);
            return category is null ? NotFound() : View(category);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoryName")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            category.CategoryId = SeedData.Categories.Count == 0
                ? 1
                : SeedData.Categories.Max(existingCategory => existingCategory.CategoryId) + 1;
            SeedData.Categories.Add(category);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var category = FindCategory(id);
            return category is null ? NotFound() : View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CategoryName")] Category category)
        {
            var existingCategory = FindCategory(id);
            if (existingCategory is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                category.CategoryId = id;
                return View(category);
            }

            existingCategory.CategoryName = category.CategoryName;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var category = FindCategory(id);
            if (category is null)
            {
                return NotFound();
            }

            ViewBag.ProductCount = SeedData.Products.Count(product => product.CategoryId == id);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = FindCategory(id);
            if (category is null)
            {
                return NotFound();
            }

            if (SeedData.Products.Any(product => product.CategoryId == id))
            {
                ModelState.AddModelError(string.Empty, "Không thể xóa danh mục đang có sản phẩm. Hãy chuyển hoặc xóa các sản phẩm trước.");
                ViewBag.ProductCount = SeedData.Products.Count(product => product.CategoryId == id);
                return View("Delete", category);
            }

            SeedData.Categories.Remove(category);
            return RedirectToAction(nameof(Index));
        }

        private static Category? FindCategory(int id) =>
            SeedData.Categories.FirstOrDefault(category => category.CategoryId == id);
    }
}

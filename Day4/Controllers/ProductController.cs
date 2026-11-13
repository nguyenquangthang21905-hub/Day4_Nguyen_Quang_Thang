using Day4.Data;
using Day4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day4.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var products = from product in SeedData.Products
                           join category in SeedData.Categories on product.CategoryId equals category.CategoryId
                           select new ProductViewModel
                           {
                               ProductId = product.ProductId,
                               ProductName = product.ProductName,
                               ProductPrice = product.Price,
                               CategoryName = category.CategoryName
                           };

            return View(products.ToList());
        }

        public IActionResult Details(int id)
        {
            var product = FindProduct(id);
            if (product is null)
            {
                return NotFound();
            }

            ViewBag.CategoryName = FindCategoryName(product.CategoryId);
            return View(product);
        }

        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductName,Price,CategoryId")] Product product)
        {
            ValidateCategory(product.CategoryId);
            if (!ModelState.IsValid)
            {
                LoadCategories(product.CategoryId);
                return View(product);
            }

            product.ProductId = SeedData.Products.Count == 0
                ? 1
                : SeedData.Products.Max(existingProduct => existingProduct.ProductId) + 1;
            SeedData.Products.Add(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var product = FindProduct(id);
            if (product is null)
            {
                return NotFound();
            }

            LoadCategories(product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductName,Price,CategoryId")] Product product)
        {
            var existingProduct = FindProduct(id);
            if (existingProduct is null)
            {
                return NotFound();
            }

            ValidateCategory(product.CategoryId);
            if (!ModelState.IsValid)
            {
                product.ProductId = id;
                LoadCategories(product.CategoryId);
                return View(product);
            }

            existingProduct.ProductName = product.ProductName;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var product = FindProduct(id);
            if (product is null)
            {
                return NotFound();
            }

            ViewBag.CategoryName = FindCategoryName(product.CategoryId);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = FindProduct(id);
            if (product is null)
            {
                return NotFound();
            }

            SeedData.Products.Remove(product);
            return RedirectToAction(nameof(Index));
        }

        private static Product? FindProduct(int id) =>
            SeedData.Products.FirstOrDefault(product => product.ProductId == id);

        private static string? FindCategoryName(int categoryId) =>
            SeedData.Categories.FirstOrDefault(category => category.CategoryId == categoryId)?.CategoryName;

        private void LoadCategories(int? selectedCategoryId = null)
        {
            ViewBag.CategoryId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                SeedData.Categories,
                nameof(Category.CategoryId),
                nameof(Category.CategoryName),
                selectedCategoryId);
        }

        private void ValidateCategory(int categoryId)
        {
            if (!SeedData.Categories.Any(category => category.CategoryId == categoryId))
            {
                ModelState.AddModelError(nameof(Product.CategoryId), "Vui lòng chọn danh mục hợp lệ.");
            }
        }
    }
}

using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eStore.Controllers
{
    public class ProductsController : Controller
    {
        IProductRepository productRepository = new ProductRepository();


        public ActionResult Index(string? name, int? price)
        {
            IEnumerable<Product> products = productRepository.GetAllProduct();
            if (name != null)
            {
                ViewBag.SearchName = name;
                return View(products.Where(p => p.ProductName
                    .Contains(name, StringComparison.OrdinalIgnoreCase)));
            }

            return View(products);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            var product = productRepository.GetProductById(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = GetCategoryIdList();
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                    productRepository.Insert(product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.CategoryId = GetCategoryIdList();
                ViewBag.Message = ex.Message;
                return View(product);
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = productRepository.GetProductById(id);
            if (product == null)
                return NotFound();

            ViewBag.CategoryId = GetCategoryIdList();
            return View(product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.ProductId = id;
                    productRepository.Update(product);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                ViewBag.CategoryId = GetCategoryIdList();
                return View(product);
            }
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = productRepository.GetProductById(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                productRepository.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(product);
            }
        }

        private IEnumerable<SelectListItem> GetCategoryIdList()
        {
            int n = 10;
            var list = new List<SelectListItem>(n);
            for (int i = 1; i <= n; i++)
            {
                list.Add(new SelectListItem(i.ToString(), i.ToString()));
            }
            return list;
        }
    }
}

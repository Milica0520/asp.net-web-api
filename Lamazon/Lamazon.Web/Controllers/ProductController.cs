using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels;
using Lamazon.Services.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lamazon.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;   
            
        }
        public IActionResult Index()
        {
            List<ProductViewModel> allProducts= _productService.GetAllProducts();
            return View(allProducts);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]

        public IActionResult Create() { 
        
            CreateProductViewModel model = new CreateProductViewModel();    

            List<ProductCategoryViewModel> allProductCategorie = 

                _productCategoryService.GetAllProductCategories();

            ViewBag.ProductCategories = new SelectList(allProductCategorie, "Id", "Name");

            return View(model);  
        }



        [HttpPost]
        [Authorize(Roles = "ADMIN")]

        public IActionResult Create([FromForm] CreateProductViewModel model) {
            try
            {
                _productService.CreateProduct(model);
                return RedirectToAction("Index");
            }
            catch
            {
                //todo
                return null;
            }
        }


        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Details(int id)
        {

           ProductViewModel productViewModel =  _productService.GetProductById(id);
           

            return View(productViewModel);  
        }

        [HttpGet]

        public IActionResult Edit(int id)
        {
           ProductViewModel model = _productService.GetProductById(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]

        public IActionResult Edit([FromForm] UpdateProductViewModel productViewModel) {

           
            try
            {
                _productService.UpdateProduct(productViewModel);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

           
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]

        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
       
    }
}

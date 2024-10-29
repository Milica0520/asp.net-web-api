using Lamazon.DataAccess.Context;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Lamazon.Web.Controllers
{
    public class ProductCategoryController : Controller
    {

        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController  (IProductCategoryService productCategoryService)
        {
             _productCategoryService = productCategoryService;  
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_productCategoryService.GetAllProductCategories());
        }

        [HttpGet]   
        public IActionResult Create() {

            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] ProductCategoryViewModel categoryViewModel)
        {
            try
            {
                _productCategoryService.CreateProductCategory(categoryViewModel);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                //todo

                return View("Index");
            }
        }


        [HttpGet]   

        public IActionResult Details(int id)
        {
            ProductCategoryViewModel productCategory = _productCategoryService.GetProductCategoryById(id);
            return View(productCategory);  
        }

        [HttpGet]
        public IActionResult Delete(int id) {

            _productCategoryService.DeleteProductCategory(id);
            return RedirectToAction("Index");   
        
        }


        [HttpGet]

        public IActionResult Edit(int id) {

            ProductCategoryViewModel productCategoryViewModel = _productCategoryService.GetProductCategoryById(id);

            return View(productCategoryViewModel);
            
                
                }

        [HttpPost]

        public IActionResult Edit([FromForm] ProductCategoryViewModel categoryViewModel)
        {
            try
            {
                _productCategoryService.UpdateProductCategory(categoryViewModel);

                return RedirectToAction("Index");   
            }
            catch(Exception) 
            {
                return RedirectToAction("Index");   
            }
        
        
        }


    }
}

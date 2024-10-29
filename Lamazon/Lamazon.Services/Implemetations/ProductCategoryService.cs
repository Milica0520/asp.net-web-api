using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.Implemetations
{
    public class ProductCategoryService : IProductCategoryService
    {

        private readonly IProductCategoryRepository _repository;

        public ProductCategoryService(IProductCategoryRepository repository)
        {
            _repository = repository; 
        }
        public void CreateProductCategory(ProductCategoryViewModel productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException("The model is NULLL");

            if (string.IsNullOrEmpty(productCategory.Name))
                throw new ArgumentException("Property can not be null");

            ProductCategory productCategoryInsert = new ProductCategory()
            {
                Name = productCategory.Name
            };

            int productCategoryId = _repository.Insert(productCategoryInsert);  

            if(productCategoryId <= 0) {
                throw new Exception("Something is wrong");
            }
        }


        public void DeleteProductCategory(int id)
        {
            if (id <= 0)
                throw new Exception("The id must be valid");

            _repository.Delete(id); 
        }

        public List<ProductCategoryViewModel> GetAllProductCategories()
        {
            List<ProductCategory>  productCategories = _repository.GetAll();

            List<ProductCategoryViewModel> result = productCategories
                .Select(x => new ProductCategoryViewModel()
                {
                    Id = x.Id,  
                    Name = x.Name,
                })
                .ToList();

            return result;
        }

        public ProductCategoryViewModel GetProductCategoryById(int id)
        {

            if(id <= 0)
                throw new Exception("Id not valid");

            ProductCategory category = _repository.GetById(id); 

            ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel()
            {
                Id= category.Id,
                Name = category.Name,
            };

            return productCategoryViewModel;
        }

        public void UpdateProductCategory(ProductCategoryViewModel productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException("The provided value is null");

            if (string.IsNullOrEmpty(productCategory.Name))
                throw new Exception("The Provided name is null");

            ProductCategory productCategoryDb = _repository.GetById(productCategory.Id);

            productCategoryDb.Name = productCategory.Name;

           _repository.Update(productCategoryDb);   
        }
    }

      
       


       
    
}

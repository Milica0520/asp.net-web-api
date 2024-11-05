using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.Implemetations
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }
        public List<ProductViewModel> GetAllProducts()
        {
            return _productRepository.GetAll().Select(p => new ProductViewModel()
            {
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Price = p.Price,
                Id = p.Id,
                ProductCategoryId = p.ProductCategoryId,
                ProductCategoryName = p.ProductCategory.Name,
            })
                .ToList();
        }


        public ProductViewModel GetProductById(int id)
        {
            Product productById = _productRepository.Get(id);

            if (productById == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            ProductViewModel product = new ProductViewModel()
            {
                Description = productById.Description,
                ImageUrl = productById.ImageUrl,
                Name = productById.Name,
                Price = productById.Price,
                Id = productById.Id,
                ProductCategoryId = productById.ProductCategoryId,
                ProductCategoryName = productById.ProductCategory.Name,

            };

            return product;
        }
        public void CreateProduct(CreateProductViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Provided model is null!");
            }

            if (model.ProductCategoryId <= 0)
            {
                throw new ArgumentException("Product category not valid");
            }

            Product product = new Product()
            {
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Name = model.Name,
                ProductCategoryId = model.ProductCategoryId,
                Price = model.Price,
            };

            int productId = _productRepository.Insert(product);

            if (productId == 0)
            {
                throw new Exception("Something went wrong.");
            }

        }

        public void UpdateProduct(UpdateProductViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("The provided value is null");

            if (string.IsNullOrEmpty(model.Name))
                throw new Exception("The Provided name is null");
            Product product = _productRepository.Get(model.Id);

     
            product.Description = model.Description;
            product.ImageUrl = model.ImageUrl;
            product.Name = model.Name;
            product.Price = model.Price;



            _productRepository.Update(product);
        }

        public void DeleteProduct(int id)
        {
            if (id <= 0)
                throw new Exception("The id must be valid");

            _productRepository.Delete(id);

        }

     

       


        
    }
}

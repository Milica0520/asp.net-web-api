
using Lamazon.DataAccess.Context;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.DataAccess.Implementations
{
    public class ProductCategoryRepository : IProductCategoryRepository 
    {

        private readonly LamazonDbContext _dbContext;


        public ProductCategoryRepository(LamazonDbContext context)
        {
            _dbContext = context;
        }


        public List<ProductCategory> GetAll()
        {
            List<ProductCategory> productCategories = _dbContext.ProductCategories.ToList();

            return productCategories;
        }

        public ProductCategory GetById(int id)
        {
            ProductCategory productCategory = _dbContext.ProductCategories.FirstOrDefault(c => c.Id == id);


            if (productCategory == null)
            {
                throw new Exception("Category not faund");
            }


            return productCategory;
        }

        public int Insert(ProductCategory category)
        {
           _dbContext
                .ProductCategories
                .Add(category);

            _dbContext
                .SaveChanges();


            return category.Id;
        }

        public void Update(ProductCategory category)
        {
           _dbContext
                .ProductCategories
                .Update(category);

            _dbContext
                .SaveChanges(); 
        }

        public void Delete(int id)
        {
            ProductCategory productCategory = _dbContext.ProductCategories.FirstOrDefault(c => c.Id == id); 


            if (productCategory == null) { 
            throw new Exception("Category not faund");
            }


            _dbContext.ProductCategories.Remove(productCategory);   


            _dbContext.SaveChanges();   
           
        }

       
    }
}

using Lamazon.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.Interfaces
{
    public interface IProductCategoryService
    {
        List<ProductCategoryViewModel> GetAllProductCategories();

        ProductCategoryViewModel GetProductCategoryById(int id);

        void CreateProductCategory (ProductCategoryViewModel productCategory);

        void UpdateProductCategory(ProductCategoryViewModel productCategory);

        void DeleteProductCategory(int id);


    } 
}

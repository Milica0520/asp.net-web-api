using Lamazon.Services.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.Interfaces
{
    public interface  IProductService
    {

        List<ProductViewModel> GetAllProducts();
        ProductViewModel GetProductById(int id);
        void CreateProduct(CreateProductViewModel model);
        void UpdateProduct(UpdateProductViewModel model);
        void DeleteProduct(int id);
    }
}

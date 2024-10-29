using Lamazon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IProductCategoryRepository
    {
        List<ProductCategory> GetAll();

        ProductCategory GetById(int id);


        int Insert(ProductCategory category);   

        void Update(ProductCategory category);

        void Delete(int id);


    }
}

using Lamazon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll();

        Product Get(int id);

        int Insert (Product product);

        void Update(Product product);

        void Delete(int id);


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}

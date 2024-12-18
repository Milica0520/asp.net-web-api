using System;
using System.Collections.Generic;

namespace Lamazon.AdminAPI.Domein;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public decimal Price { get; set; }

    public int ProductCategoryId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ProductCategory ProductCategory { get; set; } = null!;
}

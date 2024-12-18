using System;
using System.Collections.Generic;

namespace Lamazon.AdminAPI.Domein;

public partial class Order
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public int UserId { get; set; }

    public bool IsActive { get; set; }

    public decimal TotalPrice { get; set; }

    public string? ShippingUserFullName { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}

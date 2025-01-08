using System;
using System.Collections.Generic;

namespace Lamazon.AdminAPI.Domein;

public partial class Rental
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public int UserId { get; set; }

    public DateTime RentedOn { get; set; }

    public DateTime ReturnedOn { get; set; }
}

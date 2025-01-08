using System;
using System.Collections.Generic;

namespace Lamazon.AdminAPI.Domein;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int Genre { get; set; }

    public int Language { get; set; }

    public bool IsAvailable { get; set; }

    public DateTime ReleaseDate { get; set; }

    public TimeOnly Length { get; set; }

    public int AgeRestriction { get; set; }

    public int Quantity { get; set; }
}

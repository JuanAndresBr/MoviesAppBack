using System;
using System.Collections.Generic;

namespace Movies.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nam { get; set; }

    public string? Username { get; set; }

    public string? Pass { get; set; }

    public virtual ICollection<ListFavorite> ListFavorites { get; set; } = new List<ListFavorite>();
}

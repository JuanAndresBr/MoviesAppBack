using System;
using System.Collections.Generic;

namespace Movies.Models;

public partial class ListFavorite
{
    public int IdUser { get; set; }

    public int IdMovie { get; set; }

    public virtual Usuario IdUserNavigation { get; set; } = null!;
}

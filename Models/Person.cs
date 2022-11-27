using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVDB.Models;

public partial class Person
{
    public long Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public decimal? Birth { get; set; }

    public virtual ICollection<Movie> DirectedMovies { get; } = new List<Movie>();

    public virtual ICollection<Movie> StarredMovies { get; } = new List<Movie>();
}

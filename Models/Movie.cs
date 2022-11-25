using System;
using System.Collections.Generic;

namespace MVDB.Models;

public partial class Movie
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal? Year { get; set; }

    public virtual Rating? Rating { get; set; }

    public virtual ICollection<Person> Directors { get; } = new List<Person>();

    public virtual ICollection<Person> Stars { get; } = new List<Person>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVDB.Models;

public partial class Movie
{
    public long Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public decimal? Year { get; set; }

    public virtual Rating? Rating { get; set; }

    public virtual ICollection<Person> Directors { get; } = new List<Person>();

    public virtual ICollection<Person> Stars { get; } = new List<Person>();

    public double GetRating()
    {
        if (Rating == null) return 0;
        return Rating.IMDBRating;
    }

    public long GetVotes()
    {
        if (Rating == null) return 0;
        return Rating.Votes;
    }
}
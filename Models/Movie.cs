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

    public virtual IList<Person> Directors { get; } = new List<Person>();

    public virtual IList<Person> Stars { get; } = new List<Person>();

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

    public IEnumerable<string> GetStars()
    {
        var stars = new List<string>();
        foreach (var person in Stars)
        {
            stars.Add(person.Name);
        }
        return stars;
    }

    public IEnumerable<string> GetDirectors()
    {
        var directors = new List<string>();
        foreach (var person in Directors)
        {
            directors.Add(person.Name);
        }
        return directors;
    }
}
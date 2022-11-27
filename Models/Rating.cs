using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVDB.Models;

public partial class Rating
{
    public long MovieId { get; set; }

    public double IMDBRating { get; set; }
        
    public long Votes { get; set; }

    [Required]
    public virtual Movie Movie { get; set; } = null!;
}

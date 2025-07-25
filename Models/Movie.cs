using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesMVC.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required] // title can not be null
        public string Title { get; set; } = string.Empty;

        public string? Genre { get; set; }

        [Display(Name = "Release Date")] // Display Release Date instead of ReleaseDate
        [DataType(DataType.Date)] // Tells the browser that the input should be treated as a date
        public DateTime ReleaseDate { get; set; }

        [Range(0, 100)]
        public decimal Rating { get; set; }

    }
}
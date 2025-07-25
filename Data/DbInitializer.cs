using MoviesMVC.Models;

namespace MoviesMVC.Data
{
    // Helper class to add some initial data to the database if it is empty

    public static class DbInitializer
    {
        public static void Initialize(MovieContext context)
        {
            if (context.Movies.Any())
            {
                return;
            }

            var movies = new Movie[]
            {
                new Movie { Title = "Inception", ReleaseDate = DateTime.Parse("2010-07-16"), Genre = "Sci-Fi", Price = 12.99M },
                new Movie { Title = "The Godfather", ReleaseDate = DateTime.Parse("1972-03-24"), Genre = "Crime", Price = 9.99M },
                new Movie { Title = "Interstellar", ReleaseDate = DateTime.Parse("2014-11-07"), Genre = "Sci-Fi", Price = 14.99M },
                new Movie { Title = "Shrek", ReleaseDate = DateTime.Parse("2001-05-18"), Genre = "Animation", Price = 7.50M }
            };

            context.Movies.AddRange(movies);
            context.SaveChanges();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using MoviesMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace MoviesMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString) // Default action - list movies and search
        {
            // search string is automatically filled by the url query parameter
            // var movies = _context.Movies; // Linq method syntax - needs to be converted to iQueryable

            var movies = from m in _context.Movies // Linq query syntax - returns an IQueryable
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            return View(await movies.ToListAsync()); // Sends movie list to the View
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create

        [HttpPost] // Handles only POST requests
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, ReleaseDate, Genre, Price")] Movie movie) // binds form inputs to model properties
        {
            if (ModelState.IsValid) // Checks that input data is valid
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to index view
            }

            return View(movie); // If input data is not valid, re-display the form with entered data for the user to correct
        }

        //GET Movies/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie); // Returns Edit.cshtml view with the movie data for the user to update
        }


        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id, Title, ReleaseDate, Genre, Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Movies.Any(e => e.Id == movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie); // If validation fails, redisplay the form
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            

        }
    }
}
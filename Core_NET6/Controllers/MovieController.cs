using Core_NET6.Data;
using Core_NET6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace Core_NET6.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MovieController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // var moviesList = _db.Movies.ToList();  
            IEnumerable<Movie> moviesList = _db.Movies;   // vaihtoehtoinen tapa
            return View(moviesList);
        }
        
        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie obj)
        {
            // custom validation test1
            if (obj.Name == "Star Wars" && obj.Rating != 10)
            {
                ModelState.AddModelError("rating", "Did you forget to give a rating of 10?");
            }

            // custom validation test2
            var movie = _db.Movies.Where(x => x.Name == obj.Name).FirstOrDefault();
            if (movie != null) { ModelState.AddModelError("name", "This movie is already listed"); }

            // normal validation
            if (ModelState.IsValid) 
            { 
                _db.Movies.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);   
        }
    }
}

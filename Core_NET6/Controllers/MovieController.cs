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

        // GET
        public IActionResult Edit(int? id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // var movieFromDb = _db.Movies.Where(x => x.Id == id).FirstOrDefault();
            var movieFromDb = _db.Movies.Find(id);

            if (movieFromDb == null)
            {
                return NotFound();
            }   
            return View(movieFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie obj) 
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
                _db.Movies.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // var movieFromDb = _db.Movies.Where(x => x.Id == id).FirstOrDefault();
            var movieFromDb = _db.Movies.Find(id);

            if (movieFromDb == null)
            {
                return NotFound();
            }
            return View(movieFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            var movie = _db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            _db.Movies.Remove(movie);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

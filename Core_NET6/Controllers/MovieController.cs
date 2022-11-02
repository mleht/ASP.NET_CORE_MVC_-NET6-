﻿using Core_NET6.Data;
using Core_NET6.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
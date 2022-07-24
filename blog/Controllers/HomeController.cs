﻿using Microsoft.AspNetCore.Mvc;
using blog.Models;
using blog.Data;
using System.Threading.Tasks;
using blog.Data.Repository;
namespace blog.Controllers
{
    public class HomeController : Controller
    {

        private IRepository _repo;

        public HomeController(IRepository repo) 
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);

            return View(post);
        }






    }
}

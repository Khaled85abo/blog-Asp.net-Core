﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using blog.Data.Repository;
using System.Threading.Tasks;
using blog.Models;

namespace blog.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PanelController : Controller
    {

        private IRepository _repo;

        public PanelController(IRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }



        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new Post());
            else
            {
                // C# sintax to convert a variable to integer (casting)
                var post = _repo.GetPost((int)id);
                return View(post);
            }


        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);

            await _repo.SaveChangesAsync();
            return RedirectToAction("index");
        }
    }
}

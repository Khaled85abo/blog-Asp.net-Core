using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using blog.Data.Repository;
using blog.Data.FileManager;
using System.Threading.Tasks;
using blog.Models;
using blog.ViewModels;

namespace blog.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PanelController : Controller
    {

        private IRepository _repo;
        private IFileManager _fileManager;
        public PanelController(IRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
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
                return View(new PostViewModel());
            else
            {
                // C# sintax to convert a variable to integer (casting)
                var post = _repo.GetPost((int)id);
                return View( new PostViewModel
                {
                    Title = post.Title,
                    Id = post.Id,
                    Body = post.Body,
                    CurrentImage= post.Image,
                    Description = post.Description,
                    Tags = post.Tags, 
                    Category = post.Category
                } );
            }


        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
                Description = vm.Description, 
                Tags= vm.Tags, 
                Category = vm.Category
            };

            if (vm.Image == null)
                post.Image = vm.CurrentImage;
            else
                // Remvoe the previoius image before adding a new one.
                if (!string.IsNullOrEmpty(vm.CurrentImage))
                _fileManager.RemoveImage(vm.CurrentImage);
                post.Image = await _fileManager.SaveImage(vm.Image);

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

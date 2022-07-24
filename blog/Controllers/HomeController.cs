using Microsoft.AspNetCore.Mvc;
using blog.Models;
using blog.Data;
using System.Threading.Tasks;
using blog.Data.Repository;
using blog.Data.FileManager;
namespace blog.Controllers
{
    public class HomeController : Controller
    {

        private IRepository _repo;
        private IFileManager _fileManager;
        public HomeController(IRepository repo, IFileManager fileManager) 
        {
            _repo = repo;
            _fileManager = fileManager;
        }
        public IActionResult Index(string category)
        {
            var posts = string.IsNullOrEmpty(category) ? _repo.GetAllPosts() : _repo.GetAllPosts(category);
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);

            return View(post);
        }

        [HttpGet("/image/{image}")]
        public IActionResult Iamge(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }



    }
}

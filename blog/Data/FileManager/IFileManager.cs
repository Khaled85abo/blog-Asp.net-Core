using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace blog.Data.FileManager
{
    public interface IFileManager
    {
        FileStream ImageStream(string image);
        Task<string> SaveImage(IFormFile img);
        bool RemoveImage(string image);
    }
}

using ProductHub.Storage.Contract;
using ProductHub.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.Storage.Services
{
    public class ImageService : IImageService
    {
        async Task<ProcessResult> IImageService.Upload(string fileName, Stream fileStream)
        {
            if (fileStream != null && fileStream.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "images");
                
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filePath = Path.Combine(path, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(stream);
                }

                return new ProcessResult(filePath, true);
            }

            return new ProcessResult(string.Empty, false);
        }
    }
}

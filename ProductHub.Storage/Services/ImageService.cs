using Microsoft.Extensions.Configuration;
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
        private readonly string _imagePath;
        public ImageService(IConfiguration configuration)
        {
            _imagePath = configuration["ImagesPath"] ?? "img";
        }
        public async Task<ProcessResult> Upload(string fileName, Stream fileStream)
        {
            if (fileStream != null && fileStream.Length > 0)
            {
                var path = GetPath();
                
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

        public Task<ProcessResult> Delete(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return Task.FromResult(new ProcessResult(filePath, true));
                }
                else
                {
                    return Task.FromResult(new ProcessResult($"File not found: {filePath}", false));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ProcessResult($"Error deleting file: {ex.Message}", false));
            }
        }
        private string GetPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), _imagePath);
        }
    }
}

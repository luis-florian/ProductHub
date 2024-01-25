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
    public class ImageFileService(IConfiguration configuration) : IImageFileService
    {
        private readonly string _imagePath = configuration["ImagesPath"] ?? "img";

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

                return new ProcessResult(filePath, string.Empty, true);
            }
            else
            {
                return new ProcessResult(string.Empty, $"FileStream or FileName is empty: {fileName}", false);
            }
        }

        public Task<ProcessResult> Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(new ProcessResult(filePath, string.Empty, true));
            }
            else
            {
                return Task.FromResult(new ProcessResult(string.Empty, $"File not found: {filePath}", false));
            }
        }

        private string GetPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), _imagePath);
        }
    }
}

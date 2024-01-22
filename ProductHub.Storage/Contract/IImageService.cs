using ProductHub.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.Storage.Contract
{
    public interface IImageService
    {
        Task<ProcessResult> Upload(string fileName, Stream fileStream);

        Task<ProcessResult> Delete(string fileName);
    }
}

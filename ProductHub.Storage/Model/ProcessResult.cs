using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.Storage.Model
{
    public class ProcessResult(string filePath, string message,  bool processedSuccessfully)
    {
        public string FilePath { get; } = filePath;
        public bool ProcessedSuccessfully { get; } = processedSuccessfully;
        public string Message { get; } = message;
    }
}

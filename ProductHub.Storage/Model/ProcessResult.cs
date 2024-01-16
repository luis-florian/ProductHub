using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.Storage.Model
{
    public class ProcessResult(string filename, bool processedSuccessfully)
    {
        public string FileName { get; } = filename;
        public bool ProcessedSuccessfully { get; } = processedSuccessfully;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.Database.Model
{
    public class MissingRelatedEntityException : Exception
    {
        public MissingRelatedEntityException()
        {
        }

        public MissingRelatedEntityException(string message) : base(message)
        {
        }

        public MissingRelatedEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

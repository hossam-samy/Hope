using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Common.GlobalException
{
    public class UnAuthrizedException:Exception
    {
        public UnAuthrizedException(string message) : base(message) { }

    }
}

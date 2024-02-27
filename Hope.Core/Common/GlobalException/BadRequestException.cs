using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Common.GlobalException
{
    public class BadRequestException:Exception
    {
        public BadRequestException(string messsage):base(messsage) { }  
    }
}

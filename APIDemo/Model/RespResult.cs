using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Model
{
    public class RespResult<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMsg { get; set; }
        public T Result { get; set; }
    }
}

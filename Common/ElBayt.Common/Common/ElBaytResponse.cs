using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Common.Common
{
    public class ElBaytResponse<T>
    {
        public string Result;
        public T Data;
        public List<string> Errors;
    }
}

using ElBayt.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Common.Common
{
    public class ElBaytResponse<T>
    {
        public EnumResponseResult Result { set; get; }
        public T Data { set; get; }
        public List<string> Errors { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Common.Core.Services
{
    public class BasicDto<T>
    {
        protected BasicDto()
        {
        }

        public virtual T Id { get; set; }
    }
}

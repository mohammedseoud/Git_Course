using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Common.Core.Services
{
    public class BaseDto<T>
    {
        protected BaseDto()
        {
        }

        public virtual T Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}

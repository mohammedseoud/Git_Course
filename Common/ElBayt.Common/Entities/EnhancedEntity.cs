using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Common.Entities
{
    public class EnhancedEntity<T> : Entity<T> where T : IEquatable<T>
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}

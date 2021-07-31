using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Common.Entities
{
    public abstract class Entity<TKey> : IEquatable<Entity<TKey>>
       where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Entity Identifier.
        /// </summary>
        public virtual TKey Id { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<TKey>);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return Id.GetHashCode();
        }

        public virtual bool Equals(Entity<TKey> other)
        {
            if (other == null)
            {
                return false;
            }

            return Id.Equals(other.Id);
        }
    }
}

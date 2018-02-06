using System;

namespace In.Legacy.Entity
{
    public abstract class TrackableEntity<TKey>: HasKeyBase<TKey>
    {
        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }
    }
}
using System;

namespace SilentNotary.Common.Entity
{
    public abstract class TrackableEntity<TKey>: HasKeyBase<TKey>
    {
        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }
    }
}
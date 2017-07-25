using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace SmartDotNet.EF6.RuntimeUtils
{
    public static class DbContextExtensions
    {
        public static T ReAttach<T>(this DbContext context, T entity)
            where T: class
        {
            if (entity == null) return null;

            var objContext = ((IObjectContextAdapter)context).ObjectContext;
            var objSet = objContext.CreateObjectSet<T>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            ObjectStateEntry entry;
            bool attach;
            if (objContext.ObjectStateManager.TryGetObjectStateEntry(entityKey, out entry))
            {
                attach = entry.State == EntityState.Detached;
                entity = (T)entry.Entity;
            }
            else attach = true;
            if (attach)
                objContext.AttachTo(objSet.EntitySet.Name, entity);

            return entity;
        }

        public static ICollection<T> ReAttachCollection<T>(this DbContext context, ICollection<T> source)
            where T: class
        {
            return source.Select(context.ReAttach).ToList<T>();
        }
    }
}

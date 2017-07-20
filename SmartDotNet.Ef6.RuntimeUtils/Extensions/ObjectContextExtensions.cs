using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace SmartDotNet.Ef6.RuntimeUtils.Extensions
{
    public static class ObjectContextExtensions
    {
        public static void ReAttach<T>(this DbContext context, ref T entity)
            where T: class
        {
            if (entity == null) return;

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
        }
    }
}

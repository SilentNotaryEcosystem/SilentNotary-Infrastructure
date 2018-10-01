using SilentNotary.Common.Entity.Uow;
using SilentNotary.Cqrs.Domain.Interfaces;
using System.Threading.Tasks;

namespace SilentNotary.Common.Command
{
    public class SaveCommandHandler<TEntity>
        where TEntity : class, IHasKey
    {
        private readonly IDataSetUow _ctx;

        public SaveCommandHandler(IDataSetUow ctx)
        {
            _ctx = ctx;
        }


        public async Task<string> Handle(params TEntity[] messages)
        {
            foreach (var msg in messages)
            {
                _ctx.FixupState(msg);
            }

            return (await _ctx.CommitAsync())
                .ToString();
        }
    }
}
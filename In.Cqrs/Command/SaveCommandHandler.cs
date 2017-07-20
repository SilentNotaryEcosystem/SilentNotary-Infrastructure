using System.Threading.Tasks;
using In.Entity.Uow;
using SmartDotNet.Cqrs.Domain;

namespace In.Cqrs.Command
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
using Microsoft.EntityFrameworkCore;
using POC.ChatSignal.Domain.Entity.Base;
using POC.ChatSignal.Domain.Interfaces.Repository.Base;

namespace POC.ChatSignal.Sql.Repository.Base
{
    public abstract class RepositoryBase<TEntity>(ChatDbContext dbContext) : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly ChatDbContext dbContext = dbContext;

        public async Task Atualizar(TEntity entidade)
        {
            dbContext.Entry(entidade).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task<TEntity?> BuscarPorId(int id)
            => await dbContext.Set<TEntity>().Where(e => e.ID == id).FirstOrDefaultAsync();

        public async Task Inserir(TEntity entidade)
        {
            dbContext.Entry(entidade).State = EntityState.Added;
            await dbContext.SaveChangesAsync();
        }

        public async Task<IList<TEntity>> Listar()
            => await dbContext.Set<TEntity>().ToListAsync();

        public async Task Remover(int id)
        {
            var entidade = await dbContext.Set<TEntity>().Where(e => e.ID == id).FirstOrDefaultAsync();

            dbContext.Entry(entidade).State = EntityState.Deleted;
            await dbContext.SaveChangesAsync();
        }
    }
}

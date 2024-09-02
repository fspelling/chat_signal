using POC.ChatSignal.Domain.Entity.Base;
using POC.ChatSignal.Domain.Interfaces.Repository.Base;

namespace POC.ChatSignal.Sql.Repository.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        public Task Atualizar(TEntity entidade)
        {
            throw new NotImplementedException();
        }

        public Task Inserir(TEntity entidade)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TEntity>> Listar()
        {
            throw new NotImplementedException();
        }

        public Task Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}

using POC.ChatSignal.Domain.Entity.Base;

namespace POC.ChatSignal.Domain.Interfaces.Repository.Base
{
    public interface IRepositoryBase<Entity> where Entity : EntityBase
    {
        Task Inserir(Entity entidade);
        Task Atualizar(Entity entidade);
        Task<IList<Entity>> Listar();
        Task<Entity?> BuscarPorId(int id);
        Task Remover(int id);
    }
}

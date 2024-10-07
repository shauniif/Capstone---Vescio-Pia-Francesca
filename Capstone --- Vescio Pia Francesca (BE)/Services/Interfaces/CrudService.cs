using Capstone_____Vescio_Pia_Francesca__BE_.Entity;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface CrudService<T, Model> where T : BaseEntity
    {
        Task<T> Create(Model entity);
        Task<T> Read(int id);
        Task<T> Update(Model entity);
        Task<T> Delete(int id);
        Task<IEnumerable<T>> GetAll();
    }
}

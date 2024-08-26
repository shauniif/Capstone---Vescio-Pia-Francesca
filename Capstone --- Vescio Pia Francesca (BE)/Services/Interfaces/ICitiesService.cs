using Capstone_____Vescio_Pia_Francesca__BE_.Entity;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface ICitiesService
    {
        Task<City> Create(City entity);
        Task<City> Read(int id);
        Task<City> Update(City entity);

        Task<City> Delete(int id);

        Task<IEnumerable<City>> GetAllCities();
    }
}

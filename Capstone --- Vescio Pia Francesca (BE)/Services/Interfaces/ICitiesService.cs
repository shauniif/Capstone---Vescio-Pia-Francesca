using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface ICitiesService
    {
        Task<City> Create(CityModel entity);
        Task<City> Read(int id);
        Task<CityModel> Get(int id);
        Task<City> GetCity(int id);

        Task<City> Update(CityModel entity);

        Task<City> Delete(int id);

        Task<IEnumerable<City>> GetAllCities();
    }
}

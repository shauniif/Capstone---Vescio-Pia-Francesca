using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface ICitiesService : CrudService<City, CityModel>
    {      
        Task<CityModel> Get(int id);
        Task<City> GetCity(int id);

        Task<IEnumerable<City>> Search(string query);
    }
}

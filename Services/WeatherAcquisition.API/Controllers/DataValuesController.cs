using WeatherAcquisition.API.Controllers.Base;
using WeatherAcquisition.DAL.Entities;
using WeatherAcquisition.Interfaces.Base.Repositories;

namespace WeatherAcquisition.API.Controllers
{
    public class DataValuesController : EntityController<DataValue>
    {
        public DataValuesController(IRepository<DataValue> Repository) : base(Repository) { }
    }
}

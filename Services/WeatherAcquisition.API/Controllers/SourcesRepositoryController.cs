using AutoMapper;
using WeatherAcquisition.API.Controllers.Base;
using WeatherAcquisition.DAL.Entities;
using WeatherAcquisition.Domain.Base;
using WeatherAcquisition.Interfaces.Base.Repositories;

namespace WeatherAcquisition.API.Controllers
{
    public class SourcesRepositoryController : MappedEntityController<DataSourceInfo, DataSource>
    {
        public SourcesRepositoryController(IRepository<DataSource> Repository, IMapper Mapper) 
            : base(Repository, Mapper)
        {
        }
    }
}

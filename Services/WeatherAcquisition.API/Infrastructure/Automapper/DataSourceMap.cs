using AutoMapper;
using WeatherAcquisition.DAL.Entities;
using WeatherAcquisition.Domain.Base;

namespace WeatherAcquisition.API.Infrastructure.Automapper
{
    public class DataSourceMap : Profile
    {
        public DataSourceMap()
        {
            CreateMap<DataSourceInfo, DataSource>()
               .ReverseMap();
        }
    }
}

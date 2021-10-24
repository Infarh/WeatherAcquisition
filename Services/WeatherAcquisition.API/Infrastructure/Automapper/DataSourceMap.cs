using AutoMapper;
using WeatherAcquisition.DAL.Entities;
using WeatherAcquisition.Domain.Base;

namespace WeatherAcquisition.API.Infrastructure.AutoMapper
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

using WeatherAcquisition.Interfaces.Base.Entities;

namespace WeatherAcquisition.Domain.Base
{
    public class DataSourceInfo : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}

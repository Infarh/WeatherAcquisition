using WeatherAcquisition.Interfaces.Base.Entities;

namespace WeatherAcquisition.DAL.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; set; }
    }
}

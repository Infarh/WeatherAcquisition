using WeatherAcquisition.Interfaces.Base.Entities;

namespace WeatherAcquisition.DAL.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}

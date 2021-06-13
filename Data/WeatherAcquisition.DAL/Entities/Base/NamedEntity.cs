using System.ComponentModel.DataAnnotations;
using WeatherAcquisition.Interfaces.Base.Entities;

namespace WeatherAcquisition.DAL.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        [Required]
        public string Name { get; set; }
    }
}

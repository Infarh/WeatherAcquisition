using System.ComponentModel.DataAnnotations;

namespace WeatherAcquisition.Interfaces.Base.Entities
{
    public interface INamedEntity : IEntity
    {
        [Required]
        string Name { get; }
    }
}

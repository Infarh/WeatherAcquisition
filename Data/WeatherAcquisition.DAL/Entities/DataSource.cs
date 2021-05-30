using WeatherAcquisition.DAL.Entities.Base;

namespace WeatherAcquisition.DAL.Entities
{
    public class DataSource : NamedEntity
    {
        public string Description { get; set; }

        //public ICollection<DataValue> Values { get; set; } = new HashSet<DataValue>();
    }
}

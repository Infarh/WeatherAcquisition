using System;
using System.ComponentModel.DataAnnotations;
using WeatherAcquisition.Interfaces.Base.Entities;

namespace WeatherAcquisition.Domain.Base
{
    public class DataSourceInfo : INamedEntity, IEquatable<DataSourceInfo>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public bool Equals(DataSourceInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Name == other.Name && Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DataSourceInfo)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash_code = Id;
                hash_code = (hash_code * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hash_code = (hash_code * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                return hash_code;
            }
        }

        public static bool operator ==(DataSourceInfo left, DataSourceInfo right) { return Equals(left, right); }
        public static bool operator !=(DataSourceInfo left, DataSourceInfo right) { return !Equals(left, right); }
    }
}

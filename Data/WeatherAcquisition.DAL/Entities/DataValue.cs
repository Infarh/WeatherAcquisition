using System;
using Microsoft.EntityFrameworkCore;
using WeatherAcquisition.DAL.Entities.Base;

namespace WeatherAcquisition.DAL.Entities
{
    [Index(nameof(Time))]
    public class DataValue : Entity
    {
        public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;

        public string Value { get; set; }

        public DataSource Source { get; set; }

        public bool IsFault { get; set; }
    }
}

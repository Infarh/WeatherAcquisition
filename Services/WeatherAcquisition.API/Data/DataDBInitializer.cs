using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WeatherAcquisition.DAL.Context;
using WeatherAcquisition.DAL.Entities;

namespace WeatherAcquisition.API.Data
{
    public class DataDBInitializer
    {
        private readonly DataDB _db;

        public DataDBInitializer(DataDB db) => _db = db;

        public void Initialize()
        {
            _db.Database.Migrate();

            if (_db.Sources.Any()) return;

            var rnd = new Random();
            for (var i = 1; i <= 10; i++)
            {
                var source = new DataSource
                {
                    Name = $"Источник-{i}",
                    Description = $"Тестовый источник данных №{i}",
                };

                _db.Sources.Add(source);

                var values = new DataValue[rnd.Next(10, 20)];
                for (var (j, count) = (0, values.Length); j < count; j++)
                {
                    var value = new DataValue
                    {
                        Source = source,
                        Time = DateTimeOffset.Now.AddDays(-rnd.Next(0, 365)),
                        Value = $"{rnd.Next(0, 30)}"
                    };
                    //_db.Values.Add(value);
                    values[j] = value;
                }

                _db.Values.AddRange(values);
            }

            _db.SaveChanges();
        }
    }
}

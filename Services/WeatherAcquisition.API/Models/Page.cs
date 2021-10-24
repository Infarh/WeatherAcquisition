using System;
using System.Collections.Generic;
using WeatherAcquisition.Interfaces.Base.Repositories;

namespace WeatherAcquisition.API.Models
{
    public record Page<T>(IEnumerable<T> Items, int TotalCount, int PageIndex, int PageSize) : IPage<T>
    {
        public int TotalPagesCount => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherAcquisition.Interfaces.Base.Entities;

namespace WeatherAcquisition.Interfaces.Base.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<bool> ExistId(int Id);

        Task<bool> Exist(T item);

        Task<int> GetCount();

        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> Get(int Skip, int Count);

        Task<IPage<T>> GetPage(int PageIndex, int PageSize);

        //async Task<T> GetById(int Id) => (await GetAll()).FirstOrDefault(item => item.Id == Id);
        Task<T> GetById(int Id);

        Task<T> Add(T item);

        Task<T> Update(T item);

        Task<T> Delete(T item);

        Task<T> DeleteById(int Id);
    }

    public interface IPage<T>
    {
        IEnumerable<T> Items { get; }

        int TotalCount { get; }

        int PageIndex { get; }

        int PageSize { get; }

        int TotalPagesCount => (int) Math.Ceiling((double) TotalCount / PageSize);
    }
}

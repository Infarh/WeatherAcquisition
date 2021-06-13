using System.Threading;
using System.Threading.Tasks;
using WeatherAcquisition.Interfaces.Base.Entities;

namespace WeatherAcquisition.Interfaces.Base.Repositories
{
    public interface INamedRepository<T> : IRepository<T> where T : INamedEntity
    {
        Task<bool> ExistName(string Name, CancellationToken Cancel = default);

        Task<T> GetByName(string Name, CancellationToken Cancel = default);

        Task<T> DeleteByName(string Name, CancellationToken Cancel = default);
    }
}

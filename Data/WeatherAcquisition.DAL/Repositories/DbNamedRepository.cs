using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherAcquisition.DAL.Context;
using WeatherAcquisition.DAL.Entities.Base;
using WeatherAcquisition.Interfaces.Base.Repositories;

namespace WeatherAcquisition.DAL.Repositories
{
    public class DbNamedRepository<T> : DbRepository<T>, INamedRepository<T> where T : NamedEntity, new()
    {
        public DbNamedRepository(DataDB db) : base(db) { }

        public async Task<bool> ExistName(string Name, CancellationToken Cancel = default)
        {
            return await Items.AnyAsync(item => item.Name == Name, Cancel).ConfigureAwait(false);
        }

        public async Task<T> GetByName(string Name, CancellationToken Cancel = default)
        {
            //return await Items.SingleOrDefaultAsync(item => item.Name == Name, Cancel).ConfigureAwait(false);
            return await Items.FirstOrDefaultAsync(item => item.Name == Name, Cancel).ConfigureAwait(false);
        }

        public async Task<T> DeleteByName(string Name, CancellationToken Cancel = default)
        {
            var item = Set.Local.FirstOrDefault(i => i.Name == Name);
            if (item is null)
                item = await Set
                   .Select(i => new T { Id = i.Id, Name = i.Name })
                   .FirstOrDefaultAsync(i => i.Name == Name, Cancel)
                   .ConfigureAwait(false);
            if (item is null)
                return null;

            return await Delete(item, Cancel).ConfigureAwait(false);
        }
    }
}

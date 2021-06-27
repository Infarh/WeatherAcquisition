using Microsoft.EntityFrameworkCore;
using WeatherAcquisition.DAL.Entities;

namespace WeatherAcquisition.DAL.Context
{
    public class DataDB : DbContext
    {
        public DbSet<DataValue> Values { get; set; }

        public DbSet<DataSource> Sources { get; set; }

        public DataDB(DbContextOptions<DataDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<DataSource>()
               .HasMany<DataValue>()
               .WithOne(v => v.Source)
               .OnDelete(DeleteBehavior.Cascade);

            //model.Entity<DataSource>()
            //   .Property(source => source.Name)
            //   .IsRequired();

            //model.Entity<DataSource>()
            //   .HasIndex(source => source.Name)
            //   .IsUnique();
        }
    }
}

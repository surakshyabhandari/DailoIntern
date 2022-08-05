using Microsoft.EntityFrameworkCore;
using SortingMVC.Entities;

namespace SortingMVC.Context
{
    public class SortingDbContext : DbContext
    {
        public SortingDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<SortData> SortDatas { get; set; }
    }
}

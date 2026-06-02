using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class MainContext : DbContext
    {
        #region constructor
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
        #endregion

        #region db set
       public DbSet<User> Users => Set<User>();
        #endregion

        #region seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}

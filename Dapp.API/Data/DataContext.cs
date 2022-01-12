using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Dapp.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){}

        public DbSet<Value> Values { get; set; }

        public DbSet<User> Users { get; set; }
        
    }
}
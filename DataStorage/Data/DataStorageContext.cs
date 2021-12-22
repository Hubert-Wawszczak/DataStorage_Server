using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataStorage.Models;

namespace DataStorage.Data
{
    public class DataStorageContext : DbContext
    {
        public DataStorageContext (DbContextOptions<DataStorageContext> options)
            : base(options)
        {
        }

        public DbSet<DataStorage.Models.BookViewModel> BookViewModel { get; set; }
    }
}

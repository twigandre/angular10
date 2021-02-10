using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using back_end_net_core_5.Dao.Entityes;

namespace back_end_net_core_5.Dao
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options)
                : base(options)
        {
            Console.WriteLine("new context starting");
        }

        public DbSet<LoginEntity> Login { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SEPractices4ML.Dao.Entityes;

namespace SEPractices4ML.Dao
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options)
                : base(options)
        {
            Console.WriteLine("new context starting");
        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<MembersEntity> Members { get; set; }
        public DbSet<NotificationEntity> Notification { get; set; }
        public DbSet<PracticesEntity> Practices { get; set; } 
        public DbSet<PracticesAnexoEntity> PracticesAnexo { get; set; }
        public DbSet<PracticesAuthorsEntity> PracticesAuthors { get; set; }

    }
}

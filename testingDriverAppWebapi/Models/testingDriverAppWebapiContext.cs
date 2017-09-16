using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace testingDriverAppWebapi.Models
{
    public class testingDriverAppWebapiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public testingDriverAppWebapiContext() : base("name=testingDriverAppWebapiContext")
        {
        }

        public System.Data.Entity.DbSet<testingDriverAppWebapi.Models.Entity> Entities { get; set; }

        public System.Data.Entity.DbSet<testingDriverAppWebapi.Models.Horse> Horses { get; set; }

        public System.Data.Entity.DbSet<testingDriverAppWebapi.Models.Job> Jobs { get; set; }

        public System.Data.Entity.DbSet<testingDriverAppWebapi.Models.JobEntity> JobEntities { get; set; }

        public System.Data.Entity.DbSet<testingDriverAppWebapi.Models.JobHorse> JobHorses { get; set; }


    }
}

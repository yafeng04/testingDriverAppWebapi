﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace testingDriverAppWebapi.Models
{
    public class testingDriverAppWebapiDTOContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public testingDriverAppWebapiDTOContext() : base("name=testingDriverAppWebapiDTOContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

        }

        public System.Data.Entity.DbSet<testingDriverAppWebapi.DTO.EntityDTO> EntityDTOes { get; set; }

        public System.Data.Entity.DbSet<testingDriverAppWebapi.DTO.HorseDTO> HorseDTOes { get; set; }

        public System.Data.Entity.DbSet<testingDriverAppWebapi.DTO.JobDTO> JobDTOes { get; set; }

        public System.Data.Entity.DbSet<testingDriverAppWebapi.DTO.JobEntityDTO> JobEntityDTOes { get; set; }

        public System.Data.Entity.DbSet<testingDriverAppWebapi.DTO.JobHorseDTO> JobHorseDTOes { get; set; }
    }
}

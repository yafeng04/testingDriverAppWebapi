using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testingDriverAppWebapi.DTO
{
    public class EntityDTO
    {
        [Key]
        public Guid EntityId { get; set; }

      
        public string Name { get; set; }

       
        public string Mobile { get; set; }

       
        public string Email { get; set; }

        
        public List<Guid> JobEntityIds { get; set; }
    }
}
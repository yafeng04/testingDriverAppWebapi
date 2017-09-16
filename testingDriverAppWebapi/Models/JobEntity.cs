using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testingDriverAppWebapi.Models
{
    public class JobEntity
    {
        [Key]
        public Guid JobEntityId { get; set; }


        public Guid JobId { get; set; }


        public Guid EntityId { get; set; }


        public string MethodToNotify { get; set; }


        public int NotifyTime { get; set; }


        public virtual Job Job { get; set; }
        public virtual Entity Entity { get; set; }
    }
}
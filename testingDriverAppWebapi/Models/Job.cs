using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testingDriverAppWebapi.Models
{
    public class Job
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Job()
        {
            this.JobHorses = new HashSet<JobHorse>();
            this.JobEntities = new HashSet<JobEntity>();
        }

        [Key]
        public Guid JobId { get; set; }

        public Guid DriverId { get; set; }

        public string VehicleId { get; set; }

        public DateTime PickUpTime { get; set; }

        public string PickUpLocation { get; set; }

        public DateTime DropOffTime { get; set; }

        public string DropOffLocation { get; set; }

        public string Comments { get; set; }

        public int Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobHorse> JobHorses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobEntity> JobEntities { get; set; }

        //public List<Guid> JobHorseIds { get; set; }

        //public List<Guid> JobEntityIds { get; set; }
    }
}
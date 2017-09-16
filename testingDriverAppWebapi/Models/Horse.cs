using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testingDriverAppWebapi.Models
{
    public class Horse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Horse()
        {
            this.JobHorses = new HashSet<JobHorse>();
        }

        [Key]
        public Guid HorseId { get; set; }


        public string Name { get; set; }


        public string Brand { get; set; }


        public string Microchip { get; set; }


        public string Colour { get; set; }


        public string Sex { get; set; }

        public string Hand { get; set; }

        public string Another { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobHorse> JobHorses { get; set; }

    }
}
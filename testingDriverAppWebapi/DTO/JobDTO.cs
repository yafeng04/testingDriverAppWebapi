using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testingDriverAppWebapi.DTO
{
    public class JobDTO
    {
        [Key]
        public Guid JobId { get; set; }

        public Guid DriverId { get; set; }


        public string VehicleId { get; set; }


        public DateTime PickUpTime { get; set; }


        public string PickUpLocation { get; set; }


        public DateTime DropOffTime { get; set; }


        public string DropOffLocation { get; set; }


        public string Comments { get; set; }

        //[JsonProperty("unnamedhorsequantity", Order = 7)]
        //public int UnnamedHorseQuantity { get; set; }


        public int Status { get; set; }


        public List<Guid> JobHorseIds { get; set; }

        public List<Guid> JobEntityIds { get; set; }

    }
}
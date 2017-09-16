using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testingDriverAppWebapi.DTO
{
    public class HorseDTO
    {

        [Key]
        public Guid HorseId { get; set; }


        public string Name { get; set; }


        public string Brand { get; set; }

        public string Microchip { get; set; }

        public string Colour { get; set; }

        public string Sex { get; set; }

        public List<Guid> JobHorseIds { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testingDriverAppWebapi.DTO
{
    public class JobHorseDTO
    {
        [Key]
        public Guid JobHorseId { get; set; }

        public Guid JobId { get; set; }

        public Guid HorseId { get; set; }

        public int Space { get; set; }

        public string Notes { get; set; }

        public bool HasCompanionHorse { get; set; }

    }
}
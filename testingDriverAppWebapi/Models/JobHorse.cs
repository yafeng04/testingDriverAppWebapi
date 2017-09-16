﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testingDriverAppWebapi.Models
{
    public class JobHorse
    {
        [Key]
        public Guid JobHorseId { get; set; }


        public Guid JobId { get; set; }


        public Guid HorseId { get; set; }


        public int Space { get; set; }


        public string Notes { get; set; }


        public bool HasCompanionHorse { get; set; }

        public virtual Job Job { get; set; }
        public virtual Horse Horse { get; set; }

    }
}
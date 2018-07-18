using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hurtle.Data.Model
{
    public class Telemetria
    {
        public int Id { get; set; }

        [Required]
        public double Latitudine { get; set; }

        [Required]
        public double Longitudine { get; set; }

        [Required]
        public DateTimeOffset Istante { get; set; }

        public int IdRunner { get; set; }

        public int IdAttivita { get; set; }

        public string UriSelfie { get; set; }
    }
}

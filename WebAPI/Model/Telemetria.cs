using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class Telemetria
    {
        //public int Id { get; set; }

        public double Latitudine { get; set; }

        public double Longitudine { get; set; }

        public DateTimeOffset Istante { get; set; }

        public int IdRunner { get; set; }

        public int IdAttivita { get; set; }

        public string Selfie { get; set; }
    }
}

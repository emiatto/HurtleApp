using Hurtle.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfRunning.Models.Hurtle
{
    public class AttivitaDettaglioViewModel
    {
        public AttivitaDettaglioViewModel()
        {

        }

        public int Id { get; set; }

        public string Titolo { get; set; }

        public int IdRunner { get; set; }

        public DateTime? DataCreazione { get; set; }

        public string Localita { get; set; }

        public int Tipologia { get; set; }

        public string UriGara { get; set; }

        public IEnumerable<Telemetria> TelemetrieArray { get; set; }

        //public string UriSelfie { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class Attivita
    {
        public int Id { get; set; }

        public string Titolo { get; set; }

        public int IdRunner { get; set; }

        public DateTime DataCreazione { get; set; }

        public string Localita { get; set; }

        public int Tipologia { get; set; }

        public string UriGara { get; set; }
    }
}

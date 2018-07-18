using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hurtle.Data.Model
{
    public class Attivita
    {
        public int Id { get; set; }

        [Required]
        public string Titolo { get; set; }

        public int IdRunner { get; set; }

        public DateTime? DataCreazione { get; set; }

        [Required]
        public string Localita { get; set; }

        public int Tipologia { get; set; }

        public string UriGara { get; set; }

    }
}

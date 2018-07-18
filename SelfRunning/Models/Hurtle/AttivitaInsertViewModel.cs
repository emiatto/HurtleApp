using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfRunning.Models.Hurtle
{
    public class AttivitaInsertViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Titolo { get; set; }

        public int IdRunner { get; set; }

        public DateTime? DataCreazione { get; set; }

        [Required]
        public string Localita { get; set; }

        [Required]
        public int Tipologia { get; set; }

        public string UriGara { get; set; }
    }
}

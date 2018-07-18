using Hurtle.Data;
using Hurtle.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfRunning.Models.Hurtle
{
    public class AttivitaViewModel
    {
        public AttivitaViewModel()
        {

        }

        public AttivitaViewModel(int id, string titolo, int idrunner, DateTime datacreazione, string localita, int tipologia, string urigara)
        {
            this.Id = id;
            this.Titolo = titolo;
            this.IdRunner = idrunner;
            this.DataCreazione = datacreazione;
            this.Localita = localita;
            this.Tipologia = tipologia;
            this.UriGara = urigara;
        }

        public AttivitaViewModel(Attivita a)
        {
            this.Id = a.Id;
            this.Titolo = a.Titolo;
            this.IdRunner = a.IdRunner;
            this.DataCreazione = a.DataCreazione;
            this.Localita = a.Localita;
            this.Tipologia = a.Tipologia;
            this.UriGara = a.UriGara;
        }

        public int Id { get; set; }

        public string Titolo { get; set; }

        public int IdRunner { get; set; }

        public DateTime? DataCreazione { get; set; }

        public string Localita { get; set; }

        public int Tipologia { get; set; }

        public string UriGara { get; set; }
    }
}


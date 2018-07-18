using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class Runner
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Nome { get; set; }

        public string Cognome { get; set; }

        public DateTime DataDiNascita { get; set; }

        public int Sesso { get; set; }

        public string FotoUri { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hurtle.Data.Model
{
    public class Runner
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required]
        public DateTime DataDiNascita { get; set; }

        public int? Sesso { get; set; }

        public string FotoUri { get; set; }
    }
}

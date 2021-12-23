using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BibliotecaIndra.Models.Casas
{
    public class Request
    {
        [Required]
        public List<int> lstCasas { get; set; }
        public int dias { get; set; }
    }
}

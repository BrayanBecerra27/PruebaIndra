using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaIndra.Models.Casas
{
    public class Response
    {
        
        public int dias { get; set; }
        public List<int> entrada { get; set; }
        public List<int> salida { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaIndra.Models.Camion
{
    public class Request
    {
        public List<int> lstPaquetes { get; set; }
        public int tamanioCamion { get;  set; }
    }
}

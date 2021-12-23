using BibliotecaIndra.Models;
using BibliotecaIndra.Models.Casas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RequestCamion = BibliotecaIndra.Models.Camion.Request;
using ResponseCamion = BibliotecaIndra.Models.Camion.Response;

namespace BibliotecaIndra.Implementations
{
    public class GestionIndra
    {
        public static Response CalculoValorCeldas(Request request)
        {
            Response response = new Response();
            response.dias = request.dias;
            response.entrada = request.lstCasas;
            var count = request.lstCasas.Count;
            List<int> listTemporal = request.lstCasas;
            for (int i = 0; i < request.dias; i++)
            {
                List<int> listCalculo = new List<int>();
                for (int j = 0; j < count; j++)
                {
                    var vecinoizquierdo = j == 0 ? 0 : listTemporal[j - 1];
                    var vecinoderecho = j == count -1 ? 0 : listTemporal[j + 1];
                    listCalculo.Add(vecinoizquierdo == vecinoderecho ? 0 : 1);
                }
                listTemporal = listCalculo;
            }
            response.salida = listTemporal;
            return response;
        }

        public static ResponseCamion CalcularPaqueteCamion(RequestCamion request)
        {
            int valorMaxCarga = request.tamanioCamion - Enumeradores.CapacidadReserva;
            List<ResponseCamion> responseList = new List<ResponseCamion>();
            ResponseCamion response = new ResponseCamion();
            var count = request.lstPaquetes.Count;
            for (int i = 0; i < count; i++)
            {
                var countinterno = count - i ;
                for (int j = i + 1 ; j < count; j++)
                {
                    int sumapaquetes = request.lstPaquetes[i] + request.lstPaquetes[j];
                    if (sumapaquetes <= valorMaxCarga)
                    {
                        ResponseCamion responsetemp = new ResponseCamion();
                        responsetemp.lstPaquetes = new List<int> { request.lstPaquetes[i], request.lstPaquetes[j] };
                        responsetemp.Total = sumapaquetes;
                        responseList.Add(responsetemp);
                    }
                }
                
            }
            response = responseList.OrderByDescending(s=>s.Total).FirstOrDefault();
            return response;
        }
    }
}

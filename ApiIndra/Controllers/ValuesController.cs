using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaIndra.Implementations;
using BibliotecaIndra.Models;
using BibliotecaIndra.Models.Casas;
using Microsoft.AspNetCore.Mvc;
using RequestCamion = BibliotecaIndra.Models.Camion.Request;
using ResponseCamion = BibliotecaIndra.Models.Camion.Response;

namespace ApiIndra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        /// <summary>
        /// Método obtiene el estado de las celdas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("EstadoCeldas")]
        public IActionResult OnPostEstadoCeldas(Request request)
        {
            Response response = new Response();
            try
            {
                var message = ValidationRequest(request);
                if(string.IsNullOrEmpty(message))
                {
                    response = GestionIndra.CalculoValorCeldas(request);
                    return BadRequest(response);
                }
                else
                {
                    return BadRequest(message);
                }
            }
            catch(Exception ex )
            {
                return BadRequest(ex.Message);
            }


        }

        private string ValidationRequest(Request request)
        {
            var message = string.Empty;
            List<int> intValidate = new List<int> { 1, 0 };
            if(request.lstCasas.Count != Convert.ToInt32(Enumeradores.NumeroCasas))
            {
                return Enumeradores.NoCumpleLongitud;
            }
            foreach (var item in request.lstCasas)
            {
                if(!intValidate.Contains(item))
                {
                    return Enumeradores.NoCumpleValoresAceptados;
                }
            }
            
            return message;
        }
        /// <summary>
        /// Método calcula par de paquetes que se pueden cargar el camión, respetando el espacio reservado
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CargaCamion")]
        public IActionResult OnPostCargaCamion(RequestCamion request)
        {
            ResponseCamion response = new ResponseCamion();
            try
            {
                response = GestionIndra.CalcularPaqueteCamion(request);
                return BadRequest(response.lstPaquetes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}

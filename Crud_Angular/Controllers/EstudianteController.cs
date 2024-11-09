using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Crud_Angular.Data;
using Crud_Angular.Models;
using Newtonsoft.Json;
namespace Crud_Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly EstudianteData _estudianteData;
        public EstudianteController( EstudianteData estudianteData)
        {
            _estudianteData = estudianteData;
        }

        [HttpGet]
        public async Task <IActionResult> Lista()
        {
            List <Estudiante> Lista = await _estudianteData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Estudiante objeto = await _estudianteData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }


        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Estudiante objeto)
        {
            Console.WriteLine("El objeto recibido: " + JsonConvert.SerializeObject(objeto));
            bool respuesta = await _estudianteData.Crear(objeto);
            Console.WriteLine("Estudiante creado con ID: " + objeto.EstudianteID);
            Console.WriteLine("La respuesta es " + respuesta);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta, EstudianteID = objeto.EstudianteID });
        }



        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Estudiante objeto)
        {
            bool respuesta = await _estudianteData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _estudianteData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}

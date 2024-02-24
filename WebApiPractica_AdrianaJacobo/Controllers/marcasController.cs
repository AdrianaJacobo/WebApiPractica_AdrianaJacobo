using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPractica_AdrianaJacobo.Models;

namespace WebApiPractica_AdrianaJacobo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class marcasController : ControllerBase
    {
        private readonly EquiposContext _EquiposContexto;

        public marcasController(EquiposContext equiposContexto)
        {
            _EquiposContexto = equiposContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Marca> listadoMarca = (from e in _EquiposContexto.Marcas
                                          select e).ToList();

            if (listadoMarca.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoMarca);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            Marca? marca = (from e in _EquiposContexto.Marcas
                              where e.IdMarcas == id
                              select e).FirstOrDefault();

            if (marca == null)
            {
                return NotFound();
            }

            return Ok(marca);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarMarca([FromBody] Marca marca)
        {
            try
            {
                _EquiposContexto.Marcas.Add(marca);
                _EquiposContexto.SaveChanges();
                return Ok(marca);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarMarca(int id, [FromBody] Marca marcaModificar)
        {
            Marca? marcaActual = (from e in _EquiposContexto.Marcas
                                    where e.IdMarcas == id
                                    select e).FirstOrDefault();

            if (marcaActual == null)
            {
                return NotFound();
            }

            marcaActual.NombreMarca = marcaModificar.NombreMarca;
            marcaActual.Estados = marcaModificar.Estados;
            

            _EquiposContexto.Entry(marcaActual).State = EntityState.Modified;
            _EquiposContexto.SaveChanges();

            return Ok(marcaModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarMarca(int id)
        {
            Marca? marca = (from e in _EquiposContexto.Marcas
                              where e.IdMarcas == id
                              select e).FirstOrDefault();

            if (marca == null)
            {
                return NotFound();
            }

            _EquiposContexto.Marcas.Attach(marca);
            _EquiposContexto.Marcas.Remove(marca);
            _EquiposContexto.SaveChanges();

            return Ok(marca);

        }
    }
}

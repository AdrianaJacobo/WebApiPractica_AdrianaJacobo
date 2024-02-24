using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPractica_AdrianaJacobo.Models;

namespace WebApiPractica_AdrianaJacobo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estadosequipoController : ControllerBase
    {
        private readonly EquiposContext _EquiposContexto;

        public estadosequipoController(EquiposContext equiposContexto)
        {
            _EquiposContexto = equiposContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<EstadosEquipo> listadoEstadoEquipos = (from e in _EquiposContexto.EstadosEquipos
                                                        select e).ToList();

            if (listadoEstadoEquipos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoEstadoEquipos);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            EstadosEquipo? estadosEquipo = (from e in _EquiposContexto.EstadosEquipos
                                            where e.IdEstadosEquipo == id
                                            select e).FirstOrDefault();

            if (estadosEquipo == null)
            {
                return NotFound();
            }

            return Ok(estadosEquipo);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarEstadoEquipo ([FromBody] EstadosEquipo estadosEquipo)
        {
            try
            {
                _EquiposContexto.EstadosEquipos.Add(estadosEquipo);
                _EquiposContexto.SaveChanges();
                return Ok(estadosEquipo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEstadoEquipo(int id, [FromBody] EstadosEquipo estadoequipoModificar)
        {
            EstadosEquipo? estadosEquipoActual = (from e in _EquiposContexto.EstadosEquipos
                                    where e.IdEstadosEquipo == id
                                    select e).FirstOrDefault();

            if (estadosEquipoActual == null)
            {
                return NotFound();
            }

            estadosEquipoActual.Descripcion = estadoequipoModificar.Descripcion;
            estadosEquipoActual.Estado = estadoequipoModificar.Estado;
            
            

            _EquiposContexto.Entry(estadosEquipoActual).State = EntityState.Modified;
            _EquiposContexto.SaveChanges();

            return Ok(estadoequipoModificar);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarEstadoEquipo(int id)
        {
            EstadosEquipo? estadosEquipo = (from e in _EquiposContexto.EstadosEquipos
                              where e.IdEstadosEquipo == id
                              select e).FirstOrDefault();

            if (estadosEquipo == null)
            {
                return NotFound();
            }

            _EquiposContexto.EstadosEquipos.Attach(estadosEquipo);
            _EquiposContexto.EstadosEquipos.Remove(estadosEquipo);
            _EquiposContexto.SaveChanges();

            return Ok(estadosEquipo);

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPractica_AdrianaJacobo.Models;

namespace WebApiPractica_AdrianaJacobo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tipoequipoController : ControllerBase
    {
        private readonly EquiposContext _EquiposContexto;

        public tipoequipoController(EquiposContext equiposContexto)
        {
            _EquiposContexto = equiposContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<TipoEquipo> listadoTipoEquipo = (from e in _EquiposContexto.TipoEquipos
                                          select e).ToList();

            if (listadoTipoEquipo.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoTipoEquipo);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            TipoEquipo? tipoEquipo = (from e in _EquiposContexto.TipoEquipos
                              where e.IdTipoEquipo == id
                              select e).FirstOrDefault();

            if (tipoEquipo == null)
            {
                return NotFound();
            }

            return Ok(tipoEquipo);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarTipoEquipo([FromBody] TipoEquipo tipoEquipo)
        {
            try
            {
                _EquiposContexto.TipoEquipos.Add(tipoEquipo);
                _EquiposContexto.SaveChanges();
                return Ok(tipoEquipo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarTipoEquipo(int id, [FromBody] TipoEquipo tipoequipoModificar)
        {
            TipoEquipo? tipoequipoActual = (from e in _EquiposContexto.TipoEquipos
                                    where e.IdTipoEquipo == id
                                    select e).FirstOrDefault();

            if (tipoequipoActual == null)
            {
                return NotFound();
            }

            tipoequipoActual.Descripcion = tipoequipoModificar.Descripcion;
            tipoequipoActual.Estado = tipoequipoModificar.Estado;


            _EquiposContexto.Entry(tipoequipoActual).State = EntityState.Modified;
            _EquiposContexto.SaveChanges();

            return Ok(tipoequipoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarTipoEquipo(int id)
        {
            TipoEquipo? tipoEquipo = (from e in _EquiposContexto.TipoEquipos
                              where e.IdTipoEquipo == id
                              select e).FirstOrDefault();

            if (tipoEquipo == null)
            {
                return NotFound();
            }

            _EquiposContexto.TipoEquipos.Attach(tipoEquipo);
            _EquiposContexto.TipoEquipos.Remove(tipoEquipo);
            _EquiposContexto.SaveChanges();

            return Ok(tipoEquipo);

        }


    }
}

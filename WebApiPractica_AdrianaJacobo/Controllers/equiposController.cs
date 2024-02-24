using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPractica_AdrianaJacobo.Models;


namespace WebApiPractica_AdrianaJacobo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly EquiposContext _EquiposContexto;

        public equiposController(EquiposContext equiposContexto)
        {
            _EquiposContexto = equiposContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Equipo> listadoEquipo = (from e in _EquiposContexto.Equipos
                                          select e).ToList();

            if (listadoEquipo.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoEquipo);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            Equipo? equipo = (from e in _EquiposContexto.Equipos
                              where e.IdEquipos == id
                              select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }

            return Ok(equipo);
        }

        [HttpGet]
        [Route("Fin/{filtro}")]

        public IActionResult FinByDescription(string filtro)
        {
            Equipo? equipo = (from e in _EquiposContexto.Equipos
                              where e.Descripcion.Contains(filtro)
                              select e).FirstOrDefault();


            if (equipo == null)
            {
                return NotFound();
            }

            return Ok(equipo);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarEquipo([FromBody] Equipo equipo)
        {
            try
            {
                _EquiposContexto.Equipos.Add(equipo);
                _EquiposContexto.SaveChanges();
                return Ok(equipo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEquipo(int id, [FromBody] Equipo equipoModificar)
        {
            Equipo? equipoActual = (from e in _EquiposContexto.Equipos
                                    where e.IdEquipos == id
                                    select e).FirstOrDefault();

            if (equipoActual == null)
            {
                return NotFound();
            }

            equipoActual.Nombre = equipoModificar.Nombre;
            equipoActual.Descripcion = equipoModificar.Descripcion;
            equipoActual.MarcaId = equipoModificar.MarcaId;
            equipoActual.TipoEquipoId = equipoModificar.TipoEquipoId;
            equipoActual.AnioCompra = equipoModificar.AnioCompra;
            equipoActual.Costo = equipoModificar.Costo;

            _EquiposContexto.Entry(equipoActual).State = EntityState.Modified;
            _EquiposContexto.SaveChanges();

            return Ok(equipoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarEquipo(int id)
        {
            Equipo? equipo = (from e in _EquiposContexto.Equipos
                              where e.IdEquipos == id
                              select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }

            _EquiposContexto.Equipos.Attach(equipo);
            _EquiposContexto.Equipos.Remove(equipo);
            _EquiposContexto.SaveChanges();

            return Ok(equipo);

        }
    }
}
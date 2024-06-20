using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using api_depormallas.Models;
using Npgsql;
using Dapper;

namespace api_depormallas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServicioController : ControllerBase
    {
        private readonly NpgsqlConnection _connection;

        public ServicioController(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet(Name = "GetServicios")]
        public IEnumerable<Servicio> Get()
        {
            var servicios = _connection.Query<Servicio>("SELECT * FROM servicio");
            return servicios;
        }

        [HttpPost(Name = "PostServicio")]
        public IActionResult Post([FromBody] Servicio servicio)
        {
            if (servicio == null)
            {
                return BadRequest("El objeto Servicio no puede ser nulo");
            }

            // Insertar el agricultor en la base de datos
            var query = "INSERT INTO servicio (nombre, descripcion, esinstalacion, esmantenimiento) " +
                        "VALUES (@Nombre, @Descripcion, @Esinstalacion, @Esmantenimiento)";

            _connection.Execute(query, servicio);

            // Devolver una respuesta exitosa
            return Ok("Servicio creado exitosamente");
        }

        [HttpPut("{id}", Name = "UpdateServicio")]
        public IActionResult Put(int id, [FromBody] Servicio servicio)
        {
            if (servicio == null)
            {
                return BadRequest("El objeto Servicio no puede ser nulo");
            }

            // Verificar si el agricultor con el ID proporcionado existe
            var existingServicio = _connection.QuerySingleOrDefault<Servicio>("SELECT * FROM servicio WHERE id = @Id", new { Id = id });

            if (existingServicio == null)
            {
                return NotFound($"Servicio con ID {id} no encontrado");
            }

            // Actualizar los datos del agricultor existente con los nuevos datos
            var query = "UPDATE servicio SET nombre = @Nombre, descripcion = @Descripcion, esinstalacion = @Esinstalacion, esmantenimiento = @Esmantenimiento WHERE id = @Id";

            _connection.Execute(query, new
            {
                Id = id,
                servicio.Nombre,
                servicio.Descripcion, 
                servicio.Esinstalacion, 
                servicio.Esmantenimiento,
            });

            // Devolver una respuesta exitosa
            return Ok($"Servicio con ID {id} actualizado exitosamente");
        }

        //[HttpDelete("{id}", Name = "DeleteServicio")]
        //public IActionResult Delete(int id)
        //{
        //    // Verificar si el agricultor con el ID proporcionado existe
        //    var existingTelefono = _connection.QuerySingleOrDefault<Telefono>("SELECT * FROM telefono WHERE Id = @Id", new { Id = id });

        //    if (existingTelefono == null)
        //    {
        //        return NotFound($"Agricultor con ID {id} no encontrado");
        //    }

        //    // Eliminar el agricultor de la base de datos
        //    var query = "DELETE FROM telefono WHERE Id = @Id";
        //    _connection.Execute(query, new { Id = id });

        //    // Devolver una respuesta exitosa
        //    return Ok($"Telefono con ID {id} eliminado exitosamente");
        //}


    }
}
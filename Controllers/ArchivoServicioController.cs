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
    public class ArchivoServicioController : ControllerBase
    {
        private readonly NpgsqlConnection _connection;

        public ArchivoServicioController(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet(Name = "GetArchivosServicios")]
        public IEnumerable<ArchivoServicio> Get()
        {
            var archivos = _connection.Query<ArchivoServicio>("SELECT * FROM archivoservicio");
            return archivos;
        }

        [HttpPost(Name = "PostArchivoServicio")]
        public IActionResult Post([FromBody] ArchivoServicio archivo)
        {
            if (archivo == null)
            {
                return BadRequest("El objeto ArchivoServicio no puede ser nulo");
            }

            // Insertar el agricultor en la base de datos
            var query = "INSERT INTO archivoservicio (descripcion, url, fechacargue) " +
                        "VALUES (@Descripcion, @Url, @Fechacargue)";

            _connection.Execute(query, archivo);

            // Devolver una respuesta exitosa
            return Ok("ArchivoServicio creado exitosamente");
        }

        [HttpPut("{id}", Name = "UpdateArchivoServicio")]
        public IActionResult Put(int id, [FromBody] ArchivoServicio archivo)
        {
            if (archivo  == null)
            {
                return BadRequest("El objeto ArchivoServicio no puede ser nulo");
            }

            // Verificar si el agricultor con el ID proporcionado existe
            var existingArchivo = _connection.QuerySingleOrDefault<ArchivoServicio>("SELECT * FROM archivoservicio WHERE id = @Id", new { Id = id });

            if (existingArchivo == null)
            {
                return NotFound($"ArchivoServicio con ID {id} no encontrado");
            }

            // Actualizar los datos del agricultor existente con los nuevos datos
            var query = "UPDATE archivoservicio SET descripcion = @Descripcion, url = @Url, fechacargue = @Fechacargue WHERE id = @Id";

            _connection.Execute(query, new
            {
                Id = id,
                archivo.Descripcion, 
                archivo.Url,
                archivo.Fechacargue
            });

            // Devolver una respuesta exitosa
            return Ok($"ArchivoServicio con ID {id} actualizado exitosamente");
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
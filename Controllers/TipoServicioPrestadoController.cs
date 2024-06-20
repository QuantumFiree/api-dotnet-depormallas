using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;
using api_depormallas.Models;

namespace api_depormallas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoServicioPrestadoController : ControllerBase
    {
        private readonly NpgsqlConnection _connection;

        public TipoServicioPrestadoController(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet(Name = "GetTipoServicios")]
        public IEnumerable<TipoServicioPrestado> Get()
        {
            var tiposervicios = _connection.Query<TipoServicioPrestado>("SELECT * FROM tiposervicioprestado");
            return tiposervicios;
        }

        //[HttpPost(Name = "PostTipoServicio")]
        //public IActionResult Post([FromBody] TipoServicioPrestado tiposervicio)
        //{
        //    try
        //    {
        //        if (tiposervicio == null)
        //        {
        //            return BadRequest("El objeto orden no puede ser nulo");
        //        }

        //        // Insertar el agricultor en la base de datos
        //        var query = "INSERT INTO tiposervicioprestado (nombre, descripcion) " +
        //                    "VALUES (@Nombre, @Descripcion)";

        //        _connection.Execute(query, TipoServicioPrestado);

        //        // Devolver una respuesta exitosa
        //        return Ok("Tipo Servicio creado exitosamente");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar el error y devolver una respuesta adecuada
        //        Console.WriteLine($"Se produjo un error al procesar la solicitud: {ex.Message}");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Se produjo un error interno en el servidor");
        //    }
        //}


        [HttpPut("{id}", Name = "UpdateTipoServicio")]
        public IActionResult Put(int id, [FromBody] TipoServicioPrestado tiposervicio)
        {
            if (tiposervicio == null)
            {
                return BadRequest("El objeto Orden no puede ser nulo");
            }

            // Verificar si el agricultor con el ID proporcionado existe
            var existingOrden = _connection.QuerySingleOrDefault<TipoServicioPrestado>("SELECT * FROM tiposervicioprestado WHERE id = @Id", new { Id = id });

            if (existingOrden == null)
            {
                return NotFound($"Tipo Servicio con ID {id} no encontrado");
            }

            // Actualizar los datos del agricultor existente con los nuevos datos
            var query = "UPDATE tiposervicioprestado SET nombre = @Nombre, descripcion = @Descripcion WHERE id = @Id";

            _connection.Execute(query, new
            {
                Id = id,
                tiposervicio.Nombre,
                tiposervicio.Descripcion,
            });

            // Devolver una respuesta exitosa
            return Ok($"Orden con ID {id} actualizado exitosamente");
        }

        //[HttpDelete("{id}", Name = "DeleteOrdenes")]
        //public IActionResult Delete(int id)
        //{
        //    // Verificar si el agricultor con el ID proporcionado existe
        //    var existingOrden = _connection.QuerySingleOrDefault<Ordenes>("SELECT * FROM ordenes WHERE Id = @Id", new { Id = id });

        //    if (existingOrden == null)
        //    {
        //        return NotFound($"Orden con ID {id} no encontrado");
        //    }

        //    // Eliminar el agricultor de la base de datos
        //    var query = "DELETE FROM ordenes WHERE Id = @Id";
        //    _connection.Execute(query, new { Id = id });

        //    // Devolver una respuesta exitosa
        //    return Ok($"Orden con ID {id} eliminado exitosamente");
        //}


    }
}
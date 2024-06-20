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
    public class ServicioPrestadoController : ControllerBase
    {
        private readonly NpgsqlConnection _connection;

        public ServicioPrestadoController(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet(Name = "GetServiciosPrestados")]
        public IEnumerable<ServicioPrestado> Get()
        {
            var serviciosprestados = _connection.Query<ServicioPrestado>("SELECT * FROM servicioprestado");
            return serviciosprestados;
        }

        [HttpPost(Name = "PostServicioPrestado")]
        public IActionResult Post([FromBody] ServicioPrestado servicio)
        {
            if (servicio == null)
            {
                return BadRequest("El objeto Servicio no puede ser nulo");
            }

            // Insertar el agricultor en la base de datos
            var query = "INSERT INTO servicioprestado (idtiposervicio, idclienteservicio, valortotal, numerodepagos, valorpendiente, fechainicio, fechafin, fechafinestimada, idestado) " +
                        "VALUES (@Idtiposervicio, @Idclienteservicio, @Valortotal, @Numerodepagos, @Valorpendiente, @Fechainicio," +
                        "@Fechafin, @Fechaestimada, @Idestado)";

            _connection.Execute(query, servicio);

            // Devolver una respuesta exitosa
            return Ok("Servicio creado exitosamente");
        }

        [HttpPut("{id}", Name = "UpdateServicioPrestado")]
        public IActionResult Put(int id, [FromBody] ServicioPrestado servicio)
        {
            if (servicio == null)
            {
                return BadRequest("El objeto Servicio no puede ser nulo");
            }

            // Verificar si el agricultor con el ID proporcionado existe
            var existingTelefono = _connection.QuerySingleOrDefault<ServicioPrestado>("SELECT * FROM servicioprestado WHERE id = @Id", new { Id = id });

            if (existingTelefono == null)
            {
                return NotFound($"Servicio con ID {id} no encontrado");
            }

            // Actualizar los datos del agricultor existente con los nuevos datos
            var query = "UPDATE servicioprestado SET idtiposervicio = @Idtiposervicio, idclienteservicio = @Idclienteservicio, valortotal = @Valortotal, " +
                        "numerodepagos = @Numerodepagos, valorpendiente = @Valorpendiente, fechainicio = @Fechainicio, fechafin = @Fechafin," +
                        "fechafinestimada = @Fechafinestimada, idestado = @Idestado WHERE id = @Id";

            _connection.Execute(query, new
            {
                Id = id,
                servicio.Idtiposervicio,
                servicio.Idclienteservicio, 
                servicio.Valortotal, 
                servicio.Numerodepagos,
                servicio.Valorpendiente, 
                servicio.Fechainicio,
                servicio.Fechafin,
                servicio.Fechafinestimada,
                servicio.Idestado,
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
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
    public class ProductoController : ControllerBase
    {
        private readonly NpgsqlConnection _connection;

        public ProductoController(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet(Name = "GetProductos")]
        public IEnumerable<Producto> Get()
        {
            var productos = _connection.Query<Producto>("SELECT * FROM producto");
            return productos;
        }

        [HttpPost(Name = "PostProducto")]
        public IActionResult Post([FromBody] Producto producto)
        {
            if (producto == null)
            {
                return BadRequest("El objeto Producto no puede ser nulo");
            }

            // Insertar el agricultor en la base de datos
            var query = "INSERT INTO producto (nombre, descripcion, esmetroslineales, esmetroscuadrados, esgramos, eskilogramos, estoneladas, valorporunidad) " +
                        "VALUES (@Nombre, @Descripcion, @Esmetroslineales, @Esmetroscuadrados, @Esgramos, @Eskilogramos, @Estoneladas, @Valorporunidad)";

            _connection.Execute(query, producto);

            // Devolver una respuesta exitosa
            return Ok("Producto creado exitosamente");
        }

        [HttpPut("{id}", Name = "UpdateProducto")]
        public IActionResult Put(int id, [FromBody] Producto producto)
        {
            if (producto == null)
            {
                return BadRequest("El objeto Producto no puede ser nulo");
            }

            // Verificar si el agricultor con el ID proporcionado existe
            var existingTelefono = _connection.QuerySingleOrDefault<Producto>("SELECT * FROM producto WHERE id = @Id", new { Id = id });

            if (existingTelefono == null)
            {
                return NotFound($"Producto con ID {id} no encontrado");
            }

            // Actualizar los datos del agricultor existente con los nuevos datos
            var query = "UPDATE producto SET nombre = @Nombre, descripcion = @Descripcion, esmetroslineales = @Esmetroslineales, " +
                        "esmetroscuadrados = @Esmetroscuadrados, esgramos = @Esgramos, eskilogramos = @Eskilogramos, estoneladas = @Estoneladas," +
                        "valorporunidad = @Valorporunidad WHERE id = @Id";

            _connection.Execute(query, new
            {
                Id = id,
                producto.Nombre,
                producto.Descripcion,
                producto.Esmetroslineales,
                producto.Esmetroscuadrados,
                producto.Esgramos,
                producto.Eskilogramos,
                producto.Estoneladas,
                producto.Valorporunidad
            });

            // Devolver una respuesta exitosa
            return Ok($"Producto con ID {id} actualizado exitosamente");
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
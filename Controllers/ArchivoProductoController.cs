using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using api_depormallas.Models;
using Npgsql;
using Dapper;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace api_depormallas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ArchivoProductoController : ControllerBase
    {
        private readonly NpgsqlConnection _connection;

        public ArchivoProductoController(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet(Name = "GetArchivosProducto")]
        public IEnumerable<ArchivoProducto> Get()
        {
            var productos = _connection.Query<ArchivoProducto>("SELECT * FROM archivoproducto");
            return productos;
        }

        [HttpGet("GetArchivosPorIdProducto")]
        public IEnumerable<ArchivoProducto> GetArchivosPorIdProducto([FromQuery] int idproducto)
        {
            var productos = _connection.Query<ArchivoProducto>("SELECT * FROM archivoproducto WHERE idproducto = @Idproducto", new { Idproducto = idproducto});
            return productos;
        }

        [HttpPost("GuardarArchivo")]
        public IActionResult GuardarArchivo([FromForm] int idproducto, [FromForm] string descripcion, [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("El archivo no puede ser nulo o vacío");
            }

            try
            {
                // Define la ruta donde quieres guardar el archivo
                var uploadsFolderPath = Path.Combine("C:\\Users\\Udenar\\Documents\\Universidad\\Metricas\\WEB\\webapp-depormallas\\webapp-depormallas\\", "uploads");

                // Crea el directorio si no existe
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                // Define la ruta completa del archivo
                var filePath = Path.Combine(uploadsFolderPath, file.FileName);

                // Guarda el archivo en la ubicación especificada
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Si necesitas insertar datos en la base de datos, puedes hacerlo aquí
                var filePathGuardar = Path.Combine(@"\uploads", file.FileName);
                var archivo = new ArchivoProducto
                {
                    Idproducto = idproducto,
                    Descripcion = descripcion,
                    Url = filePathGuardar, // o la URL relativa si prefieres
                    Fechacargue = DateTime.UtcNow
                };

                var query = "INSERT INTO archivoproducto (idproducto, descripcion, url, fechacargue) " +
                            "VALUES (@Idproducto, @Descripcion, @Url, @Fechacargue)";
                _connection.Execute(query, archivo);

                return Ok(new { message = $"Archivo creado exitosamente con id {idproducto} y descripcion {descripcion}", url = filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al guardar el archivo: {ex.Message}");
            }
        }


        [HttpPost(Name = "PostArchivoProducto")]
        public IActionResult Post([FromBody] string idproducto)
        {
            //if (archivo == null)
            //{
            //    return BadRequest("El objeto Archivo no puede ser nulo");
            //}

            //// Insertar el agricultor en la base de datos
            //var query = "INSERT INTO archivoproducto (idproducto, descripcion, url, fechacargue) " +
            //            "VALUES (@Idproducto, @Descripcion, @Url, @Fechacargue)";

            //_connection.Execute(query, archivo);

            // Devolver una respuesta exitosa
            return Ok("Archivo creado exitosamente");
        }

        

        [HttpPut("{id}", Name = "UpdateArchivoProducto")]
        public IActionResult Put(int id, [FromBody] ArchivoProducto archivo)
        {
            if (archivo == null)
            {
                return BadRequest("El objeto Archivo no puede ser nulo");
            }

            // Verificar si el agricultor con el ID proporcionado existe
            var existingArchivo = _connection.QuerySingleOrDefault<ArchivoProducto>("SELECT * FROM archivoproducto WHERE id = @Id", new { Id = id });

            if (existingArchivo == null)
            {
                return NotFound($"Archivo con ID {id} no encontrado");
            }

            // Actualizar los datos del agricultor existente con los nuevos datos
            var query = "UPDATE archivoproducto SET idproducto = @Idproducto, descripcion = @Descripcion, url = @Url, fechacargue = @Fechacargue WHERE id = @Id";

            _connection.Execute(query, new
            {
                Id = id,
                archivo.Idproducto,
                archivo.Descripcion,
                archivo.Url,
                archivo.Fechacargue,
            });

            // Devolver una respuesta exitosa
            return Ok($"Archivo con ID {id} actualizado exitosamente");
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api_depormallas.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using api_depormallas.Controllers;
using Npgsql;
using Dapper;
namespace api_depormallas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string? SecretKey;
        private readonly NpgsqlConnection _connection;
        public AutenticacionController(IConfiguration config, NpgsqlConnection connection)
        {
            _connection = connection;
            SecretKey = config.GetSection("Settings").GetSection("SecretKey").ToString() ?? "vacio";
        }

        [HttpPost]
        [Route("Validar")]
        public IActionResult Validar([FromBody] Usuario usuario)
        {
            try
            {
                var usuarios = _connection.Query<Usuario>("SELECT * FROM usuario WHERE correo = @Correo", new { Correo = usuario.Correo });
                Usuario usuarioRes = usuarios.FirstOrDefault();
                //var usuarios = _connection.Query<Usuario>("SELECT * FROM usuario WHERE correo = @Correo", new { Correo = usuario.Correo });
                DateTime tokenExpira = DateTime.UtcNow.AddMinutes(5);
                if (usuarioRes != null && usuario.Correo == usuarioRes.Correo && usuario.Clave == usuarioRes.Clave)
                {
                    var keyBytes = Encoding.ASCII.GetBytes(SecretKey);
                    var claims = new ClaimsIdentity();

                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Correo));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = tokenExpira,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                    string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                    var infoUser = new
                    {
                        Nombres = usuarioRes.Nombres,
                        Apellidos = usuarioRes.Apellidos,
                        Telefono = usuarioRes.Telefono,
                        Correo = usuarioRes.Correo,
                        Clave = usuarioRes.Clave,
                        Rol = usuarioRes.Rol
                    };
                    return StatusCode(StatusCodes.Status200OK, new { token = tokenCreado, expira = tokenExpira.ToString("yyyy-MM-dd hh:mm:ss"), usuario = usuarioRes });
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new { messageError = "Credenciales incorrectas." });
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}

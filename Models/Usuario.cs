namespace api_depormallas.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Correo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string? Nombreempresa { get; set; }
        public int Telefono { get; set; }
        public int? Rol { get; set; }

        public Usuario() // Constructor por defecto requerido para Dapper
        {
            Id = 0;
            Clave = "";
            Correo = "";
            Nombres = "";
            Apellidos = "";
            Nombreempresa = "";
            Telefono = 0;
            Rol = 0;
        }

        public Usuario(int id, string correo, string clave, string nombres, string apellidos, string nombreempresa, int telefono, int? rol)
        {
            Id = id;
            Correo = correo;
            Clave = clave;
            Nombres = nombres;
            Apellidos = apellidos;
            Nombreempresa = nombreempresa;
            Telefono = telefono;
            Rol = rol;
        }
    }
}

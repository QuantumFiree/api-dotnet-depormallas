using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace api_depormallas.Models
{
    public class Servicio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Esinstalacion { get; set; }
        public bool Esmantenimiento { get; set; }

        public Servicio() // Constructor por defecto requerido para Dapper
        {
            Id = 0;
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Esinstalacion = false;
            Esmantenimiento = false;
        }
        public Servicio(int id, string nombre, string descripcion, bool esinstalacion, bool esmantenimiento)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Esinstalacion = esinstalacion;
            Esmantenimiento = esmantenimiento;
        }
    }
}

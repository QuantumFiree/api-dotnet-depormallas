using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_depormallas.Models
{
    public class InfoNegocio
    {
        public int Id { get; set; }
        public string Nombre {  get; set; }
        public int Nit {  get; set; }
        public string Direccion {  get; set; }
        public int Telefono {  get; set; }
        public string Urlinstagram {  get; set; }
        public string Urlfacebook { get; set; }
        public string Nombrerepresentante {  get; set; }

        public InfoNegocio() // Constructor por defecto requerido para Dapper
        {
            Nombre = "";
            Nit = 0;
            Direccion = "";
            Telefono = 0;
            Urlinstagram = "";
            Urlfacebook = "";
            Nombrerepresentante = "";
        }

        public InfoNegocio(int id, string nombre, int nit, string direccion, int telefono, string urlinstagram, string urlfacebook, string nombrerepresentante)
        {
            Id = id;
            Nombre = nombre;
            Nit = nit;
            Direccion = direccion;
            Telefono = telefono;
            Urlinstagram = urlinstagram;
            Urlfacebook = urlfacebook;
            Nombrerepresentante = nombrerepresentante;
        }
    }
}

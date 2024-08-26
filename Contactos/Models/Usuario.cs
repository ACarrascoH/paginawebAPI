using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Contactos.Models
{
    public class Usuario
    {
        [Key]
        //[JsonIgnore] por si no se quiere q aparesca en los resultados de json en swagger o postman
        public int UsuarioPK { get; set; }
        [Required]
        [MaxLength(10)]
        public string? Rut { get; set; }
        [Required]
        //[EmailAddress] si fuera un email
        [MaxLength(120)]
        public string? Nombre { get; set; }

        public int Edad { get; set; }
        


    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaBiomedico.Models
{
    public class Rol
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRol { get; set; }
        [Required, StringLength(100)]
        public string Nombre { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}

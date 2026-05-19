using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaBiomedico.Models
{
    public class Equipo
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEquipo { get; set; }
        [Required, StringLength(100)]
        public string NameEquipo { get; set; }
        [Required, StringLength(100)]
        public string Categoria { get; set; }
        [Required, StringLength(100)]
        public string Ubicacion { get; set; }
        [Required, StringLength(100)]
        public string Estado { get; set; }
        public DateOnly FechaRegistro { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}

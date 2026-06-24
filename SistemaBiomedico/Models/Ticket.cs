using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaBiomedico.Models
{
    public class Ticket
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTicket { get; set; }
        [Required, StringLength(100)]
        public string Descripcion { get; set; }

        [Required]
        public DateOnly FechaCreacion { get; set; }
        [Required, StringLength(50)]
        public string Estado { get; set; }
        [Required, StringLength(50)]
        public string Prioridad { get; set; }
        public int IdEquipo { get; set; }
        public Equipo Equipo { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}

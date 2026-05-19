using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaBiomedico.Models
{
    public class RegistroInter
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRegistroInter { get; set; }
        [Required,StringLength(100)]
        public string Observacion { get; set; }
        [Required]
        public DateTime FechaIntervencion { get; set; }
        [Required, MaxLength(10)]
        public int TecnicoId { get; set; }
        public Usuario Tecnico { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}

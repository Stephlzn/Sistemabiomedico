using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaBiomedico.Models
{
    public class Ticket
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTicket { get; set; }
        [Required, StringLength(200)]
        public string TipoFalla { get; set; }
        [Required, StringLength(1000)]
        public string Descripcion { get; set; }
        [Required, StringLength(200)]
        public string Urgencia { get; set; }
        [Required, StringLength(200)]
        public string Estado { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public DateTime? FechaCierre { get; set; }

        public int EquipoId { get; set; }
        public Equipo Equipo { get; set; }
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<RegistroInter> RegistrosInter { get; set; }
    }
}

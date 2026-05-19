using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaBiomedico.Models
{
    public class Usuario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }
        [Required, StringLength(100)]
        public string Nombre { get; set; }
        [Required, StringLength(100)]    
        public string Apellido { get; set; }
        [Required, StringLength(100)]   
        public string Email { get; set; }
        [Required, StringLength(200)]
        public string Password { get; set; }

        public int RolId { get; set; }
        public Rol Roles { get; set; }

        public ICollection<Ticket> TicketsReportados { get; set; }
        public ICollection<RegistroInter> Intervenciones { get; set; }

    }
}


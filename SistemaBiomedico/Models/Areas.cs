using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaBiomedico.Models
{
    public class Areas
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAreas { get; set; }
        [Required, StringLength(100)]
        public string NombreArea { get; set; }
        public ICollection<Equipo> Equipos { get; set; }
    }
}


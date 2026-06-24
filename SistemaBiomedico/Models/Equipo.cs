using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaBiomedico.Models
{
    public class Equipo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEquipo { get; set; }
        [Required, StringLength(100)]
        public string NombreEquipo { get; set; }
        [Required, StringLength(100)]
        public string CategoriaEquipo { get; set; }
        [Required, StringLength(100)]
        public string MarcaEquipo { get; set; }
        [Required, StringLength(100)]
        public string ModeloEquipo { get; set; }

        public int IdAreas { get; set; }
        public Areas Areas { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
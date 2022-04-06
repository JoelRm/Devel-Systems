using System.ComponentModel.DataAnnotations;

namespace DevelSystem.Entities
{
    public class EncuestaCab
    {
        [Key]
        public int IdEncuesta { get; set; }
        public string NombreEncuesta { get; set; }
        public string DescripcionEncuesta { get; set; }
        public bool Estado { get; set; }
    }
}

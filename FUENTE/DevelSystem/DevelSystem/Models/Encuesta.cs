using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevelSystem.Models
{
    public class Encuesta
    {
        public int IdEncuesta { get; set; }
        [Required]
        public string NombreEncuesta { get; set; }
        [Required]
        public string DescripcionEncuesta { get; set; }
        [Required]
        public bool Estado { get; set; }

        public List<DetalleEncuesta> Detalle { get; set; }    
    }
    public class DetalleEncuesta
    {
        public int IdEncuestaDetalle { get; set; }
        [Required]
        public int IdEncuesta { get; set; }
        [Required]
        public string NombreCampo { get; set; }
        [Required]
        public string TituloCampo { get; set; }
        [Required]
        public string EsRequerido { get; set; }
        [Required]
        public string TipoCampo { get; set; }

    }

}

using System.ComponentModel.DataAnnotations;

namespace DevelSystem.Entities
{
    public class EncuestaDet
    {
        [Key]
        public int IdEncuestaDetalle { get; set; }
        public int IdEncuesta { get; set; }
        public string NombreCampo { get; set; }
        public string TituloCampo { get; set; }
        public string EsRequerido { get; set; }
        public string TipoCampo { get; set; }
    }
}

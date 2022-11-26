using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models.DTO
{
    public class NoticiaUpdateDTO
    {
        public string tituloNoticia { get; set; } = null!;
        [Column("contenidoNoticia")]
        public string contenidoNoticia { get; set; } = null!;
        [Column("fechaNoticia", TypeName = "datetime")]
        public DateTime fechaNoticia { get; set; }
        [Column("foto")]
        public string? Foto { get; set; }
        public IFormFile? FotoFile { get; set; }
        [Column("estatusNoticia")]
        public bool noticiaStatus { get; set; }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models.DTO
{
    public class NoticiaUpdateDTO
    {
        [Key]
        [Column("idNoticia")]
        public int IdNoticia { get; set; }
        [Column("idUsuario")]
        public int IdUsuario { get; set; }
        [Column("tituloNoticia")]
        [StringLength(50)]
        public string TituloNoticia { get; set; } = null!;
        [Column("contenidoNoticia")]
        public string ContenidoNoticia { get; set; } = null!;
        [Column("noticiaStatus")]
        public bool NoticiaStatus { get; set; }
        [Column("fechaNoticia", TypeName = "datetime")]
        public DateTime FechaNoticia { get; set; }
        [Column("foto")]
        public string? Foto { get; set; }
    }
}

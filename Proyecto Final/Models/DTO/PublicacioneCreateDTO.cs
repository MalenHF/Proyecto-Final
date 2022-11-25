using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models.DTO
{
    public class PublicacioneCreateDTO
    {
        [Column("publicacionID")]
        public int PublicacionId { get; set; }
        [Column("idUsuario")]
        public int IdUsuario { get; set; }
        [Column("fotoPath")]
        public string FotoPath { get; set; } = null!;
        [Column("estatus")]
        public bool Estatus { get; set; }
        [Column("titulo")]
        [StringLength(50)]
        public string Titulo { get; set; } = null!;
        [Column("fechaPublicacion", TypeName = "datetime")]
        public DateTime FechaPublicacion { get; set; }
    }
}

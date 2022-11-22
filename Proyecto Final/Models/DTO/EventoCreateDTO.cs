using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models.DTO
{
    public class EventoCreateDTO
    {
        [Column("idUsuario")]
        public int IdUsuario { get; set; }
        [Column("tituloEvento")]
        [StringLength(50)]
        public string TituloEvento { get; set; } = null!;
        [Column("contenidoEvento")]
        public string ContenidoEvento { get; set; } = null!;
        [Column("estatusEvento")]
        public bool EstatusEvento { get; set; }
        [Column("fechaEvento", TypeName = "datetime")]
        public DateTime FechaEvento { get; set; }
        [Column("foto")]
        public string? Foto { get; set; }
        public IFormFile? FotoFile { get; set; }
    }
}

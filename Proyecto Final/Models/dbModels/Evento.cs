using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Final.Models.dbModels
{
    public partial class Evento
    {
        [Key]
        [Column("idEvento")]
        public int IdEvento { get; set; }
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

        [ForeignKey("IdUsuario")]
        [InverseProperty("Eventos")]
        public virtual AplicationUser IdUsuarioNavigation { get; set; } = null!;
    }
}

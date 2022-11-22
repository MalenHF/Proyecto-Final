using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Final.Models.dbModels
{
    public partial class Noticia
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

        [ForeignKey("IdUsuario")]
        [InverseProperty("Noticia")]
        public virtual AplicationUser IdUsuarioNavigation { get; set; } = null!;
    }
}

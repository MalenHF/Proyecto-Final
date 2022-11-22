using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Final.Models.dbModels
{
    public partial class Publicacione
    {
        [Key]
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

        [ForeignKey("IdUsuario")]
        [InverseProperty("Publicaciones")]
        public virtual AplicationUser IdUsuarioNavigation { get; set; } = null!;
    }
}

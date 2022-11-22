using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Final.Models.dbModels
{
    
    public partial class AplicationUser : IdentityUser<int>
    {
        public AplicationUser()
        {
            Eventos = new HashSet<Evento>();
            Noticia = new HashSet<Noticia>();
            Publicaciones = new HashSet<Publicacione>();
        }

        
        [Column("nombre")]
        [StringLength(50)]
        [Unicode(false)]
        public string Nombre { get; set; } = null!;
        
       
        [Column("fechaCUsuario", TypeName = "datetime")]
        public DateTime FechaCusuario { get; set; }

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Evento> Eventos { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Noticia> Noticia { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Publicacione> Publicaciones { get; set; }
    }
}

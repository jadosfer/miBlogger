using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WebBlog.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Email { get; set; }
       

        [Required (ErrorMessage = "El {0} es obligatorio")]
        public string Titulo { get; set; }
        [StringLength(20, MinimumLength = 4, ErrorMessage = "El {0} debe tener una logitud minima de {2} y máxima de {1}")]
        public string Genero { get; set; }
        public DateTime Fecha{ get; set; }
        public string Texto { get; set; }

    }  
}


//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace videoclub_project.Backend.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using videoclub_project.MVVM;

    public partial class productos : PropertyChangedDataError {
        public const int PELICULA = 1;
        public const int JUEGO = 2;

        public int idProductos { get; set; }

        [Required(ErrorMessage = "Titulo obligatorio")]
        public string titulo { get; set; }

        [Required(ErrorMessage = "Fecha obligatorio")]
        public Nullable<System.DateTime> fecha { get; set; }
        public string portada { get; set; }
        public int nota { get; set; }
        public string edad_nota { get; set; }
        public int id_genero { get; set; }
        public int id_idioma { get; set; }
        public int id_idiomaDoblado { get; set; }


        [Required(ErrorMessage = "Genero obligatorio")]
        public virtual generos generos { get; set; }

        [Required(ErrorMessage = "Idioma obligatorio")]
        public virtual idiomas idiomas { get; set; }

        [Required(ErrorMessage = "Idioma Doblado obligatorio")]
        public virtual idiomas idiomas1 { get; set; }
        public virtual peliculas peliculas { get; set; }
        public virtual videojuegos videojuegos { get; set; }

        public override string ToString() {
            return titulo;
        }
    }
}

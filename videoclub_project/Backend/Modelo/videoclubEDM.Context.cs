﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class videoclubEntities : DbContext
    {
        public videoclubEntities()
            : base("name=videoclubEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<actores_peliculas> actores_peliculas { get; set; }
        public virtual DbSet<alquileres> alquileres { get; set; }
        public virtual DbSet<cliente> cliente { get; set; }
        public virtual DbSet<empleado> empleado { get; set; }
        public virtual DbSet<formatos> formatos { get; set; }
        public virtual DbSet<formatos_peliculas> formatos_peliculas { get; set; }
        public virtual DbSet<generos> generos { get; set; }
        public virtual DbSet<idiomas> idiomas { get; set; }
        public virtual DbSet<peliculas> peliculas { get; set; }
        public virtual DbSet<permisos> permisos { get; set; }
        public virtual DbSet<permisos_roles> permisos_roles { get; set; }
        public virtual DbSet<plataformas> plataformas { get; set; }
        public virtual DbSet<plataformas_videojuegos> plataformas_videojuegos { get; set; }
        public virtual DbSet<productos> productos { get; set; }
        public virtual DbSet<productos_alquiler> productos_alquiler { get; set; }
        public virtual DbSet<reservas> reservas { get; set; }
        public virtual DbSet<roles> roles { get; set; }
        public virtual DbSet<tipo_alquiler> tipo_alquiler { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }
        public virtual DbSet<ventas> ventas { get; set; }
        public virtual DbSet<ventas_productos> ventas_productos { get; set; }
        public virtual DbSet<videojuegos> videojuegos { get; set; }
    }
}

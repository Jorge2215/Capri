namespace Pampa.InSol.Dal
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Pampa.InSol.Entities;
    using Pampa.InSol.Entities.Models;
    using Pampa.InSol.Entities.Entities;

    public partial class AppDBContext : DbContext
    {
        public AppDBContext()
            : base("name=AppDBContext")
        {
        }

        public virtual DbSet<Funcion> Funciones { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        public virtual DbSet<Ambiente> Ambientes { get; set; }
        public virtual DbSet<BitacoraInterfaz> BitacoraInterfaz { get; set; }
        public virtual DbSet<CicloAplicativo> CicloAplicativo { get; set; }
        public virtual DbSet<Obsolescencia> Obsolescencia { get; set; }
        public virtual DbSet<Sitio> Sitio { get; set; }
        public virtual DbSet<Frecuencia> Frecuencias { get; set; }
        public virtual DbSet<Interfaz> Interfaces { get; set; }
        public virtual DbSet<Modulo> Modulos { get; set; }
        public virtual DbSet<Negocio> Negocio { get; set; }
        public virtual DbSet<Proceso> Proceso { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<ProductoAmbiente> ProductosAmbientes { get; set; }

        public virtual DbSet<Tecnologia> Tecnologias { get; set; }
        public virtual DbSet<TipoDato> TipoDato { get; set; }
        public virtual DbSet<TipoInterfaz> TipoInterfaz { get; set; }
        public virtual DbSet<Transporte> Transporte { get; set; }

        public virtual DbSet<TipoProducto> TipoProducto { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }
        public virtual DbSet<RolAmbiente> RolAmbiente { get; set; }
      //  public virtual DbSet<ProductoNegocio> ProductosNegocios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Seguridad
            modelBuilder.Entity<Funcion>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Funcion>()
                .Property(e => e.CreadoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Funcion>()
                .Property(e => e.ModificadoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Funcion>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Funciones)
                .Map(m => m.ToTable("RolFuncion").MapLeftKey("IdFuncion").MapRightKey("IdRol"));

            modelBuilder.Entity<Rol>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .Property(e => e.CreadoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .Property(e => e.ModificadoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.UsuarioNT)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.CreadoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.ModificadoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Usuarios)
                .Map(m => m.ToTable("UsuarioRol").MapLeftKey("IdUsuario").MapRightKey("IdRol"));
            #endregion

            //modelBuilder.Entity<ProductoAmbiente>().Ignore(x => x.Id);

            modelBuilder.Entity<ProductoAmbiente>()
                .HasKey(x => x.Id);
              //  .HasForeignKey(x => new { x.IdProducto, x.IdAmbiente, x.IdRolAmbiente });
            //.HasKey(x => new { x.IdProducto, x.IdAmbiente, x.IdRolAmbiente });

            //modelBuilder.Entity<ProductoNegocio>()
            //    .HasKey(x => new { x.IdProducto, x.IdNegocio });

            //modelBuilder.Entity<Producto>()
            //    .HasMany(e => e.ProductoAmbiente)
            //    .WithRequired(e => e.Productos)
            //    .HasForeignKey(e => e.IdProducto);

            //modelBuilder.Entity<Producto>()
            //   .HasMany(e => e.ProductoNegocios)
            //   .WithRequired(e => e.Producto)
            //   .HasForeignKey(e => e.IdProducto);

            //modelBuilder.Entity<Ambiente>()
            //    .HasMany(e => e.ProductoAmbiente)
            //    .WithRequired(e => e.Ambientes)
            //    .HasForeignKey(e => e.IdAmbiente)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Negocio>()
            //    .HasMany(e => e.Productos)
            //    .WithOptional(e => e.Negocio)
            //    .HasForeignKey(e => e.NegocioId);

            //modelBuilder.Entity<Proceso>()
            //    .HasMany(e => e.Productos)
            //    .WithOptional(e => e.Proceso)
            //    .HasForeignKey(e => e.ProcesoId);

            modelBuilder.Entity<Producto>()
                .HasMany(x => x.Modulos)
                .WithMany(x => x.Productos)
                .Map(m => m.ToTable("ProductoModulo").MapLeftKey("IdProducto").MapRightKey("IdModulo"));

            modelBuilder.Entity<Producto>()
                .HasMany(x => x.Procesos)
                .WithMany(x => x.Productos)
                .Map(m => m.ToTable("ProductoProceso").MapLeftKey("IdProducto").MapRightKey("IdProceso"));


            modelBuilder.Entity<Producto>()
                .HasMany(x => x.Negocios)
                .WithMany(x => x.Productos)
                .Map(m => m.ToTable("ProductoNegocio").MapLeftKey("IdProducto").MapRightKey("IdNegocio"));

            //modelBuilder.Entity<Producto>()
            //    .HasMany(x => x.Ambientes)
            //    .WithMany(x => x.Productos)
            //    .Map(m => m.ToTable("ProductoAmbiente").MapLeftKey("IdProducto").MapRightKey("IdAmbiente"));

        }

        public virtual User GetCurrentUser(string userLogin)
        {
            return (from u in this.Usuarios
                    where u.UsuarioNT.ToUpper() == userLogin
                    select new User
                    {
                        CreationDate = u.FechaCreacion,
                        Enabled = u.Activo,
                        Id = u.Id,
                        LastName = u.Apellido,
                        LastUpdtae = u.FechaModificacion,
                        Login = u.UsuarioNT,
                        Name = u.Nombre
                    }).FirstOrDefault();
        }
    }
}

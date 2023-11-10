using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CompraVentaCarrosApi.Models
{
    public partial class VENTACARROSContext : DbContext
    {
        public VENTACARROSContext()
        {
        }

        public VENTACARROSContext(DbContextOptions<VENTACARROSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<Mensaje> Mensajes { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Pqr> Pqrs { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<TipoPqr> TipoPqrs { get; set; }
        public virtual DbSet<TipoVentum> TipoVenta { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Vehiculo> Vehiculos { get; set; }
        public virtual DbSet<Ventum> Venta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-PB1NSRB;Database=VENTACARROS;User Id=julian;password=1234;Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("Factura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.IdVenta).HasColumnName("id_venta");

                entity.Property(e => e.Importe)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("importe");

                entity.Property(e => e.PagoVehiculo)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("pagoVehiculo");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_Factura_Vehiculo");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK_Factura_Venta");
            });

            modelBuilder.Entity<Mensaje>(entity =>
            {
                entity.ToTable("Mensaje");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Email)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.Property(e => e.IdVehiculo).HasColumnName("idVehiculo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Pregunta)
                    .IsUnicode(false)
                    .HasColumnName("pregunta");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Mensajes)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("FK_Mensaje_Persona1");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.Mensajes)
                    .HasForeignKey(d => d.IdVehiculo)
                    .HasConstraintName("FK_Mensaje_Vehiculo");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("imagen");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Ocupacion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("ocupacion");

                entity.Property(e => e.Sexo).HasColumnName("sexo");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<Pqr>(entity =>
            {
                entity.ToTable("PQRS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.Mensaje)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("mensaje");

                entity.Property(e => e.TipoPqrs).HasColumnName("tipoPqrs");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Pqrs)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_PQRS_Persona");

                entity.HasOne(d => d.TipoPqrsNavigation)
                    .WithMany(p => p.Pqrs)
                    .HasForeignKey(d => d.TipoPqrs)
                    .HasConstraintName("FK_PQRS_TipoPqrs");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TipoPqr>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<TipoVentum>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TipoVenta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("tipoVenta");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("contraseña");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.ExpTokenContraseña).HasColumnType("datetime");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.TokenContraseña)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuario");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("FK_Usuario_Persona1");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_Usuario_Rol");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("Vehiculo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Aprovacion).HasColumnName("aprovacion");

                entity.Property(e => e.Carroseria)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("carroseria");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EstadoCompra).HasColumnName("estadoCompra");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.Property(e => e.Imagen1)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("imagen1");

                entity.Property(e => e.Imagen2)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("imagen2");

                entity.Property(e => e.Imagen3)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("imagen3");

                entity.Property(e => e.Kilometraje).HasColumnName("kilometraje");

                entity.Property(e => e.Linea)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("linea");

                entity.Property(e => e.Marca)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("marca");

                entity.Property(e => e.Modelo).HasColumnName("modelo");

                entity.Property(e => e.Placa)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("placa");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("FK_Vehiculo_Persona");
            });

            modelBuilder.Entity<Ventum>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApellidoTitular)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("apellidoTitular");

                entity.Property(e => e.CorreoPaypal)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("correoPaypal");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.IdPago).HasColumnName("id_pago");

                entity.Property(e => e.IdVehiculo).HasColumnName("id_vehiculo");

                entity.Property(e => e.NombreTitular)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nombreTitular");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_Venta_Persona");

                entity.HasOne(d => d.IdPagoNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdPago)
                    .HasConstraintName("FK_Venta_TipoVenta");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdVehiculo)
                    .HasConstraintName("FK_Venta_Vehiculo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

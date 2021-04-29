using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DigitalWareFacturacion.Models
{
    public partial class DIGITALWARE_DBContext : DbContext
    {
        public DIGITALWARE_DBContext()
        {
        }

        public DIGITALWARE_DBContext(DbContextOptions<DIGITALWARE_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<TipoPago> TipoPagos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-G5O2FK8B\\FREDY;Database=DIGITALWARE_DB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.PkIdCategoria)
                    .HasName("PK__CATEGORI__F6908D32014744D3");

                entity.ToTable("CATEGORIA");

                entity.Property(e => e.PkIdCategoria).HasColumnName("PK_Id_categoria");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaActualizacion");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.UltimoModificador).HasColumnName("ultimoModificador");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.PkIdCliente)
                    .HasName("PK__CLIENTE__67D6178607CA0A79");

                entity.ToTable("CLIENTE");

                entity.Property(e => e.PkIdCliente).HasColumnName("PK_Id_cliente");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("apellido");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaActualizacion");

                entity.Property(e => e.FechaNacimineto)
                    .HasColumnType("date")
                    .HasColumnName("fechaNacimineto");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("telefono");

                entity.Property(e => e.UltimoModificador).HasColumnName("ultimoModificador");
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.PkIdDetalleFactura)
                    .HasName("PK__DETALLE___6314F65DCDA441CC");

                entity.ToTable("DETALLE_FACTURA");

                entity.Property(e => e.PkIdDetalleFactura).HasColumnName("PK_Id_detalleFactura");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");

                entity.Property(e => e.FkIdFactura).HasColumnName("FK_Id_factura");

                entity.Property(e => e.FkIdProducto).HasColumnName("FK_Id_producto");

                entity.Property(e => e.Precio)
                    .HasColumnType("money")
                    .HasColumnName("precio");

                entity.Property(e => e.UltimoModificador).HasColumnName("ultimoModificador");

                entity.HasOne(d => d.FkIdFacturaNavigation)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.FkIdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_04_DETALLE_FACTURA_FACTURA");

                entity.HasOne(d => d.FkIdProductoNavigation)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.FkIdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_05_DETALLE_FACTURA_PRODUCTO");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.PkIdFactura)
                    .HasName("PK__FACTURA__C8AFE47F95BE108C");

                entity.ToTable("FACTURA");

                entity.Property(e => e.PkIdFactura).HasColumnName("PK_Id_factura");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");

                entity.Property(e => e.FkIdCliente).HasColumnName("FK_Id_cliente");

                entity.Property(e => e.FkIdTipoPago).HasColumnName("FK_Id_tipoPago");

                entity.Property(e => e.UltimoModificador).HasColumnName("ultimoModificador");

                entity.HasOne(d => d.FkIdClienteNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.FkIdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_02_FACTURA_CLIENTE");

                entity.HasOne(d => d.FkIdTipoPagoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.FkIdTipoPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_03_FACTURA_TIPO_PAGO");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.PkIdProducto)
                    .HasName("PK__PRODUCTO__227720269836D1F2");

                entity.ToTable("PRODUCTO");

                entity.Property(e => e.PkIdProducto).HasColumnName("PK_Id_producto");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaActualizacion");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");

                entity.Property(e => e.FkIdCategoria).HasColumnName("FK_Id_categoria");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasColumnType("money")
                    .HasColumnName("precio");

                entity.Property(e => e.Stock)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("stock");

                entity.Property(e => e.UltimoModificador).HasColumnName("ultimoModificador");

                entity.HasOne(d => d.FkIdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.FkIdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_01_PRODUCTO_CATEGORIA");
            });

            modelBuilder.Entity<TipoPago>(entity =>
            {
                entity.HasKey(e => e.PkIdTipoPago)
                    .HasName("PK__TIPO_PAG__7DFB30B81905FF07");

                entity.ToTable("TIPO_PAGO");

                entity.Property(e => e.PkIdTipoPago).HasColumnName("PK_Id_tipoPago");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaActualizacion");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.UltimoModificador).HasColumnName("ultimoModificador");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

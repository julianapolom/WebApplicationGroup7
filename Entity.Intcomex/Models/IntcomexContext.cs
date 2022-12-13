using Microsoft.EntityFrameworkCore;

namespace Entity.Intcomex.Models;

public partial class IntcomexContext : DbContext
{
    public IntcomexContext()
    {
    }

    public IntcomexContext(DbContextOptions<IntcomexContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ContractClient> ContractClients { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //        optionsBuilder.UseSqlServer("Server=DESKTOP-IADH1LN;DataBase=Intcomex;Trusted_Connection=yes;Encrypt=false;");
    //}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient);

            entity.ToTable("Client");

            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.Charge)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("charge");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstNombre)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("first_nombre");
            entity.Property(e => e.IdContract).HasColumnName("id_contract");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.SecondName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("second_name");
            entity.Property(e => e.UserClient)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("user_client");

            entity.HasOne(d => d.IdContractNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdContract)
                .HasConstraintName("FK_Client_Contract_Client");
        });

        modelBuilder.Entity<ContractClient>(entity =>
        {
            entity.HasKey(e => e.IdContract);

            entity.ToTable("Contract_Client");

            entity.Property(e => e.IdContract).HasColumnName("id_contract");
            entity.Property(e => e.TypeContract)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("type_contract");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

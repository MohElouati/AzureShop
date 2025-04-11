using Microsoft.EntityFrameworkCore;
using DomainModel;

namespace DAL
{
    public class AzureShopContext : DbContext
    {
        public AzureShopContext(DbContextOptions<AzureShopContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<CommandeProduits> CommandeProduits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommandeProduits>()
                .HasKey(cp => new { cp.NumCommande, cp.ProduitID });

            modelBuilder.Entity<CommandeProduits>()
                .HasOne(cp => cp.Commande)
                .WithMany(c => c.CommandeProduits)
                .HasForeignKey(cp => cp.NumCommande);

            modelBuilder.Entity<CommandeProduits>()
                .HasOne(cp => cp.Produit)
                .WithMany()
                .HasForeignKey(cp => cp.ProduitID);
        }
    }
}

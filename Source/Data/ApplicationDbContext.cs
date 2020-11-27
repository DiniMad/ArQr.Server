using Data.Configuration;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<User>                    Users                    { get; set; }
        public DbSet<QrCode>                  QrCodes                  { get; set; }
        public DbSet<QrCodeViewer>            QrCodeViewers            { get; set; }
        public DbSet<Service>                 Services                 { get; set; }
        public DbSet<Purchase>                Purchases                { get; set; }
        public DbSet<MediaContent>            MediaContents            { get; set; }
        public DbSet<SupportedMediaExtension> SupportedMediaExtensions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new QrCodeConfiguration());
            modelBuilder.ApplyConfiguration(new QrCodeViewerConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
            modelBuilder.ApplyConfiguration(new SupportedMediaExtensionConfiguration());
        }
    }
}
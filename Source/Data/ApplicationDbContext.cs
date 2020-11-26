﻿using Data.Configuration;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<User>         Users         { get; set; }
        public DbSet<QrCode>       QrCodes       { get; set; }
        public DbSet<QrCodeViewer> QrCodeViewers { get; set; }
        public DbSet<Service>      Services      { get; set; }

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
        }
    }
}
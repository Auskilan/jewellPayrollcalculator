using PayrollCalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }


        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<BranchAddress> BranchAddresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<AdminBranchMapping> AdminBranchMappings { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PasswordResetOtp> PasswordResetOtps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Organization → Tenant (1 - Many) - Optional initially
            modelBuilder.Entity<Tenant>()
                .HasOne(t => t.Organization)
                .WithMany(o => o.Tenants)
                .HasForeignKey(t => t.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Organization → Branch (1 - Many) - Optional initially
            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Organization)
                .WithMany(o => o.Branches)
                .HasForeignKey(b => b.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tenant → Branch (1 - Many)
            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Tenant)
                .WithMany(t => t.Branches)
                .HasForeignKey(b => b.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Branch → BranchAddress (1 - Many)
            modelBuilder.Entity<BranchAddress>()
                .HasOne(ba => ba.Branch)
                .WithMany(b => b.BranchAddresses)
                .HasForeignKey(ba => ba.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            // Address → BranchAddress (1 - Many)
            modelBuilder.Entity<BranchAddress>()
                .HasOne(ba => ba.Address)
                .WithMany(a => a.BranchAddresses)
                .HasForeignKey(ba => ba.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // Organization → BranchAddress (1 - Many)
            modelBuilder.Entity<BranchAddress>()
                .HasOne(ba => ba.Organization)
                .WithMany(o => o.BranchAddresses)
                .HasForeignKey(ba => ba.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tenant → User (1 - Many)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            // User → Role (Many - Many)
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });


            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // User → AdminBranchMapping (Many - Many)
            modelBuilder.Entity<AdminBranchMapping>()
                .HasKey(ab => new { ab.UserId, ab.BranchId });

            modelBuilder.Entity<AdminBranchMapping>()
                .HasOne(ab => ab.User)
                .WithMany(u => u.AdminBranchMappings)
                .HasForeignKey(ab => ab.UserId);

            modelBuilder.Entity<AdminBranchMapping>()
                .HasOne(ab => ab.Branch)
                .WithMany(b => b.AdminBranchMappings)
                .HasForeignKey(ab => ab.BranchId);

            // User → RefreshToken (1 - Many)

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // PasswordResetOtp configuration
            modelBuilder.Entity<PasswordResetOtp>()
                .HasKey(p => p.OtpId);

            modelBuilder.Entity<PasswordResetOtp>()
                .Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<PasswordResetOtp>()
                .Property(p => p.OtpCode)
                .IsRequired()
                .HasMaxLength(6);

            // data seeding for Roles

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "SuperAdmin" },
                new Role { RoleId = 2, RoleName = "Admin" },
                new Role { RoleId = 3, RoleName = "Employee" }
            );


        }
    }
}

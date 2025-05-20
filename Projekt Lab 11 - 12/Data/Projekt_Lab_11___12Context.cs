using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projekt_Lab_11___12.Models.Entities;
using System.Reflection.Emit;

namespace Projekt_Lab_11___12.Data;

public class Projekt_Lab_11___12Context : IdentityDbContext<User>
{
    public Projekt_Lab_11___12Context(DbContextOptions<Projekt_Lab_11___12Context> options)
        : base(options)
    {
    }

    public DbSet<Shop> Shops { get; set; }
    public DbSet<PointForClick> PointForClicks { get; set; }
    public DbSet<PickaxeShopResourceCost> PickaxeShopResourceCosts { get; set; }
    public DbSet<PickaxeShop> PickaxeShops { get; set; }
    public DbSet<PickaxeResourceMultiplier> PickaxeResourceMultipliers { get; set; }
    public DbSet<Pickaxe> Pickaxes { get; set; }
    public DbSet<Mine> Mines { get; set; }
    public DbSet<LevelRequirement> LevelRequirements { get; set; }
    public DbSet<IronMine> IronMines { get; set; }
    public DbSet<GoldMine> GoldMines { get; set; }
    public DbSet<DiamondMine> DiamondMines { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<PointForClick>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18, 2)");

        builder.Entity<PickaxeShopResourceCost>()
            .Property(p => p.Value)
            .HasColumnType("decimal(18,2)");

        builder.Entity<PickaxeResourceMultiplier>()
            .Property(p => p.Value)
            .HasColumnType("decimal(18,2)");

        builder.Entity<LevelRequirement>()
            .Property(p => p.Amount)
            .HasColumnType("decimal(18,2)");

        builder.Entity<User>()
            .Property(p => p.Iron)
            .HasColumnType("decimal(18,2)");

        builder.Entity<User>()
           .Property(p => p.Gold)
           .HasColumnType("decimal(18,2)");

        builder.Entity<User>()
           .Property(p => p.Diamond)
           .HasColumnType("decimal(18,2)");

        builder.Entity<User>()
            .HasMany(u => u.PickaxesEq)
            .WithMany(p => p.Users)
            .UsingEntity(j => j.ToTable("UserPickaxes"));
        builder.Entity<User>()
            .HasOne(u => u.UsedPickaxe)
            .WithMany()
            .HasForeignKey(u => u.UsedPickaxeId)
            .IsRequired(false);


    }
}

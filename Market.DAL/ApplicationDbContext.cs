using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Market.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);

                builder.HasData(new User[]
                {
                    new User()
                    {
                        Id = 1,
                        Name = "Admin",
                        Password = HashPasswordHelper.HashPassowrd("123456"),
                        Role = Role.Admin
                    },
                    new User()
                    {
                        Id = 2,
                        Name = "Moderator",
                        Password = HashPasswordHelper.HashPassowrd("654321"),
                        Role = Role.Moderator
                    }
                });

                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Profile)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(x => x.Basket)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product>(builder =>
            {
                builder.ToTable("Products").HasKey(x => x.Id);

                builder.HasData(new Product[]
                {
                new Product
                {
                    Id = 1,
                    Name = "Мультиварка",
                    Description = new string('A', 50),
                    DateCreate = DateTime.Now,
                    Price = 2500,
                    Power = 900,
                    Model = "Zepline",
                    Avatar = null,
                    TypeProduct = TypeProduct.ElectricalAppliances
                },


                new Product
                {
                    Id = 2,
                    Name = "Дощовик",
                    Description = new string('A', 50),
                    DateCreate = DateTime.Now,
                    Price = 800,
                    Model = "Пончо-намет 3 в 1",
                    Avatar = null,
                    TypeProduct = TypeProduct.MilitaryEquipment
                },


                new Product
                {
                    Id = 3,
                    Name = "Пилосос",
                    Description = new string('A', 50),
                    DateCreate = DateTime.Now,
                    Price = 3000,
                    Power = 4200,
                    Model = "Rainberg RB-653TB",
                    Avatar = null,
                    TypeProduct = TypeProduct.AppliancesForHome
                },
                new Product
                {
                    Id = 4,
                    Name = "Труси чоловічі",
                    Description = new string('A', 50),
                    DateCreate = DateTime.Now,
                    Price = 120,
                    Power = 900,
                    Model = "Lacoste",
                    Avatar = null,
                    TypeProduct = TypeProduct.Clothes
                }, 
                new Product
                {
                    Id = 5,
                    Name = "М'ясорубка",
                    Description = new string('A', 50),
                    DateCreate = DateTime.Now,
                    Price = 3000,
                    Power = 900,
                    Model = "Grunhelm",
                    Avatar = null,
                    TypeProduct = TypeProduct.ElectricalAppliances
                }


                }) ;
            });

            modelBuilder.Entity<Profile>(builder =>
            {
                builder.ToTable("Profiles").HasKey(x => x.Id);

                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Age);
                builder.Property(x => x.Address).HasMaxLength(200).IsRequired(false);

                builder.HasData(new Profile()
                {
                    Id = 1,
                    UserId = 1
                });
            });

            modelBuilder.Entity<Basket>(builder =>
            {
                builder.ToTable("Baskets").HasKey(x => x.Id);

                builder.HasData(new Basket()
                {
                    Id = 1,
                    UserId = 1
                });
            });

            modelBuilder.Entity<Order>(builder =>
            {
                builder.ToTable("Orders").HasKey(x => x.Id);

                builder.HasOne(r => r.Basket).WithMany(t => t.Orders)
                    .HasForeignKey(r => r.BasketId);
            });
        }
    }
}
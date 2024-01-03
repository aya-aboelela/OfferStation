using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using offerStation.Core.Constants;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.EF.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        public DbSet<City> City { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Delivery> Delivery { get; set; }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerCart> CustomerCart { get; set; }
        public DbSet<CustomerOrder> CustomerOrder { get; set; }
        public DbSet<CustomerReview> CustomerReview { get; set; }
        public DbSet<CustomerCartOffer> CustomerCartOffer { get; set; }
        public DbSet<CustomerOrderOffer> CustomerOrderOffer { get; set; }
        public DbSet<CustomerCardDetails> CustomerCardDetails { get; set; }
        public DbSet<CustomerCartProduct> CustomerCartProduct { get; set; }
        public DbSet<CustomerOrderProduct> CustomerOrderProduct { get; set; }
        public DbSet<CustomerOrderDelivery> CustomerOrderDelivery { get; set; }

        public DbSet<Owner> Owner { get; set; }
        public DbSet<OwnerCart> OwnerCart { get; set; }
        public DbSet<OwnerOrder> OwnerOrder { get; set; }
        public DbSet<OwnerOffer> OwnerOffer { get; set; }
        public DbSet<OwnerReview> OwnerReview { get; set; }
        public DbSet<OwnerProduct> OwnerProduct { get; set; }
        public DbSet<OwnerCategory> OwnerCategory { get; set; }
        public DbSet<OwnerCartOffer> OwnerCartOffer { get; set; }
        public DbSet<OwnerOrderOffer> OwnerOrderOffer { get; set; }
        public DbSet<OwnerCardDetails> OwnerCardDetails { get; set; }
        public DbSet<OwnerCartProduct> OwnerCartProduct { get; set; }
        public DbSet<OwnerMenuCategory> OwnerMenuCategory { get; set; }
        public DbSet<OwnerOrderProduct> OwnerOrderProduct { get; set; }
        public DbSet<OwnerOfferProduct> OwnerOfferProduct { get; set; }
        public DbSet<OwnerOrderDelivery> OwnerOrderDelivery { get; set; }

        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<SupplierOffer> SupplierOffer { get; set; }
        public DbSet<SupplierProduct> SupplierProduct { get; set; }
        public DbSet<SupplierCategory> SupplierCategory { get; set; }
        public DbSet<SupplierMenuCategory> SupplierMenuCategory { get; set; }
        public DbSet<SupplierOfferProduct> SupplierOfferProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<City>().HasData(
                new List<City>{
                    new City
                    {
                        Id = 1,
                        Name = "Assiut"
                    },
                    new City
                    {
                        Id = 2,
                        Name = "Sohag"
                    }
                }
            );

            builder.Entity<OwnerCategory>().HasData(
                new List<OwnerCategory>{
                    new OwnerCategory
                    {
                        Id = 1,
                        Name = "Clothes"
                    },
                    new OwnerCategory
                    {
                        Id = 2,
                        Name = "restaurent"
                    },
                       new OwnerCategory
                    {
                        Id = 3,
                        Name = "Moles"
                    }
                }
            );

            builder.Entity<SupplierCategory>().HasData(
                new List<SupplierCategory>{
                    new SupplierCategory
                    {
                        Id = 1,
                        Name = "Clothes"
                    },
                    new SupplierCategory
                    {
                        Id = 2,
                        Name = "Restaurent"
                    },
                    new SupplierCategory
                    {
                        Id = 3,
                        Name = "Moles"
                    }
                }
            );
            //remove when we project recieve
            builder.Entity<ApplicationUser>().HasData(
                new List<ApplicationUser>{
                    new ApplicationUser
                    {
                        Id = "1",
                        Name = " slasa",
                        Email="salsa@gmail.com",
                        NormalizedEmail="salsa@gmail.com",
                        IsDeleted=false,
                        UserName="salsa",
                        NormalizedUserName="salsa",
                        EmailConfirmed=true,
                        PasswordHash="salsa",
                        SecurityStamp="salsa",
                        ConcurrencyStamp="salsa",
                        PhoneNumber="01111111111",
                        PhoneNumberConfirmed=true,
                        TwoFactorEnabled=false,
                        LockoutEnabled=false,
                        LockoutEnd=DateTime.Now,
                        AccessFailedCount=0,
                    },
                    new ApplicationUser
                    {
                        Id = "2",
                        Name = " supermarket",
                        Email="supermarket@gmail.com",
                        NormalizedEmail="supermarket@gmail.com",
                        IsDeleted=false,
                        UserName="supermarket",
                        NormalizedUserName="supermarket",
                        EmailConfirmed=true,
                        PasswordHash="supermarket",
                        SecurityStamp="supermarket",
                        ConcurrencyStamp="supermarket",
                        PhoneNumber="01111111111",
                        PhoneNumberConfirmed=true,
                        TwoFactorEnabled=false,
                        LockoutEnabled=false,
                        LockoutEnd=DateTime.Now,
                        AccessFailedCount=0,
                    },
                    new ApplicationUser
                    {
                        Id = "3",
                        Name = " bazooka",
                        Email="bazooka@gmail.com",
                        NormalizedEmail="bazooka@gmail.com",
                        IsDeleted=false,
                        UserName="bazooka",
                        NormalizedUserName="bazooka",
                        EmailConfirmed=true,
                        PasswordHash="bazooka",
                        SecurityStamp="bazooka",
                        ConcurrencyStamp="bazooka",
                        PhoneNumber="01111111111",
                        PhoneNumberConfirmed=true,
                        TwoFactorEnabled=false,
                        LockoutEnabled=false,
                        LockoutEnd=DateTime.Now,
                        AccessFailedCount=0,
                    },
                    new ApplicationUser
                    {
                        Id = "4",
                        Name = "super2",
                        Email="super2@gmail.com",
                        NormalizedEmail="super2@gmail.com",
                        IsDeleted=false,
                        UserName="super2",
                        NormalizedUserName="super2",
                        EmailConfirmed=true,
                        PasswordHash="super2",
                        SecurityStamp="super2",
                        ConcurrencyStamp="super2",
                        PhoneNumber="01111111111",
                        PhoneNumberConfirmed=true,
                        TwoFactorEnabled=false,
                        LockoutEnabled=false,
                        LockoutEnd=DateTime.Now,
                        AccessFailedCount=0,
                    },
                    new ApplicationUser
                    {
                        Id = "5",
                        Name = "Ahmed",
                        Email="ahmed@gmail.com",
                        NormalizedEmail="ahmed@gmail.com",
                        IsDeleted=false,
                        UserName="ahmed",
                        NormalizedUserName="ahmed",
                        EmailConfirmed=true,
                        PasswordHash="ahmed",
                        SecurityStamp="ahmed",
                        ConcurrencyStamp="ahmed",
                        PhoneNumber="01551111111",
                        PhoneNumberConfirmed=true,
                        TwoFactorEnabled=false,
                        LockoutEnabled=false,
                        LockoutEnd=DateTime.Now,
                        AccessFailedCount=0,
                    },
                    new ApplicationUser
                    {
                        Id = "6",
                        Name = "Elfarouq",
                        Email="elfarouq@gmail.com",
                        NormalizedEmail="elfarouq@gmail.com",
                        IsDeleted=false,
                        UserName="elfarouq",
                        NormalizedUserName="elfarouq",
                        EmailConfirmed=true,
                        PasswordHash="elfarouq",
                        SecurityStamp="elfarouq",
                        ConcurrencyStamp="elfarouq",
                        PhoneNumber="01771111111",
                        PhoneNumberConfirmed=true,
                        TwoFactorEnabled=false,
                        LockoutEnabled=false,
                        LockoutEnd=DateTime.Now,
                        AccessFailedCount=0,
                    },
                     new ApplicationUser
                    {
                        Id = "7",
                        Name = "ali",
                        Email="ali@gmail.com",
                        NormalizedEmail="ali@gmail.com",
                        IsDeleted=false,
                        UserName="ali",
                        NormalizedUserName="ali",
                        EmailConfirmed=true,
                        PasswordHash="ali",
                        SecurityStamp="ali",
                        ConcurrencyStamp="ali",
                        PhoneNumber="01111111111",
                        PhoneNumberConfirmed=true,
                        TwoFactorEnabled=false,
                        LockoutEnabled=false,
                        LockoutEnd=DateTime.Now,
                        AccessFailedCount=0,
                    },

                }
            ); ;
            builder.Entity<Owner>().HasData(
               new List<Owner>{
                    new Owner
                    {
                       AppUserId="1",
                       Id=1,
                       OwnerCategoryId=1,
                       IsDeleted=false,
                       Approved=true
                    },
                     new Owner
                    {
                       AppUserId="2",
                       Id=2,
                       OwnerCategoryId=2,
                       IsDeleted=false,
                       Approved=true

                    },
                        new Owner
                    {
                       AppUserId="3",
                       Id=3,
                       OwnerCategoryId=3,
                       IsDeleted=false,
                       Approved=true

                    },
                    new Owner
                    {
                       AppUserId="4",
                       Id=4,
                       OwnerCategoryId=3,
                       IsDeleted=false,
                       Approved=true

                    },

               }
           ); ;
            builder.Entity<Customer>().HasData(
              new List<Customer>{
                    new Customer
                    {
                       AppUserId="5",
                       Id=1,
                       IsDeleted=false,
                    },
                    new Customer
                    {
                       AppUserId="6",
                       Id=2,
                       IsDeleted=false,
                    },
              }
          );


            builder.Entity<Supplier>().HasData(
               new List<Supplier>{
                    new Supplier
                    {
                       AppUserId="7",
                       Id=1,
                       SupplierCategoryId=3,
                       IsDeleted=false,
                       Approved=true
                    },
               }
           );
            builder.Entity<OwnerOffer>().HasData(
              new List<OwnerOffer>{
                    new OwnerOffer
                    {
                       Id=1,
                       Name="OFFER1",
                       Description="new offer",
                       OwnerId=1,
                       IsDeleted=false,
                       Price=300,
                    },
                     new OwnerOffer
                    {
                       Id=2,
                       Name="OFFER2",
                       Description="new offer",
                       OwnerId=1,
                       IsDeleted=false,
                       Price=100,
                    },
                      new OwnerOffer
                    {

                       Id=3,
                       Name="OFFER3",
                       Description="new offer",
                       OwnerId=1,
                       IsDeleted=false,
                       Price=500,
                    },
                      new OwnerOffer
                    {
                       Id=4,
                       Name="OFFER4",
                       Description="new offer",
                       OwnerId=2,
                       IsDeleted=false,
                       Price=300,
                    },
                     new OwnerOffer
                    {
                       Id=5,
                       Name="OFFER5",
                       Description="new offer",
                       OwnerId=2,
                       IsDeleted=false,
                       Price=100,
                    },
                      new OwnerOffer
                    {

                       Id=6,
                       Name="OFFER6",
                       Description="new offer",
                       OwnerId=2,
                       IsDeleted=false,
                       Price=500,
                    },
                      new OwnerOffer
                    {
                       Id=7,
                       Name="OFFER7",
                       Description="new offer",
                       OwnerId=3,
                       IsDeleted=false,
                       Price=700,
                    },
                     new OwnerOffer
                    {
                       Id=8,
                       Name="OFFER8",
                       Description="new offer",
                       OwnerId=3,
                       IsDeleted=false,
                       Price=100,
                    },
                      new OwnerOffer
                    {

                       Id=9,
                       Name="OFFER9",
                       Description="new offer",
                       OwnerId=3,
                       IsDeleted=false,
                       Price=400,
                    },
                            new OwnerOffer
                    {

                       Id=10,
                       Name="OFFER10",
                       Description="new offer",
                       OwnerId=3,
                       IsDeleted=false,
                       Price=400,
                    },
                    new OwnerOffer
                    {

                       Id=11,
                       Name="OFFER11",
                       Description="new offer",
                       OwnerId=3,
                       IsDeleted=false,
                       Price=400,
                    },
                    new OwnerOffer
                    {

                       Id=12,
                       Name="OFFER12",
                       Description="new offer",
                       OwnerId=3,
                       IsDeleted=false,
                       Price=400,
                    },

              }
          ); ;

            builder.Entity<Delivery>().HasData(
             new List<Delivery>{
                    new Delivery
                    {

                      Id=1,
                      Name="nada",
                      Phone="0112567898",
                      IsDeleted=false,
                    },
                     new Delivery
                    {
                      Id=2,
                      Name="omnia",
                      Phone="0112567898",
                      IsDeleted=false,
                    },

             }
         ); ;

            builder.Entity<CustomerOrder>().HasData(
             new List<CustomerOrder>{
                    new CustomerOrder
                    {
                       Id=1,
                       CustomerId=1,
                       OwnerId=1,
                       IsDeleted=false,
                       orderDate=DateTime.Now,
                       PaymentMethod=PaymentMethods.cash,
                       orderStatus=OrderStatus.pending

                    },
                     new CustomerOrder
                    {

                       Id=2,
                       CustomerId=1,
                       OwnerId=1,
                       IsDeleted=false,
                       orderDate=DateTime.Now,
                       PaymentMethod=PaymentMethods.cash,
                       orderStatus=OrderStatus.pending

                    },
                       new CustomerOrder
                    {

                       Id=3,
                       CustomerId=1,
                       OwnerId=2,
                       IsDeleted=false,
                       orderDate=DateTime.Now,
                       PaymentMethod=PaymentMethods.cash,
                       orderStatus=OrderStatus.pending

                    },
                    new CustomerOrder
                    {

                       Id=4,
                       CustomerId=1,
                       OwnerId=2,
                       IsDeleted=false,
                       orderDate=DateTime.Now,
                       PaymentMethod=PaymentMethods.cash,
                       orderStatus=OrderStatus.pending
                    },

             }
         );
            builder.Entity<CustomerOrderOffer>().HasData(
             new List<CustomerOrderOffer>{
                    new CustomerOrderOffer
                    {

                       Id=1,
                       OrderId=1,
                       OwnerOffertId=1,
                       Quantity=2,
                       IsDeleted=false,

                    },
                    new CustomerOrderOffer
                    {

                       Id=2,
                       OrderId=1,
                       OwnerOffertId=2,
                       Quantity=5,
                       IsDeleted=false,

                    },
                     new CustomerOrderOffer
                    {

                       Id=3,
                       OrderId=1,
                       OwnerOffertId=3,
                       Quantity=3,
                       IsDeleted=false,
                    }

             }
         );

            builder.Entity<OwnerMenuCategory>().HasData(
         new List<OwnerMenuCategory>{
                    new OwnerMenuCategory
                    {

                       Id=1,
                       IsDeleted=false,
                       Name = "Elmahweyat",
                       OwnerId =1,
                    },
                    new OwnerMenuCategory
                    {

                       Id=2,
                       IsDeleted=false,
                       Name = "meshElmashweyat",
                       OwnerId =1,

                    },
                     new OwnerMenuCategory
                    {

                       Id=3,
                       IsDeleted=false,
                       Name = "Salatat",
                       OwnerId =1,
                    }

         }
     );

            builder.Entity<OwnerProduct>().HasData(
               new List<OwnerProduct>{
                    new OwnerProduct
                    {
                        Id = 1,
                        Name = "p1",
                        Price = 120,
                        Description = "Short Description",
                        Discount = 3,
                        CreatedTime = DateTime.Now,
                        OwnerId =1,
                        CategoryId =1,
                    },
                    new OwnerProduct
                    {
                        Id = 2,
                        Name = "p2",
                        Price = 210,
                        Description = "Short Description2",
                        Discount = 5,
                        CreatedTime = DateTime.Now,
                        OwnerId = 1,
                        CategoryId = 2,
                    }
               }
           );
        }
    }
}

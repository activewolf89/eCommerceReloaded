using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using eCommerceReloaded.Models;

namespace eCommerceReloaded.Migrations
{
    [DbContext(typeof(eCommerceReloadedContext))]
    [Migration("20170502225427_UpdatedImages")]
    partial class UpdatedImages
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("eCommerceReloaded.Models.Cart", b =>
                {
                    b.Property<int>("cartId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("userId");

                    b.HasKey("cartId");

                    b.HasIndex("userId");

                    b.ToTable("carts");
                });

            modelBuilder.Entity("eCommerceReloaded.Models.Category", b =>
                {
                    b.Property<int>("categoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_At");

                    b.Property<string>("name");

                    b.HasKey("categoryId");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("eCommerceReloaded.Models.Event", b =>
                {
                    b.Property<int>("eventId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("month");

                    b.Property<string>("name");

                    b.HasKey("eventId");

                    b.ToTable("events");
                });

            modelBuilder.Entity("eCommerceReloaded.Models.Order", b =>
                {
                    b.Property<int>("orderId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_At");

                    b.Property<int>("userId");

                    b.HasKey("orderId");

                    b.HasIndex("userId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("eCommerceReloaded.Models.Product", b =>
                {
                    b.Property<int>("productId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("categoryId");

                    b.Property<int>("cost");

                    b.Property<DateTime>("created_At");

                    b.Property<string>("description");

                    b.Property<byte[]>("image");

                    b.Property<string>("imageUrl");

                    b.Property<int>("inventory");

                    b.Property<string>("name");

                    b.Property<int>("price");

                    b.HasKey("productId");

                    b.HasIndex("categoryId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("eCommerceReloaded.Models.ProductEvent", b =>
                {
                    b.Property<int>("productEventId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("eventId");

                    b.Property<int>("productId");

                    b.HasKey("productEventId");

                    b.HasIndex("eventId");

                    b.HasIndex("productId");

                    b.ToTable("productEvents");
                });

            modelBuilder.Entity("eCommerceReloaded.Models.ProductInCart", b =>
                {
                    b.Property<int>("picId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("cartId");

                    b.Property<DateTime>("created_At");

                    b.Property<int>("productId");

                    b.Property<int>("quantity");

                    b.HasKey("picId");

                    b.HasIndex("cartId");

                    b.HasIndex("productId");

                    b.ToTable("productInCarts");
                });

            modelBuilder.Entity("eCommerceReloaded.Models.ProductInOrder", b =>
                {
                    b.Property<int>("productInOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_At");

                    b.Property<int>("orderId");

                    b.Property<int>("productId");

                    b.Property<int>("quantity");

                    b.HasKey("productInOrderId");

                    b.HasIndex("orderId");

                    b.HasIndex("productId");

                    b.ToTable("productinOrders");
                });

            modelBuilder.Entity("eCommerceReloaded.Models.ProductInWishlist", b =>
                {
                    b.Property<int>("piwId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("created_At");

                    b.Property<int>("productId");

                    b.Property<int>("wishlistId");

                    b.HasKey("piwId");

                    b.HasIndex("productId");

                    b.HasIndex("wishlistId");

                    b.ToTable("productInWishlists");
                });

            modelBuilder.Entity("eCommerceReloaded.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("city");

                    b.Property<DateTime>("created_At");

                    b.Property<string>("email");

                    b.Property<string>("firstName");

                    b.Property<string>("imgUrl");

                    b.Property<string>("lastName");

                    b.Property<string>("password");

                    b.Property<string>("shipToAddress");

                    b.Property<string>("state");

                    b.Property<int>("wishlistId");

                    b.Property<int>("zipcode");

                    b.HasKey("userId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("eCommerceReloaded.Models.Wishlist", b =>
                {
                    b.Property<int>("wishlistId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("userId");

                    b.HasKey("wishlistId");

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("wishlists");
                });

            modelBuilder.Entity("eCommerceReloaded.Models.Cart", b =>
                {
                    b.HasOne("eCommerceReloaded.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("eCommerceReloaded.Models.Order", b =>
                {
                    b.HasOne("eCommerceReloaded.Models.User", "user")
                        .WithMany("listOfOrders")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eCommerceReloaded.Models.Product", b =>
                {
                    b.HasOne("eCommerceReloaded.Models.Category", "category")
                        .WithMany("listOfProducts")
                        .HasForeignKey("categoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eCommerceReloaded.Models.ProductEvent", b =>
                {
                    b.HasOne("eCommerceReloaded.Models.Event", "anEvent")
                        .WithMany("listOfProductEvent")
                        .HasForeignKey("eventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eCommerceReloaded.Models.Product", "product")
                        .WithMany("listOfProductEvent")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eCommerceReloaded.Models.ProductInCart", b =>
                {
                    b.HasOne("eCommerceReloaded.Models.Cart", "cart")
                        .WithMany("listOfProductInCart")
                        .HasForeignKey("cartId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eCommerceReloaded.Models.Product", "product")
                        .WithMany("listOfProductInCart")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eCommerceReloaded.Models.ProductInOrder", b =>
                {
                    b.HasOne("eCommerceReloaded.Models.Order", "order")
                        .WithMany("listOfProductInOrder")
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eCommerceReloaded.Models.Product", "product")
                        .WithMany("listOfProductInOrder")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eCommerceReloaded.Models.ProductInWishlist", b =>
                {
                    b.HasOne("eCommerceReloaded.Models.Product", "product")
                        .WithMany("listOfProductInWishlist")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eCommerceReloaded.Models.Wishlist", "wishlist")
                        .WithMany("listOfProductInWishlist")
                        .HasForeignKey("wishlistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eCommerceReloaded.Models.Wishlist", b =>
                {
                    b.HasOne("eCommerceReloaded.Models.User", "user")
                        .WithOne("wishlist")
                        .HasForeignKey("eCommerceReloaded.Models.Wishlist", "userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

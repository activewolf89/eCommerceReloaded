using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerceReloaded.Migrations
{
    public partial class firsttime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    categoryId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    created_At = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.categoryId);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    eventId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    month = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.eventId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    city = table.Column<string>(nullable: true),
                    created_At = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    firstName = table.Column<string>(nullable: true),
                    imgUrl = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    shipToAddress = table.Column<string>(nullable: true),
                    state = table.Column<string>(nullable: true),
                    wishlistId = table.Column<int>(nullable: false),
                    zipcode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    productId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    categoryId = table.Column<int>(nullable: false),
                    cost = table.Column<int>(nullable: false),
                    created_At = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    image = table.Column<byte[]>(nullable: true),
                    imageUrl = table.Column<string>(nullable: true),
                    inventory = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.productId);
                    table.ForeignKey(
                        name: "FK_products_categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "categories",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    cartId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    userId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.cartId);
                    table.ForeignKey(
                        name: "FK_carts_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    orderId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    created_At = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.orderId);
                    table.ForeignKey(
                        name: "FK_orders_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wishlists",
                columns: table => new
                {
                    wishlistId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wishlists", x => x.wishlistId);
                    table.ForeignKey(
                        name: "FK_wishlists_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productEvents",
                columns: table => new
                {
                    productEventId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    eventId = table.Column<int>(nullable: false),
                    productId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productEvents", x => x.productEventId);
                    table.ForeignKey(
                        name: "FK_productEvents_events_eventId",
                        column: x => x.eventId,
                        principalTable: "events",
                        principalColumn: "eventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productEvents_products_productId",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productInCarts",
                columns: table => new
                {
                    picId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    cartId = table.Column<int>(nullable: false),
                    created_At = table.Column<DateTime>(nullable: false),
                    productId = table.Column<int>(nullable: false),
                    quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productInCarts", x => x.picId);
                    table.ForeignKey(
                        name: "FK_productInCarts_carts_cartId",
                        column: x => x.cartId,
                        principalTable: "carts",
                        principalColumn: "cartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productInCarts_products_productId",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productinOrders",
                columns: table => new
                {
                    productInOrderId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    created_At = table.Column<DateTime>(nullable: false),
                    orderId = table.Column<int>(nullable: false),
                    productId = table.Column<int>(nullable: false),
                    quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productinOrders", x => x.productInOrderId);
                    table.ForeignKey(
                        name: "FK_productinOrders_orders_orderId",
                        column: x => x.orderId,
                        principalTable: "orders",
                        principalColumn: "orderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productinOrders_products_productId",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productInWishlists",
                columns: table => new
                {
                    piwId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    created_At = table.Column<DateTime>(nullable: false),
                    productId = table.Column<int>(nullable: false),
                    wishlistId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productInWishlists", x => x.piwId);
                    table.ForeignKey(
                        name: "FK_productInWishlists_products_productId",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productInWishlists_wishlists_wishlistId",
                        column: x => x.wishlistId,
                        principalTable: "wishlists",
                        principalColumn: "wishlistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carts_userId",
                table: "carts",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_userId",
                table: "orders",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_products_categoryId",
                table: "products",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_productEvents_eventId",
                table: "productEvents",
                column: "eventId");

            migrationBuilder.CreateIndex(
                name: "IX_productEvents_productId",
                table: "productEvents",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_productInCarts_cartId",
                table: "productInCarts",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_productInCarts_productId",
                table: "productInCarts",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_productinOrders_orderId",
                table: "productinOrders",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_productinOrders_productId",
                table: "productinOrders",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_productInWishlists_productId",
                table: "productInWishlists",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_productInWishlists_wishlistId",
                table: "productInWishlists",
                column: "wishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_wishlists_userId",
                table: "wishlists",
                column: "userId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productEvents");

            migrationBuilder.DropTable(
                name: "productInCarts");

            migrationBuilder.DropTable(
                name: "productinOrders");

            migrationBuilder.DropTable(
                name: "productInWishlists");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "wishlists");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using onlineShop.Data;

namespace onlineShop.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210916103617_CartItemTableUpdate")]
    partial class CartItemTableUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CartEntityInventoryEntity", b =>
                {
                    b.Property<int>("CartssId")
                        .HasColumnType("int");

                    b.Property<int>("InventoriessId")
                        .HasColumnType("int");

                    b.HasKey("CartssId", "InventoriessId");

                    b.HasIndex("InventoriessId");

                    b.ToTable("CartEntityInventoryEntity");
                });

            modelBuilder.Entity("onlineShop.Entity.CartEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CheckedOut")
                        .HasColumnType("bit");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.Property<int?>("UserEntityId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserEntityId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("onlineShop.Entity.CartItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CartEntityId")
                        .HasColumnType("int");

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int?>("InventoryEntityId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartEntityId");

                    b.HasIndex("InventoryEntityId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("onlineShop.Entity.InventoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailableStock")
                        .HasColumnType("int");

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnitPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("onlineShop.Entity.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CartEntityInventoryEntity", b =>
                {
                    b.HasOne("onlineShop.Entity.CartEntity", null)
                        .WithMany()
                        .HasForeignKey("CartssId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("onlineShop.Entity.InventoryEntity", null)
                        .WithMany()
                        .HasForeignKey("InventoriessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("onlineShop.Entity.CartEntity", b =>
                {
                    b.HasOne("onlineShop.Entity.UserEntity", "UserEntity")
                        .WithMany("Cartss")
                        .HasForeignKey("UserEntityId");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("onlineShop.Entity.CartItemEntity", b =>
                {
                    b.HasOne("onlineShop.Entity.CartEntity", "CartEntity")
                        .WithMany("CartItemss")
                        .HasForeignKey("CartEntityId");

                    b.HasOne("onlineShop.Entity.InventoryEntity", "InventoryEntity")
                        .WithMany("CartItemss")
                        .HasForeignKey("InventoryEntityId");

                    b.Navigation("CartEntity");

                    b.Navigation("InventoryEntity");
                });

            modelBuilder.Entity("onlineShop.Entity.CartEntity", b =>
                {
                    b.Navigation("CartItemss");
                });

            modelBuilder.Entity("onlineShop.Entity.InventoryEntity", b =>
                {
                    b.Navigation("CartItemss");
                });

            modelBuilder.Entity("onlineShop.Entity.UserEntity", b =>
                {
                    b.Navigation("Cartss");
                });
#pragma warning restore 612, 618
        }
    }
}

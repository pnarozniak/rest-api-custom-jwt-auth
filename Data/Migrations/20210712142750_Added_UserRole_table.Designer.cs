﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using rest_api_custom_jwt_auth.Data;

namespace rest_api_custom_jwt_auth.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210712142750_Added_UserRole_table")]
    partial class Added_UserRole_table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("rest_api_custom_jwt_auth.Models.Role", b =>
                {
                    b.Property<int>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("IdRole")
                        .HasName("Role_pk");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("rest_api_custom_jwt_auth.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasMaxLength(86)
                        .HasColumnType("nvarchar(86)");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime?>("RefreshTokenExpirationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IdUser")
                        .HasName("User_pk");

                    b.ToTable("User");
                });

            modelBuilder.Entity("rest_api_custom_jwt_auth.Models.UserRole", b =>
                {
                    b.Property<int>("IdRole")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.HasKey("IdRole", "IdUser")
                        .HasName("UserRole_pk");

                    b.HasIndex("IdUser");

                    b.ToTable("User_Role");
                });

            modelBuilder.Entity("rest_api_custom_jwt_auth.Models.UserRole", b =>
                {
                    b.HasOne("rest_api_custom_jwt_auth.Models.Role", "IdRoleNavigation")
                        .WithMany("UserRoles")
                        .HasForeignKey("IdRole")
                        .HasConstraintName("Role_UserRole")
                        .IsRequired();

                    b.HasOne("rest_api_custom_jwt_auth.Models.User", "IdUserNavigation")
                        .WithMany("UserRoles")
                        .HasForeignKey("IdUser")
                        .HasConstraintName("User_UserRole")
                        .IsRequired();

                    b.Navigation("IdRoleNavigation");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("rest_api_custom_jwt_auth.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("rest_api_custom_jwt_auth.Models.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelPics.Domains.DataAccess;

#nullable disable

namespace TravelPics.Domains.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230510101454_update-id-long-notification-log")]
    partial class updateidlongnotificationlog
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TravelPics.Domains.Entities.Document", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("BlobFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BlobUri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("DocumentBlobContainerId")
                        .HasColumnType("int");

                    b.Property<int>("DocumentExtensionId")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsProfileImage")
                        .HasColumnType("bit");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<long>("UploadedById")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DocumentBlobContainerId");

                    b.HasIndex("DocumentExtensionId");

                    b.HasIndex("PostId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.DocumentBlobContainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContainerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DocumentBlobContainers");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.DocumentExtension", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("DocumentExtensions");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.InAppNotification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("GeneratedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("NotificationLogId")
                        .HasColumnType("bigint");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NotificationLogId");

                    b.ToTable("InAppNotifications");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("DislikedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTimeOffset>("LikedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Latitude")
                        .HasPrecision(12, 6)
                        .HasColumnType("decimal(12,6)");

                    b.Property<decimal>("Longitude")
                        .HasPrecision(12, 6)
                        .HasColumnType("decimal(12,6)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.NotificationLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("NotificationTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Payload")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NotificationTypeId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("StatusId");

                    b.ToTable("NotificationLogs");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.NotificationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NotificationStatuses");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.NotificationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NotificationTypes");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("PublishedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("LocationId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ProfileImageId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProfileImageId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.Document", b =>
                {
                    b.HasOne("TravelPics.Domains.Entities.DocumentBlobContainer", "DocumentBlobContainer")
                        .WithMany("Documents")
                        .HasForeignKey("DocumentBlobContainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelPics.Domains.Entities.DocumentExtension", "DocumentExtension")
                        .WithMany("Documents")
                        .HasForeignKey("DocumentExtensionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelPics.Domains.Entities.Post", "Post")
                        .WithMany("Photos")
                        .HasForeignKey("PostId");

                    b.Navigation("DocumentBlobContainer");

                    b.Navigation("DocumentExtension");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.InAppNotification", b =>
                {
                    b.HasOne("TravelPics.Domains.Entities.NotificationLog", "NotificationLog")
                        .WithMany("InAppNotifications")
                        .HasForeignKey("NotificationLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NotificationLog");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.Like", b =>
                {
                    b.HasOne("TravelPics.Domains.Entities.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelPics.Domains.Entities.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.NotificationLog", b =>
                {
                    b.HasOne("TravelPics.Domains.Entities.NotificationType", "NotificationType")
                        .WithMany("NotificationLogs")
                        .HasForeignKey("NotificationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelPics.Domains.Entities.User", "Receiver")
                        .WithMany("NotificationLogs")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelPics.Domains.Entities.NotificationStatus", "Status")
                        .WithMany("NotificationLogs")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NotificationType");

                    b.Navigation("Receiver");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.Post", b =>
                {
                    b.HasOne("TravelPics.Domains.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelPics.Domains.Entities.Location", "Location")
                        .WithMany("Posts")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.User", b =>
                {
                    b.HasOne("TravelPics.Domains.Entities.Document", "ProfileImage")
                        .WithMany()
                        .HasForeignKey("ProfileImageId");

                    b.Navigation("ProfileImage");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.DocumentBlobContainer", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.DocumentExtension", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.Location", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.NotificationLog", b =>
                {
                    b.Navigation("InAppNotifications");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.NotificationStatus", b =>
                {
                    b.Navigation("NotificationLogs");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.NotificationType", b =>
                {
                    b.Navigation("NotificationLogs");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.Post", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("TravelPics.Domains.Entities.User", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("NotificationLogs");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Library.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryManagement.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20240510102133_UpdatedEmailConfiguration")]
    partial class UpdatedEmailConfiguration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Library.Model.Models.Author", b =>
                {
                    b.Property<Guid>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuthorId");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("Library.Model.Models.Book", b =>
                {
                    b.Property<Guid>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Edition")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishedYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Library.Model.Models.BookAuthor", b =>
                {
                    b.Property<Guid>("AuthorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AuthorID", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BookAuthor");
                });

            modelBuilder.Entity("Library.Model.Models.BookCategory", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoryId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BookCategories");
                });

            modelBuilder.Entity("Library.Model.Models.BookCopy", b =>
                {
                    b.Property<Guid>("BookCopyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("int");

                    b.Property<Guid>("OriginaBookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("BookCopyId");

                    b.HasIndex("OriginaBookId");

                    b.ToTable("BookCopies");
                });

            modelBuilder.Entity("Library.Model.Models.BookCopyShelf", b =>
                {
                    b.Property<Guid>("BookCopyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShelfId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BookCopyId", "ShelfId", "RoomId");

                    b.HasIndex("RoomId", "ShelfId");

                    b.ToTable("Stored");
                });

            modelBuilder.Entity("Library.Model.Models.BookPublisher", b =>
                {
                    b.Property<Guid>("PublisherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PublisherId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BookPublisher");
                });

            modelBuilder.Entity("Library.Model.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Library.Model.Models.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("RegularExpression", "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")
                        .HasAnnotation("RegularExpressionErrorMessage", "Invalid email address format");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("RegularExpression", "^\\+\\d{1,3}\\s?\\(?\\d{1,4}\\)?[\\s.-]?\\d{1,10}$")
                        .HasAnnotation("RegularExpressionErrorMessage", "Invalid phone number format");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Library.Model.Models.EmailTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemplateName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("To")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmailTemplate");

                    b.HasData(
                        new
                        {
                            Id = new Guid("013f3261-0a3b-4753-a98d-463668414616"),
                            Body = "Dear @@userName@@,<br><br>Please click the following link to verify your email address:<br><br>@@verificationLink@@<br><br>If you didn't request this verification, please ignore this email.<br><br>Best regards,<br>The Team",
                            From = "noreply@example.com",
                            Subject = "Verify Your Email Address",
                            TemplateName = "Email Confirmation",
                            To = "@@userEmail@@"
                        });
                });

            modelBuilder.Entity("Library.Model.Models.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("RoomId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Library.Model.Models.NavigationMenu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ControllerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentMenuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Permitted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ParentMenuId");

                    b.ToTable("NavigationMenus");

                    b.HasData(
                        new
                        {
                            Id = new Guid("81a3994d-37ee-4833-a978-de19e7364514"),
                            Name = "Admin",
                            Permitted = true
                        },
                        new
                        {
                            Id = new Guid("bf7fed55-6c0a-4559-8add-139cd98bd876"),
                            ActionName = "Roles",
                            ControllerName = "Administrator",
                            Name = "Roles",
                            ParentMenuId = new Guid("81a3994d-37ee-4833-a978-de19e7364514"),
                            Permitted = true
                        },
                        new
                        {
                            Id = new Guid("b86538b2-c245-40fe-be8b-ff64cdc62637"),
                            ActionName = "Users",
                            ControllerName = "Administrator",
                            Name = "Users",
                            ParentMenuId = new Guid("81a3994d-37ee-4833-a978-de19e7364514"),
                            Permitted = true
                        },
                        new
                        {
                            Id = new Guid("1c48f4be-67d9-4201-93ca-6fbaead68f59"),
                            ActionName = "Templates",
                            ControllerName = "Administrator",
                            Name = "Email Templates",
                            ParentMenuId = new Guid("81a3994d-37ee-4833-a978-de19e7364514"),
                            Permitted = true
                        });
                });

            modelBuilder.Entity("Library.Model.Models.Publisher", b =>
                {
                    b.Property<Guid>("PublisherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PublisherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PublisherId");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("Library.Model.Models.Reservation", b =>
                {
                    b.Property<Guid>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ActualReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("BookCopyID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CheckoutTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsLate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bit")
                        .HasComputedColumnSql("CASE WHEN [ActualReturnDate] IS NULL THEN CAST(NULL AS BIT) ELSE CAST(CASE WHEN [ActualReturnDate] > [SupposedReturnDate] THEN 1 ELSE 0 END AS BIT) END");

                    b.Property<Guid>("ReturnCustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SupposedReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ReservationId");

                    b.HasIndex("BookCopyID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Library.Model.Models.RoleMenuPermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NavigationMenuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("NavigationMenuId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleMenus");

                    b.HasData(
                        new
                        {
                            Id = new Guid("851217e0-a1c0-43a9-a1d3-7667f5ad6aae"),
                            NavigationMenuId = new Guid("bf7fed55-6c0a-4559-8add-139cd98bd876"),
                            RoleId = "2a2e8e85-49f0-45ef-97c5-3151d1b91306"
                        },
                        new
                        {
                            Id = new Guid("85e04330-3d75-494c-a115-61f52c389c95"),
                            NavigationMenuId = new Guid("b86538b2-c245-40fe-be8b-ff64cdc62637"),
                            RoleId = "2a2e8e85-49f0-45ef-97c5-3151d1b91306"
                        },
                        new
                        {
                            Id = new Guid("320027db-c5a8-459e-865c-cf673d15cfe3"),
                            NavigationMenuId = new Guid("81a3994d-37ee-4833-a978-de19e7364514"),
                            RoleId = "2a2e8e85-49f0-45ef-97c5-3151d1b91306"
                        },
                        new
                        {
                            Id = new Guid("683bbe59-c119-4037-a42a-df75cbd86725"),
                            NavigationMenuId = new Guid("1c48f4be-67d9-4201-93ca-6fbaead68f59"),
                            RoleId = "2a2e8e85-49f0-45ef-97c5-3151d1b91306"
                        });
                });

            modelBuilder.Entity("Library.Model.Models.Room", b =>
                {
                    b.Property<Guid>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.HasKey("RoomId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Library.Model.Models.Shelf", b =>
                {
                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShelfId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.HasKey("RoomId", "ShelfId");

                    b.ToTable("Shelf");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "27c223b9-9d7e-402e-ac9a-2433f2ab4121",
                            Name = "Librarian",
                            NormalizedName = "LIBRARIAN"
                        },
                        new
                        {
                            Id = "5812cd57-6d8c-4c22-be9d-207667e9b28c",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "6074b943-b2f1-4c2a-8de9-3063bde3099f",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "72ac7328-ef19-4631-b627-642816e0d0ab",
                            Name = "Archivist",
                            NormalizedName = "ARCHIVIST"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Library.Model.Models.BookAuthor", b =>
                {
                    b.HasOne("Library.Model.Models.Author", "Author")
                        .WithMany("BooksWritten")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Model.Models.Book", "Book")
                        .WithMany("Authors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Library.Model.Models.BookCategory", b =>
                {
                    b.HasOne("Library.Model.Models.Book", "Book")
                        .WithMany("Categories")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Model.Models.Category", "Category")
                        .WithMany("BookCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Library.Model.Models.BookCopy", b =>
                {
                    b.HasOne("Library.Model.Models.Book", "OriginalBook")
                        .WithMany("Copyrights")
                        .HasForeignKey("OriginaBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OriginalBook");
                });

            modelBuilder.Entity("Library.Model.Models.BookCopyShelf", b =>
                {
                    b.HasOne("Library.Model.Models.BookCopy", "BookCopy")
                        .WithMany("Shelves")
                        .HasForeignKey("BookCopyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Model.Models.Shelf", "Shelf")
                        .WithMany("Books")
                        .HasForeignKey("RoomId", "ShelfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookCopy");

                    b.Navigation("Shelf");
                });

            modelBuilder.Entity("Library.Model.Models.BookPublisher", b =>
                {
                    b.HasOne("Library.Model.Models.Book", "Book")
                        .WithMany("Publishers")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Model.Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("Library.Model.Models.Employee", b =>
                {
                    b.HasOne("Library.Model.Models.Room", null)
                        .WithMany("Employees")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("Library.Model.Models.NavigationMenu", b =>
                {
                    b.HasOne("Library.Model.Models.NavigationMenu", "ParentNavigationMenu")
                        .WithMany()
                        .HasForeignKey("ParentMenuId");

                    b.Navigation("ParentNavigationMenu");
                });

            modelBuilder.Entity("Library.Model.Models.Reservation", b =>
                {
                    b.HasOne("Library.Model.Models.BookCopy", "BookCopy")
                        .WithMany("Reservations")
                        .HasForeignKey("BookCopyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Model.Models.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Model.Models.Employee", "Employee")
                        .WithMany("Reservations")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookCopy");

                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Library.Model.Models.RoleMenuPermission", b =>
                {
                    b.HasOne("Library.Model.Models.NavigationMenu", "NavigationMenu")
                        .WithMany()
                        .HasForeignKey("NavigationMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NavigationMenu");
                });

            modelBuilder.Entity("Library.Model.Models.Shelf", b =>
                {
                    b.HasOne("Library.Model.Models.Room", "Room")
                        .WithMany("Shelves")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Library.Model.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Library.Model.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Model.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Library.Model.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Library.Model.Models.Author", b =>
                {
                    b.Navigation("BooksWritten");
                });

            modelBuilder.Entity("Library.Model.Models.Book", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Categories");

                    b.Navigation("Copyrights");

                    b.Navigation("Publishers");
                });

            modelBuilder.Entity("Library.Model.Models.BookCopy", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("Shelves");
                });

            modelBuilder.Entity("Library.Model.Models.Category", b =>
                {
                    b.Navigation("BookCategories");
                });

            modelBuilder.Entity("Library.Model.Models.Customer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Library.Model.Models.Employee", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Library.Model.Models.Publisher", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Library.Model.Models.Room", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Shelves");
                });

            modelBuilder.Entity("Library.Model.Models.Shelf", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using ESCenter.Persistence.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ESCenter.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240204061111_UpdateFKForTutor")]
    partial class UpdateFKForTutor
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ESCenter.Domain.Aggregates.Courses.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<int>("AcademicLevelRequirement")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeleterId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("GenderRequirement")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifierId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LearnerGender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LearnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LearnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LearningMode")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfLearner")
                        .HasColumnType("int");

                    b.Property<int>("SessionDuration")
                        .HasColumnType("int")
                        .HasColumnName("SessionDuration");

                    b.Property<int>("SessionPerWeek")
                        .HasColumnType("int")
                        .HasColumnName("SessionPerWeek");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid?>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LearnerId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.Discoveries.Discovery", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Discovery", (string)null);
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.DiscoveryUsers.DiscoveryUser", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<int>("DiscoveryId")
                        .HasColumnType("int")
                        .HasColumnName("DiscoveryId");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("DiscoveryId");

                    b.HasIndex("UserId");

                    b.ToTable("DiscoveryUser", (string)null);
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.Notifications.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifierId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("NotificationType")
                        .HasColumnType("int");

                    b.Property<string>("ObjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notification", (string)null);
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.Subjects.Subject", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeleterId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifierId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Subject", (string)null);
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.Subscribers.Subscriber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TutorId");

                    b.HasKey("Id");

                    b.ToTable("Subscriber", (string)null);
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.TutorRequests.TutorRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifierId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LearnerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("LearnerId");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("RequestStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TutorId");

                    b.HasKey("Id");

                    b.HasIndex("LearnerId");

                    b.HasIndex("TutorId");

                    b.ToTable("TutorRequest", (string)null);
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.Tutors.Tutor", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<int>("AcademicLevel")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifierId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Rate")
                        .HasColumnType("real");

                    b.Property<string>("University")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tutor", (string)null);
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.Users.Identities.IdentityRole", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IdentityRole", (string)null);
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.Users.Identities.IdentityUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("IdentityRoleId")
                        .HasColumnType("int")
                        .HasColumnName("IdentityRoleId");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varbinary(128)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("IdentityRoleId");

                    b.ToTable("IdentityUser", (string)null);
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BirthYear")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeleterId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifierId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.Courses.Course", b =>
                {
                    b.HasOne("ESCenter.Domain.Aggregates.Users.User", null)
                        .WithMany()
                        .HasForeignKey("LearnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESCenter.Domain.Aggregates.Subjects.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ESCenter.Domain.Aggregates.Courses.ValueObjects.Fee", "ChargeFee", b1 =>
                        {
                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<float>("Amount")
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("real")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Currency");

                            b1.HasKey("CourseId");

                            b1.ToTable("Course");

                            b1.WithOwner()
                                .HasForeignKey("CourseId");
                        });

                    b.OwnsOne("ESCenter.Domain.Aggregates.Courses.ValueObjects.Fee", "SectionFee", b1 =>
                        {
                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<float>("Amount")
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("real")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Currency");

                            b1.HasKey("CourseId");

                            b1.ToTable("Course");

                            b1.WithOwner()
                                .HasForeignKey("CourseId");
                        });

                    b.OwnsMany("ESCenter.Domain.Aggregates.Courses.CourseRequests.CourseRequest", "CourseRequests", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("Id");

                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreationTime")
                                .HasColumnType("datetime2");

                            b1.Property<string>("CreatorId")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("nvarchar(128)");

                            b1.Property<DateTime?>("LastModificationTime")
                                .HasColumnType("datetime2");

                            b1.Property<string>("LastModifierId")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("RequestStatus")
                                .HasColumnType("int");

                            b1.Property<Guid>("TutorId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("TutorId");

                            b1.HasKey("Id");

                            b1.HasIndex("CourseId");

                            b1.HasIndex("TutorId");

                            b1.ToTable("CourseRequest", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CourseId");

                            b1.HasOne("ESCenter.Domain.Aggregates.Tutors.Tutor", null)
                                .WithMany()
                                .HasForeignKey("TutorId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();
                        });

                    b.OwnsOne("ESCenter.Domain.Aggregates.Courses.Review", "Review", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Detail")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<short>("Rate")
                                .HasColumnType("smallint");

                            b1.HasKey("Id");

                            b1.HasIndex("CourseId")
                                .IsUnique();

                            b1.ToTable("Review", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CourseId");
                        });

                    b.Navigation("ChargeFee")
                        .IsRequired();

                    b.Navigation("CourseRequests");

                    b.Navigation("Review");

                    b.Navigation("SectionFee")
                        .IsRequired();
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.Discoveries.Discovery", b =>
                {
                    b.HasOne("ESCenter.Domain.Aggregates.Subjects.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("ESCenter.Domain.Aggregates.Discoveries.Entities.DiscoverySubject", "DiscoverySubjects", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<int>("DiscoveryId")
                                .HasColumnType("int");

                            b1.Property<int>("SubjectId")
                                .HasColumnType("int")
                                .HasColumnName("SubjectId");

                            b1.Property<string>("SubjectName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Id");

                            b1.HasIndex("DiscoveryId");

                            b1.ToTable("DiscoverySubject", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("DiscoveryId");
                        });

                    b.Navigation("DiscoverySubjects");
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.DiscoveryUsers.DiscoveryUser", b =>
                {
                    b.HasOne("ESCenter.Domain.Aggregates.Discoveries.Discovery", null)
                        .WithMany()
                        .HasForeignKey("DiscoveryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESCenter.Domain.Aggregates.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.TutorRequests.TutorRequest", b =>
                {
                    b.HasOne("ESCenter.Domain.Aggregates.Users.User", null)
                        .WithMany()
                        .HasForeignKey("LearnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESCenter.Domain.Aggregates.Tutors.Tutor", null)
                        .WithMany()
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.Tutors.Tutor", b =>
                {
                    b.HasOne("ESCenter.Domain.Aggregates.Users.User", null)
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsMany("ESCenter.Domain.Aggregates.Tutors.Entities.ChangeVerificationRequest", "ChangeVerificationRequests", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<int>("RequestStatus")
                                .HasColumnType("int");

                            b1.Property<Guid>("TutorId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("TutorId");

                            b1.HasKey("Id");

                            b1.HasIndex("TutorId");

                            b1.ToTable("ChangeVerificationRequest", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TutorId");

                            b1.OwnsMany("ESCenter.Domain.Aggregates.Tutors.Entities.ChangeVerificationRequestDetail", "ChangeVerificationRequestDetails", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b2.Property<int>("Id"));

                                    b2.Property<int>("ChangeVerificationRequestId")
                                        .HasColumnType("int");

                                    b2.Property<string>("ImageUrl")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("Id");

                                    b2.HasIndex("ChangeVerificationRequestId");

                                    b2.ToTable("ChangeVerificationRequestDetail", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("ChangeVerificationRequestId");
                                });

                            b1.Navigation("ChangeVerificationRequestDetails");
                        });

                    b.OwnsMany("ESCenter.Domain.Aggregates.Tutors.Entities.TutorMajor", "TutorMajors", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("Id");

                            b1.Property<int>("SubjectId")
                                .HasColumnType("int")
                                .HasColumnName("SubjectId");

                            b1.Property<string>("SubjectName")
                                .IsRequired()
                                .HasMaxLength(32)
                                .HasColumnType("nvarchar(32)");

                            b1.Property<Guid>("TutorId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("SubjectId");

                            b1.HasIndex("TutorId");

                            b1.ToTable("TutorMajor", (string)null);

                            b1.HasOne("ESCenter.Domain.Aggregates.Subjects.Subject", null)
                                .WithMany()
                                .HasForeignKey("SubjectId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("TutorId");
                        });

                    b.OwnsMany("ESCenter.Domain.Aggregates.Tutors.Entities.TutorVerificationInfo", "TutorVerificationInfos", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Image")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<Guid>("TutorId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("TutorId");

                            b1.ToTable("TutorVerificationInfo", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TutorId");
                        });

                    b.Navigation("ChangeVerificationRequests");

                    b.Navigation("TutorMajors");

                    b.Navigation("TutorVerificationInfos");
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.Users.Identities.IdentityUser", b =>
                {
                    b.HasOne("ESCenter.Domain.Aggregates.Users.Identities.IdentityRole", "IdentityRole")
                        .WithMany()
                        .HasForeignKey("IdentityRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ESCenter.Domain.Aggregates.Users.ValueObjects.OtpCode", "OtpCode", b1 =>
                        {
                            b1.Property<Guid>("IdentityUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("ExpiredTime")
                                .HasColumnType("datetime2")
                                .HasColumnName("ExpiredTime");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(6)
                                .HasColumnType("nvarchar(6)")
                                .HasColumnName("OtpCode");

                            b1.HasKey("IdentityUserId");

                            b1.ToTable("IdentityUser");

                            b1.WithOwner()
                                .HasForeignKey("IdentityUserId");
                        });

                    b.Navigation("IdentityRole");

                    b.Navigation("OtpCode")
                        .IsRequired();
                });

            modelBuilder.Entity("ESCenter.Domain.Aggregates.Users.User", b =>
                {
                    b.OwnsOne("ESCenter.Domain.Aggregates.Users.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Country");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

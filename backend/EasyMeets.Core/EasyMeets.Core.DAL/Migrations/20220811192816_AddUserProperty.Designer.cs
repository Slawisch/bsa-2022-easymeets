﻿// <auto-generated />
using System;
using EasyMeets.Core.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EasyMeets.Core.DAL.Migrations
{
    [DbContext(typeof(EasyMeetsCoreContext))]
    [Migration("20220811192816_AddUserProperty")]
    partial class AddUserProperty
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.AdvancedSlotSettings", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("ActivityType")
                        .HasColumnType("int");

                    b.Property<long>("AvailabilitySlotId")
                        .HasColumnType("bigint");

                    b.Property<int>("BookingScheduleBlockingTimeMeetingInHours")
                        .HasColumnType("int");

                    b.Property<int>("Color")
                        .HasColumnType("int");

                    b.Property<int>("Days")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MaxNumberOfBookings")
                        .HasColumnType("int");

                    b.Property<int>("PaddingBeforeMeeting")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("AvailabilitySlotId")
                        .IsUnique();

                    b.ToTable("AdvancedSlotSettings");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.AvailabilitySlot", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("AdvancedSlotSettingsId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<long>("LocationId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<long>("TeamId")
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("LocationId");

                    b.HasIndex("TeamId");

                    b.ToTable("AvailabilitySlots");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.Calendar", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("AddEventsFromTeamId")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<bool>("CheckForConflicts")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AddEventsFromTeamId");

                    b.HasIndex("UserId");

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.CalendarVisibleForTeam", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("CalendarId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("TeamId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CalendarId");

                    b.HasIndex("TeamId");

                    b.ToTable("CalendarVisibleForTeams");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.ExternalAttendee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AvailabilitySlotId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTimeOffset>("EventTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AvailabilitySlotId");

                    b.ToTable("ExternalAttendees");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.ExternalAttendeeAvailability", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTimeOffset>("EndEvent")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("ExternalAttendeeId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("StartEvent")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("ExternalAttendeeId");

                    b.ToTable("ExternalAttendeeAvailabilities");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.Location", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.Meeting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("LocationId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("TeamId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("LocationId");

                    b.HasIndex("TeamId");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AvailabilitySlotId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("AvailabilitySlotId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.Sample", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Samples");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.Team", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LogoPath")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PageLink")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.TeamMember", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long>("TeamId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("TeamMembers");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.TeamMemberMeeting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("EventId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("MemberId")
                        .HasColumnType("bigint");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("MemberId");

                    b.ToTable("TeamMemberMeetings");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("Country")
                        .HasColumnType("int");

                    b.Property<int>("DateFormat")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("TimeFormat")
                        .HasColumnType("int");

                    b.Property<string>("TimeZone")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.UserSlot", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AvailabilitySlotId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AvailabilitySlotId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSlot");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.AdvancedSlotSettings", b =>
                {
                    b.HasOne("EasyMeets.Core.DAL.Entities.AvailabilitySlot", "AvailabilitySlot")
                        .WithOne("AdvancedSlotSettings")
                        .HasForeignKey("EasyMeets.Core.DAL.Entities.AdvancedSlotSettings", "AvailabilitySlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AvailabilitySlot");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.AvailabilitySlot", b =>
                {
                    b.HasOne("EasyMeets.Core.DAL.Entities.User", "Author")
                        .WithMany("CreatedSlots")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EasyMeets.Core.DAL.Entities.Location", "Location")
                        .WithMany("AvailabilitySlots")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EasyMeets.Core.DAL.Entities.Team", "Team")
                        .WithMany("AvailabilitySlots")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Location");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.Calendar", b =>
                {
                    b.HasOne("EasyMeets.Core.DAL.Entities.Team", "ImportEventsFromTeam")
                        .WithMany("ExportEventsToCalendars")
                        .HasForeignKey("AddEventsFromTeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EasyMeets.Core.DAL.Entities.User", "User")
                        .WithMany("Calendars")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImportEventsFromTeam");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.CalendarVisibleForTeam", b =>
                {
                    b.HasOne("EasyMeets.Core.DAL.Entities.Calendar", "Calendar")
                        .WithMany("VisibleForTeams")
                        .HasForeignKey("CalendarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyMeets.Core.DAL.Entities.Team", "Team")
                        .WithMany("VisibleCalendars")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calendar");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.ExternalAttendee", b =>
                {
                    b.HasOne("EasyMeets.Core.DAL.Entities.AvailabilitySlot", "AvailabilitySlot")
                        .WithMany("ExternalAttendees")
                        .HasForeignKey("AvailabilitySlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AvailabilitySlot");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.ExternalAttendeeAvailability", b =>
                {
                    b.HasOne("EasyMeets.Core.DAL.Entities.ExternalAttendee", "ExternalAttendee")
                        .WithMany("ExternalAttendeeAvailabilities")
                        .HasForeignKey("ExternalAttendeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExternalAttendee");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.Meeting", b =>
                {
                    b.HasOne("EasyMeets.Core.DAL.Entities.User", "Author")
                        .WithMany("CreatedMeetings")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EasyMeets.Core.DAL.Entities.Location", "Location")
                        .WithMany("Meetings")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EasyMeets.Core.DAL.Entities.Team", "Team")
                        .WithMany("Meetings")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Location");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.Question", b =>
                {
                    b.HasOne("EasyMeets.Core.DAL.Entities.AvailabilitySlot", "AvailabilitySlot")
                        .WithMany("Questions")
                        .HasForeignKey("AvailabilitySlotId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AvailabilitySlot");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.TeamMember", b =>
                {
                    b.HasOne("EasyMeets.Core.DAL.Entities.Team", "Team")
                        .WithMany("TeamMembers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyMeets.Core.DAL.Entities.User", "User")
                        .WithMany("TeamMembers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.TeamMemberMeeting", b =>
                {
                    b.HasOne("EasyMeets.Core.DAL.Entities.Meeting", "Event")
                        .WithMany("TeamMeetings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyMeets.Core.DAL.Entities.User", "User")
                        .WithMany("TeamMeetings")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.UserSlot", b =>
                {
                    b.HasOne("EasyMeets.Core.DAL.Entities.AvailabilitySlot", "AvailabilitySlot")
                        .WithMany("Members")
                        .HasForeignKey("AvailabilitySlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyMeets.Core.DAL.Entities.User", "User")
                        .WithMany("Slots")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AvailabilitySlot");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.AvailabilitySlot", b =>
                {
                    b.Navigation("AdvancedSlotSettings")
                        .IsRequired();

                    b.Navigation("ExternalAttendees");

                    b.Navigation("Members");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.Calendar", b =>
                {
                    b.Navigation("VisibleForTeams");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.ExternalAttendee", b =>
                {
                    b.Navigation("ExternalAttendeeAvailabilities");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.Location", b =>
                {
                    b.Navigation("AvailabilitySlots");

                    b.Navigation("Meetings");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.Meeting", b =>
                {
                    b.Navigation("TeamMeetings");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.Team", b =>
                {
                    b.Navigation("AvailabilitySlots");

                    b.Navigation("ExportEventsToCalendars");

                    b.Navigation("Meetings");

                    b.Navigation("TeamMembers");

                    b.Navigation("VisibleCalendars");
                });

            modelBuilder.Entity("EasyMeets.Core.DAL.Entities.User", b =>
                {
                    b.Navigation("Calendars");

                    b.Navigation("CreatedMeetings");

                    b.Navigation("CreatedSlots");

                    b.Navigation("Slots");

                    b.Navigation("TeamMeetings");

                    b.Navigation("TeamMembers");
                });
#pragma warning restore 612, 618
        }
    }
}

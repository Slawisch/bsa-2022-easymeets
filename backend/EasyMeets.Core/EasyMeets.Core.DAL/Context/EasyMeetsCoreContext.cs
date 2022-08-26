﻿using EasyMeets.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyMeets.Core.DAL.Context
{
    public class EasyMeetsCoreContext : DbContext
    {
        public DbSet<Sample> Samples { get; private set; }
        public DbSet<AdvancedSlotSettings> AdvancedSlotSettings { get; private set; }
        public DbSet<AvailabilitySlot> AvailabilitySlots { get; private set; }
        public DbSet<Calendar> Calendars { get; private set; }
        public DbSet<CalendarVisibleForTeam> CalendarVisibleForTeams { get; private set; }
        public DbSet<ExternalAttendee> ExternalAttendees { get; private set; }
        public DbSet<ExternalAttendeeAvailability> ExternalAttendeeAvailabilities { get; private set; }
        public DbSet<Meeting> Meetings { get; private set; } 
        public DbSet<MeetingMember> MeetingMembers { get; private set; } 
        public DbSet<Question> Questions { get; private set; }
        public DbSet<Team> Teams { get; private set; }
        public DbSet<TeamMember> TeamMembers { get; private set; }
        public DbSet<SlotMember> SlotMembers { get; private set; }
        public DbSet<User> Users { get; private set; } 
        public DbSet<Schedule> Schedules { get; private set; }
        public DbSet<ScheduleItem> ScheduleItems { get; private set; }


        public EasyMeetsCoreContext(DbContextOptions<EasyMeetsCoreContext> options) : base(options)
        {
            Samples = Set<Sample>();
            AdvancedSlotSettings = Set<AdvancedSlotSettings>();
            AvailabilitySlots = Set<AvailabilitySlot>();
            Calendars = Set<Calendar>();
            CalendarVisibleForTeams = Set<CalendarVisibleForTeam>();
            ExternalAttendees = Set<ExternalAttendee>();
            ExternalAttendeeAvailabilities = Set<ExternalAttendeeAvailability>();
            Meetings = Set<Meeting>();
            Questions = Set<Question>();
            Teams = Set<Team>();
            TeamMembers = Set<TeamMember>();
            SlotMembers = Set<SlotMember>();
            Users = Set<User>(); 
            Schedules = Set<Schedule>();
            ScheduleItems = Set<ScheduleItem>();
            MeetingMembers = Set<MeetingMember>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Configure();
            modelBuilder.Seed();
        }
    }
}

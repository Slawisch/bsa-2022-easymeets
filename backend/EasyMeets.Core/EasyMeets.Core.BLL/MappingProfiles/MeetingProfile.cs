﻿using AutoMapper;
using EasyMeets.Core.Common.DTO.Meeting;
using EasyMeets.Core.Common.DTO.Zoom;
using EasyMeets.Core.DAL.Entities;

namespace EasyMeets.Core.BLL.MappingProfiles
{
    public sealed class MeetingProfile : Profile
    {
        public MeetingProfile()
        {
            CreateMap<User, UserMeetingDTO>()
                .ForMember(d => d.TimeZone, s => s.MapFrom(s => s.TimeZone.ToString()));
            CreateMap<ExternalAttendee, UserMeetingDTO>()
                .ForMember(d => d.TimeZone, s => s.MapFrom(s => s.TimeZone.ToString()));;
            CreateMap<Meeting, MeetingThreeMembersDTO>()
                .ForMember(dest => dest.MeetingTime, src => src.MapFrom(meeting =>
                    $"{meeting.StartTime.Hour}:{meeting.StartTime.Minute:00} - " +
                    $"{meeting.StartTime.AddMinutes(meeting.Duration).Hour}:{meeting.StartTime.AddMinutes(meeting.Duration).Minute:00}"))
                .ForMember(dest => dest.MeetingTitle, src => src.MapFrom(s => s.Name))
                .ForMember(dest => dest.MeetingDuration, src => src.MapFrom(s => $"{s.Duration} min"))
                .ForMember(dest => dest.MembersTitle, src => src.MapFrom(s => CreateMemberTitle(s)))
                .ForMember(dest => dest.MeetingLink, src => src.MapFrom(s => s.MeetingLink))
                .ForMember(dest => dest.MeetingMembers, src => src.MapFrom(s => GetThreeMembersForMeeting(s)))
                .ForMember(dest => dest.MeetingCount, src => src.MapFrom(s => GetAllParticipants(s).Count()))
                .ForMember(dest => dest.Location, src => src.MapFrom(s => s.LocationType.ToString()));
            CreateMap<SaveMeetingDto, Meeting>();

            CreateMap<Meeting, NewZoomMeetingDto>()
                .ForMember(m => m.Agenda, o
                    => o.MapFrom(s => s.Name))
                .ForMember(m => m.ScheduleFor, o =>
                    o.MapFrom(s => s.Author.Email))
                .AfterMap((src, dest) =>
                {
                    dest.Settings.ContactName = src.Author.Name;
                    dest.Settings.ContactEmail = src.Author.Email;
                });

            CreateMap<ExternalAttendeeMeetingDto, Meeting>();
        }

        private string CreateMemberTitle(Meeting meeting)
        {
            return meeting.MeetingMembers.Count() switch
            {
                0 => "Empty meeting.",
                1 => meeting.MeetingMembers.First().TeamMember.User.Name,
                _ => $"{meeting.MeetingMembers.Count()} + Team Members"
            };
        }

        private List<UserMeetingDTO> GetThreeMembersForMeeting(Meeting meeting)
        {
            return GetAllParticipants(meeting).Take(3).ToList();
        }

        private IEnumerable<UserMeetingDTO> GetAllParticipants(Meeting meeting)
        {
            var slotMembers = meeting.MeetingMembers
                .Select(x => new UserMeetingDTO 
                    { Name = x.TeamMember.User.Name, Email = x.TeamMember.User.Email, TimeZone = x.TeamMember.User.TimeZone.ToString() }).ToList();

            if (meeting.AvailabilitySlot is not null)
            {
                var external = meeting.AvailabilitySlot.ExternalAttendees
                    .Select(x => new UserMeetingDTO
                        { Name = x.Name, Email = x.Email, TimeZone = x.TimeZone.ToString() }).ToList();

                return slotMembers.Union(external).ToList();
            }

            return slotMembers;
        }
    }
}
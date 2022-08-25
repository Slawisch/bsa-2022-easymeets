﻿using EasyMeets.Core.Common.DTO.Meeting;

namespace EasyMeets.Core.BLL.Interfaces
{
    public interface IMeetingService
    {
        public Task<List<MeetingThreeMembersDTO>> GetThreeMeetingMembersAsync();
        Task<List<UserMeetingDTO>> GetAllMembers(int id);
    }
}

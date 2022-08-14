﻿using AutoMapper;
using EasyMeets.Core.Common.DTO.Team;
using EasyMeets.Core.DAL.Entities;

namespace EasyMeets.Core.BLL.MappingProfiles;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<TeamDto, Team>();
        CreateMap<Team, TeamDto>()
            .ForMember(dto => dto.Members,
                opt => opt.MapFrom(src => src.TeamMembers.Select(member => member.User)));
    }
}
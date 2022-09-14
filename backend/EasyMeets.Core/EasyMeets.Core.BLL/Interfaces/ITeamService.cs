using EasyMeets.Core.Common.DTO.Team;
using EasyMeets.Core.Common.DTO.UploadImage;
using EasyMeets.Core.Common.Enums;
using Microsoft.AspNetCore.Http;
namespace EasyMeets.Core.BLL.Interfaces;

public interface ITeamService
{
    Task<List<TeamDto>> GetCurrentUserTeams();
    Task<TeamDto?> GetTeamAsync(long teamId);
    Task<bool> ValidatePageLinkAsync(long? teamId, string pageLink);
    Task<TeamDto> CreateTeamAsync(NewTeamDto teamDto);
    Task UpdateTeamAsync(UpdateTeamDto team);
    Task DeleteTeamAsync(long teamId);
    Task<ImagePathDto> UploadLogoAsync(IFormFile file, long? teamId);
    Task<ICollection<NewMeetingMemberDto>> GetTeamMembersByNameAsync(string searchName, long? teamId);
    Task<NewMeetingMemberDto> GetCurrentUserAsTeamMemberAsync(long userId, long teamId);
    Task<List<TeamMemberDto>> GetTeamMembersAsync(long id);
    Task CreateTeamMemberAsync(TeamMemberDto teamMemberDto, long teamId);
    Task UpdateTeamMemberRoleAsync(TeamMemberDto teamMemberDto);
    Task DeleteTeamMemberAsync(long teamMemberId);
}

﻿using EasyMeets.Core.BLL.Interfaces;
using EasyMeets.Core.Common.DTO.Team;
using EasyMeets.Core.WebAPI.Validators.Team;
using Microsoft.AspNetCore.Mvc;
namespace EasyMeets.Core.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;
    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeamDto>> GetTeamById(long id)
    {
        var teamDto = await _teamService.GetTeamAsync(id);
        if (teamDto is null)
        {
            return NotFound();
        }
        return Ok(teamDto);
    }

    [HttpGet("newpagelink")]
    public async Task<ActionResult<string>> GenerateNewPageLinkAsync(long id, string teamname)
    {
        return Ok(await _teamService.GenerateNewPageLinkAsync(id, teamname));
    }

    [HttpPost]
    public async Task<ActionResult<TeamDto>> CreateAsync(NewTeamDto newTeamDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var sample = await _teamService.CreateTeamAsync(newTeamDto);
        return Ok(sample);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTeam([FromBody] TeamDto teamDto)
    {
        await _teamService.UpdateTeamAsync(teamDto);
        return Ok();
    }

    [HttpDelete("{teamId}")]
    public async Task<ActionResult> DeleteAsync(int teamId)
    {
        await _teamService.DeleteTeamAsync(teamId);
        return NoContent();
    }
}

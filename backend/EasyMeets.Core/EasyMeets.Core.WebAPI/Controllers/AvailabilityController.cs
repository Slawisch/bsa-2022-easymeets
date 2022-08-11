﻿using EasyMeets.Core.BLL.Interfaces; 
using EasyMeets.Core.Common.DTO.Availability;
using EasyMeets.Core.Common.DTO.Availability.NewAvailability; 
using Microsoft.AspNetCore.Mvc;

namespace EasyMeets.Core.WebAPI.Controllers
{
    //TODO: return Authorize
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;
        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAvailabilitySlot([FromBody] NewAvailabilitySlotDto slotDto)
        {
            await _availabilityService.CreateAvailabilitySlot(slotDto);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ICollection<AvailabilitySlotDto>>> GetAllAvailabilitySlotsForTeamAsync(int id)
        {
            var availabilitySlots = await _availabilityService.GetAllAvailabilitySlotsGroupByTeamsAsync(id);
            return Ok(availabilitySlots);
        }

        [HttpGet("personal/{id}")]
        public async Task<ActionResult<ICollection<AvailabilitySlotDto>>> GetAllUserAvailabilitySlotsAsync(int id)
        {
            var availabilitySlots = await _availabilityService.GetAllUserAvailabilitySlotsAsync(id);
            return Ok(availabilitySlots);
        }
    }
}

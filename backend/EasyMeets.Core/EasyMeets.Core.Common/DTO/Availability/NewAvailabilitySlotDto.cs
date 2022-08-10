﻿namespace EasyMeets.Core.Common.DTO.Availability;

public class NewAvailabilitySlotDto
{
    public long TeamId { get; set; }
    public long LocationId { get; set; }
    public long? AdvancedSlotSettingsId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public int Type { get; set; }
    public int Size { get; set; }
    public int Frequency { get; set; }
    public bool IsEnabled { get; set; }
    public bool IsVisible { get; set; }
    public AdvancedSlotSettingsDto? AdvancedSettings { get; set; }
}
using AutoMapper;
using EasyMeets.Core.BLL.Helpers;
using EasyMeets.Core.BLL.Interfaces;
using EasyMeets.Core.Common.DTO.Calendar;
using EasyMeets.Core.Common.DTO.User;
using EasyMeets.Core.DAL.Context;
using EasyMeets.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EasyMeets.Core.BLL.Services
{
    public class CalendarsService : BaseService, ICalendarsService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IGoogleOAuthService _googleOAuthService;
        private readonly IMeetingService _meetingService;
        public CalendarsService(EasyMeetsCoreContext context, IMapper mapper, IUserService userService, IConfiguration configuration, 
            IGoogleOAuthService googleOAuthService, IMeetingService meetingService) : base(context, mapper)
        {
            _configuration = configuration;
            _userService = userService;
            _googleOAuthService = googleOAuthService;
            _meetingService = meetingService;
        }

        public async Task<bool> CreateGoogleCalendarConnection(TokenResultDto tokenResultDto, UserDto currentUser)
        {
            var response = await HttpClientHelper.SendGetRequest<GoogleCalendarDto>($"{_configuration["GoogleCalendar:GetCalendarAPI"]}/primary", null,
                tokenResultDto.AccessToken);

            var connectedEmail = response.Id;

            if (await _context.Calendars.AnyAsync(el => el.ConnectedCalendar == connectedEmail))
            {
                throw new ArgumentException($"Calendar {connectedEmail} is already connected!");
            }

            var calendar = new Calendar
            {
                UserId = currentUser.Id,
                CheckForConflicts = false,
                ConnectedCalendar = connectedEmail,
                AccessToken = tokenResultDto.AccessToken,
                RefreshToken = tokenResultDto.RefreshToken,
                Uid = Environment.GetEnvironmentVariable("codeVerifier")!,
                CreatedBy = currentUser.Id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Calendars.Add(calendar);
            await _context.SaveChangesAsync();

            var synced = await _context.SyncGoogleCalendar.AnyAsync(x => x.Email == connectedEmail);

            if (!synced)
            {
                return true;
            }

            await SubscribeOnCalendarChanges(tokenResultDto, connectedEmail);
            await _context.SyncGoogleCalendar.AddAsync(new SyncGoogleCalendar { Email = connectedEmail });
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task SubscribeOnCalendarChanges(TokenResultDto tokenResultDto, string connectedEmail)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "calendarId", "primary" }
            };

            var emailName = connectedEmail.Split('@')[0];

            var body = new
            {
                id = emailName,
                type = "web_hook",
                address = _configuration["GoogleCalendar:WebHookCalendarUrl"]
            };

            await HttpClientHelper.SendPostTokenRequest<SubscribeEventDTO>($"{_configuration["GoogleCalendar:SubscribeOnEventsCalendar"]}", queryParams, body,
                tokenResultDto.AccessToken);
        }

        public async Task<List<EventItemDTO>> GetEventsFromGoogleCalendar(string email)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "calendarId", email }
            };

            var refreshToken = _context.Calendars.FirstOrDefault(x => x.ConnectedCalendar == email) ?? throw new Exception("Connected email don't have refresh token.");

            var tokenResultDto = await _googleOAuthService.RefreshToken(refreshToken.RefreshToken);

            var response = await HttpClientHelper.SendGetRequest<CalendarEventsDTO>($"{_configuration["GoogleCalendar:GetEventsFromGoogleCalendar"]}", queryParams,
                tokenResultDto.AccessToken);

            if (response.Items is null)
            {
                return new List<EventItemDTO>();
            }

            var events = response.Items.Where(x => x.Start is not null && x.End is not null && x.Start.DateTime > DateTime.Now).ToList();

            return events;
        }

        public async Task<bool> UpdateGoogleCalendar(List<UserCalendarDto> calendarDtoList)
        {
            foreach (var calendarDto in calendarDtoList)
            {
                var calendar = await _context.Calendars
                    .Include(c => c.ImportEventsFromTeam)
                    .Include(c => c.VisibleForTeams)
                    .FirstOrDefaultAsync(el => el.Id == calendarDto.Id);

                await UpdateVisibleForTeamsTable(calendar!, calendarDto);

                calendar!.CheckForConflicts = calendarDto.CheckForConflicts;
                calendar.AddEventsFromTeamId = calendarDto.ImportEventsFromTeam?.Id;

                if (calendar.AddEventsFromTeamId is not null)
                {
                    await AddMeetingsToCalendar(calendar.AddEventsFromTeamId);
                }

                _context.Calendars.Update(calendar);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<List<UserCalendarDto>> GetCurrentUserCalendars()
        {
            var currentUser = await _userService.GetCurrentUserAsync();

            var calendarsList = await _context.Calendars
                .Where(c => c.UserId == currentUser.Id)
                .Include(c => c.User)
                .Include(c => c.ImportEventsFromTeam)
                .Include(c => c.VisibleForTeams)
                    .ThenInclude(v => v.Team)
                .ToListAsync();

            return _mapper.Map<List<UserCalendarDto>>(calendarsList);
        }

        public async Task<bool> DeleteCalendar(long id)
        {
            var calendar = await _context.Calendars
                .Include(el => el.VisibleForTeams)
                .FirstOrDefaultAsync(el => el.Id == id);

            if (calendar is not null)
            {
                _context.Calendars.Remove(calendar);
                await _context.SaveChangesAsync();
                return true;
            }

            throw new ArgumentException("Calendar doesn't exist in database");
        }

        private async Task UpdateVisibleForTeamsTable(Calendar calendar, UserCalendarDto calendarDto)
        {
            _context.CalendarVisibleForTeams.RemoveRange(calendar!.VisibleForTeams);

            await RemoveCalendarMeetings(calendar.VisibleForTeams);

            calendar.VisibleForTeams = Array.Empty<CalendarVisibleForTeam>();

            var newVisibleForList = calendarDto.VisibleForTeams?
                .Select(el => new CalendarVisibleForTeam
                {
                    CalendarId = calendar.Id,
                    TeamId = el.Id,
                    IsDeleted = false,
                }).ToList();

            if (newVisibleForList is not null)
            {
                calendar.VisibleForTeams = newVisibleForList;
                await _context.CalendarVisibleForTeams.AddRangeAsync(newVisibleForList);
            }
        }

        public async Task<bool> SyncChangesFromGoogleCalendar(string email)
        {
            var calendar = await _context.Calendars.FirstOrDefaultAsync(x => x.ConnectedCalendar == email);

            if (calendar is null)
            {
                return false;
            }

            var visibleCalendar = await _context.CalendarVisibleForTeams.Where(x => x.CalendarId == calendar.Id).ToListAsync();

            await RemoveCalendarMeetings(visibleCalendar);
            await AddMeetingsFromCalendar(email, calendar.VisibleForTeams, calendar.UserId);

            return true;
        }

        private async Task RemoveCalendarMeetings(IEnumerable<CalendarVisibleForTeam> visibleCalendar)
        {
            foreach (var item in visibleCalendar.ToList())
            {
                await _meetingService.DeleteGoogleCalendarMeetings(item.TeamId);
            }
        }

        private async Task AddMeetingsFromCalendar(string email, IEnumerable<CalendarVisibleForTeam> visibleCalendar, long userId)
        {
            var events = await GetEventsFromGoogleCalendar(email);

            foreach (var item in visibleCalendar.ToList())
            {
                await _meetingService.AddGoogleCalendarMeetings(item.TeamId, events, userId);
            }
        }
        private async Task AddMeetingsToCalendar(long? teamId)
        {
            var meetings = await _context.Meetings.Where(x => x.TeamId == teamId).ToListAsync();

            if (meetings.Any())
            {
                foreach (var item in meetings)
                {
                    await AddMeetingToCalendar(item);
                }
            }
        }

        private async Task AddMeetingToCalendar(Meeting meeting)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "calendarId", "primary" }
            };

            var starttime = meeting.StartTime.DateTime.AddHours(12).ToString("yyyy-MM-dd HH:mm").Replace(" ", "T") + ":00" + "-09:00";
            var endtime = meeting.StartTime.DateTime.AddMinutes(meeting.Duration).AddHours(12).ToString("yyyy-MM-dd HH:mm").Replace(" ", "T") + ":00" + "-09:00";

            var body = new
            {
                summary = meeting.Name,
                status = "confirmed",
                end = new
                {
                    dateTime = DateTime.Parse(endtime)
                },
                start = new
                {
                    dateTime = DateTime.Parse(starttime)
                }
            };

            var refreshToken = _context.Calendars.FirstOrDefault(x => x.ConnectedCalendar == "doofeee@gmail.com") ?? throw new Exception("Connected email don't have refresh token.");

            var tokenResultDto = await _googleOAuthService.RefreshToken(refreshToken.RefreshToken);

            await HttpClientHelper.SendPostTokenRequest<SubscribeEventDTO>($"https://www.googleapis.com/calendar/v3/calendars/calendarId/events", queryParams, body,
                tokenResultDto.AccessToken);
        }
    }
}
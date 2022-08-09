namespace EasyMeets.Core.DAL.Entities;

public class Meeting : AuditEntity<long>
{
    public Meeting()
    {
        Team = new Team();
        Author = new User();
        Location = new Location();
        TeamMeetings = new List<TeamMemberMeeting>();
    }
    public long TeamId { get; set; }
    public long LocationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Duration { get; set; }
    public DateTimeOffset StartTime { get; set; }
    
    public Team Team { get; set; }
    public User Author { get; set; }
    public Location Location { get; set; }
    public ICollection<TeamMemberMeeting> TeamMeetings { get; set; }
}

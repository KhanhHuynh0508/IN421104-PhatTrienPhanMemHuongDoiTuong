using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public class ClubStat
{
    public string ClubName { get; set; } = string.Empty;
    public int Members { get; set; }
}

// --- DESIGN PATTERN: STRATEGY ---
public interface IReportStrategy
{
    string Name { get; }
    string Render(IReadOnlyList<ClubStat> stats);
}

public class ConsoleReportStrategy : IReportStrategy
{
    public string Name => "Console";
    public string Render(IReadOnlyList<ClubStat> stats)
    {
        var lines = new List<string>();
        foreach (var s in stats)
        {
            lines.Add($"Club: {s.ClubName} | Members: {s.Members}");
        }
        return string.Join("\n", lines);
    }
}

public class CsvReportStrategy : IReportStrategy
{
    public string Name => "CSV";
    public string Render(IReadOnlyList<ClubStat> stats)
    {
        var lines = new List<string> { "Club,Members" };
        foreach (var s in stats)
        {
            lines.Add($"\"{s.ClubName}\",{s.Members}");
        }
        return string.Join("\n", lines);
    }
}

public class ReportService
{
    private readonly IClubRepository _clubs;
    private readonly IMemberRepository _members;

    public ReportService(IClubRepository clubs, IMemberRepository members)
    {
        _clubs = clubs;
        _members = members;
    }

    public string Generate(IReportStrategy strategy)
    {
        var stats = _clubs.GetAll().Select(c => new ClubStat
        {
            ClubName = c.Name,
            Members = _members.CountByClub(c.Id)
        }).ToList();

        return strategy.Render(stats);
    }
}
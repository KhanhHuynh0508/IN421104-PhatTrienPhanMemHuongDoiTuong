namespace EduHub.Ch02.Domain;

/// <summary>Sự kiện của CLB, kèm danh sách đăng ký và check-in.</summary>
public class Event
{
    public Guid Id { get; set; }
    public Guid ClubId { get; set; }
    public required string Title { get; set; }
    public DateTime Date { get; set; }

    public HashSet<Guid> RegisteredMemberIds { get; } = new();
    public HashSet<Guid> CheckedInMemberIds { get; } = new();

    public bool Register(Guid memberId)
    {
        if (RegisteredMemberIds.Contains(memberId))
        {
            return false; // Đã đăng ký trước đó
        }

        RegisteredMemberIds.Add(memberId);
        return true;
    }

    public void CheckIn(Guid memberId)
    {
        if (!RegisteredMemberIds.Contains(memberId))
        {
            throw new InvalidOperationException("Member must register before checking in.");
        }

        CheckedInMemberIds.Add(memberId);
    }
}

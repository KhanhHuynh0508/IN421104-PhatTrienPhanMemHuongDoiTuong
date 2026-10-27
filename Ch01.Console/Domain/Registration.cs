namespace EduHub.Ch01.Domain;

/// <summary>Phiếu đăng ký của Member cho Event.</summary>
public class Registration
{
    public const string StatusPending = "Pending";
    public const string StatusConfirmed = "Confirmed";
    public const string StatusCancelled = "Cancelled";

    public Guid Id { get; private set; }
    public Guid MemberId { get; private set; }
    public Guid EventId { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public string Status { get; private set; }

    private Registration(Guid memberId, Guid eventId)
    {
        Id = Guid.NewGuid();
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = DateTime.UtcNow;
        Status = StatusPending;
    }

    public static Registration Create(Guid memberId, Guid eventId)
    {
        return new Registration(memberId, eventId);
    }

    public void Confirm()
    {
        if (Status != StatusPending)
        {
            throw new InvalidOperationException($"Không thể xác nhận phiếu đang ở trạng thái ({Status}). Chỉ phiếu Pending mới được xác nhận.");
        }
        Status = StatusConfirmed;
    }

    public void Cancel()
    {
        if (Status == StatusCancelled)
        {
            throw new InvalidOperationException("Phiếu đăng ký này đã bị hủy trước đó.");
        }
        Status = StatusCancelled;
    }
}
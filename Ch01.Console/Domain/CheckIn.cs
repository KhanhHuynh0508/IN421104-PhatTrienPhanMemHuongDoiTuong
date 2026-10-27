namespace EduHub.Ch01.Domain;

/// <summary>Ghi nhận sự hiện diện thực tế của một Registration.</summary>
public class CheckIn
{
    public Guid Id { get; private set; }
    public Guid RegistrationId { get; private set; }
    public DateTime CheckedInAt { get; private set; }
    public string Method { get; private set; }

    private CheckIn(Guid registrationId, string method)
    {
        Id = Guid.NewGuid();
        RegistrationId = registrationId;
        CheckedInAt = DateTime.UtcNow;
        Method = method;
    }

    public static CheckIn Create(Registration registration, string method = "Manual")
    {
        ArgumentNullException.ThrowIfNull(registration);
        if (registration.Status != Registration.StatusConfirmed)
        {
            throw new InvalidOperationException("Chỉ những phiếu đăng ký ở trạng thái Confirmed (Đã xác nhận) mới được phép check-in.");
        }

        return new CheckIn(registration.Id, method);
    }

    public string Summary() => $"[CheckIn] Mã phiếu: {RegistrationId} | Phương thức: {Method} | Thời gian: {CheckedInAt:dd/MM/yyyy HH:mm:ss}";
}

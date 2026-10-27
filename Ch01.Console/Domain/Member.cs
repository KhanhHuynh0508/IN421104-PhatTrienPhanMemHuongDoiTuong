namespace EduHub.Ch01.Domain;

/// <summary>Thành viên sinh viên của câu lạc bộ.</summary>
public class Member
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string StudentCode { get; private set; }
    public DateTime JoinedAt { get; private set; }
    public string Status { get; private set; }

    private Member(string name, string email, string studentCode)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        StudentCode = studentCode;
        JoinedAt = DateTime.UtcNow;
        Status = "Active";
    }

    public static Member Create(string name, string email, string studentCode)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên không được để trống", nameof(name));
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email không hợp lệ (phải chứa ký tự @)", nameof(email));
        if (string.IsNullOrWhiteSpace(studentCode))
            throw new ArgumentException("Mã số sinh viên không được để trống", nameof(studentCode));

        return new Member(name.Trim(), email.Trim().ToLowerInvariant(), studentCode.Trim());
    }

    public bool IsActive() => Status == "Active";

    public override string ToString() => $"{Name} ({StudentCode})";
}

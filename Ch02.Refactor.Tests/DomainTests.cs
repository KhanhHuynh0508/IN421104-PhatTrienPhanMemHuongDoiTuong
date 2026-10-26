using EduHub.Ch02;
using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests;

// Mẫu AAA: Arrange (chuẩn bị) - Act (thực hiện) - Assert (kiểm tra).
// Mỗi test chỉ kiểm tra MỘT hành vi. Đặt tên: TênMethod_TìnhHuống_KếtQuảMongĐợi.
public class DomainTests
{
    // Test mẫu đã viết sẵn: chạy được ngay trên ClubManager hiện tại.
    [Fact]
    public void CreateClub_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var manager = new ClubManager();

        // Act
        var act = () => manager.CreateClub("", "mô tả", "p@gmail.com");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    // TODO (Nhiệm vụ 4): bỏ Skip và viết thân test cho từng trường hợp dưới đây.
    // Cần ≥ 6 test AAA cho lớp Domain (Club, Event), trong đó có cả trường hợp lỗi.

    [Fact]
    public void Club_Create_WithValidName_ReturnsClub()
    {
        // Arrange
        const string name = "Tech Club";
        const string desc = "Câu lạc bộ công nghệ";

        // Act
        var club = Club.Create(name, desc);

        // Assert
        Assert.NotNull(club);
        Assert.Equal(name, club.Name);
        Assert.Equal(desc, club.Description);
        Assert.NotEqual(Guid.Empty, club.Id);
    }

    [Fact]
    public void Club_Create_WithInvalidName_ThrowsArgumentException()
    {
        // Arrange
        string longName = new string('a', 101);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Club.Create("", "desc"));
        Assert.Throws<ArgumentException>(() => Club.Create(longName, "desc"));
    }

    [Fact]
    public void Event_Register_NewMember_ReturnsTrue()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop", Date = DateTime.Now };
        var memberId = Guid.NewGuid();

        // Act
        var result = ev.Register(memberId);

        // Assert
        Assert.True(result);
        Assert.Contains(memberId, ev.RegisteredMemberIds);
    }

    [Fact]
    public void Event_Register_SameMemberTwice_ReturnsFalse()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop", Date = DateTime.Now };
        var memberId = Guid.NewGuid();
        ev.Register(memberId); // Đăng ký lần 1

        // Act
        var result = ev.Register(memberId); // Đăng ký lần 2

        // Assert
        Assert.False(result);
    }


    [Fact]
    public void Event_CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop", Date = DateTime.Now };
        var unregisteredId = Guid.NewGuid();

        // Act
        var act = () => ev.CheckIn(unregisteredId);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }
}

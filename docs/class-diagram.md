# Sơ đồ lớp sơ bộ — EduHub

Vẽ bằng Mermaid (hiển thị trực tiếp trên GitHub) hoặc xuất ảnh từ draw.io rồi đặt cạnh file này.
Ghi rõ thuộc tính chính, khoá ngoại (FK) và bội số quan hệ (1, *, 0..1).

```mermaid
classDiagram
    class Club {
        +Guid Id
        +string Name
        +string Description
        +DateTime CreatedAt
        +IReadOnlyList~Member~ Members
        +Create(string name, string description) Club
        +AddMember(Member member) void
        +MemberCount() int
    }

    class Member {
        +Guid Id
        +string Name
        +string Email
        +string StudentCode
        +DateTime JoinedAt
        +MemberStatus Status
        +Create(string name, string email, string studentCode) Member
        +IsActive() bool
        +ToString() string
    }

    class Event {
        +Guid Id
        +Guid ClubId
        +string Title
        +DateTime Date
        +int Capacity
        +bool IsOnline
        +Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline) Event
        +HasCapacity(int registeredCount) bool
    }

    class Registration {
        +Guid Id
        +Guid MemberId
        +Guid EventId
        +DateTime RegisteredAt
        +string Status
        +Create(Guid memberId, Guid eventId) Registration
        +Confirm() void
        +Cancel() void
    }

    class CheckIn {
        +Guid Id
        +Guid RegistrationId
        +DateTime CheckedInAt
        +string Method
        +Create(Registration registration, string method) CheckIn
        +Summary() string
    }

    %% Quan hệ giữa các lớp và bội số
    Club "1" --> "*" Event : tổ chức (ClubId)
    Member "1" --> "*" Registration : thực hiện (MemberId)
    Event "1" --> "*" Registration : nhận (EventId)
    Registration "1" --> "0..1" CheckIn : điểm danh (RegistrationId)

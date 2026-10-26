# Phân tích vi phạm nguyên lý SOLID trong ClubManager

Lớp `ClubManager` trong mã nguồn khởi đầu là một ví dụ điển hình về **God Class**, cố tình vi phạm các nguyên lý SOLID nhằm phục vụ bài tập Refactoring. Dưới đây là phân tích chi tiết 4 vi phạm chính:

---

## 1. Single Responsibility Principle (SRP) - Nguyên lý đơn trách nhiệm
 **Vị trí trong code:** Phương thức `CreateClub` (và các phương thức khác như `AddMemberToClub`, `CreateEvent`, `PrintClubReport`) trong `ClubManager.cs`.
 **Hành vi vi phạm:** Phương thức `CreateClub` đảm nhận quá nhiều việc cùng một lúc: vừa kiểm tra tính hợp lệ (validate tên CLB), vừa lưu dữ liệu vào danh sách (`_clubs.Add`), vừa ghi file log (`File.AppendAllText`), lại vừa xử lý logic gửi thông báo (kiểm tra email `@gmail.com`, `@yahoo.com` để in ra màn hình).
 **Hậu quả:** Class ôm đè quá nhiều trách nhiệm, dẫn đến code dài, khó đọc, và mỗi khi thay đổi cách ghi log hay cách gửi thông báo thì phải sửa lại class này.

## 2. Open/Closed Principle (OCP) - Nguyên lý đóng/mở
 **Vị trí trong code:** Khối lệnh `if/else` kiểm tra đuôi email (`@gmail.com`, `@yahoo.com`) trong `CreateClub` và việc gửi thông báo ở `AddMemberToClub`.
 **Hành vi vi phạm:** Khi hệ thống muốn hỗ trợ thêm một kênh thông báo mới (ví dụ: kênh *Slack* hoặc *SMS*), lập trình viên buộc phải mở trực tiếp file `ClubManager.cs` ra để viết thêm nhánh `else if`.
 **Hậu quả:** Vi phạm tính "đóng" đối với việc sửa đổi; việc sửa code cũ dễ làm phát sinh lỗi cho các tính năng đang chạy ổn định trước đó.

## 3. Interface Segregation Principle (ISP) / Liskov Substitution (LSP)
 **Vị trí trong code:** Việc quản lý tất cả thao tác dồn vào một chỗ thông qua các danh sách cục bộ (`_clubs`, `_members`) mà không qua các giao diện (interface) tách bạch.
 **Hành vi vi phạm:** Các thành phần cấp cao bị ép phải phụ thuộc vào toàn bộ cấu trúc lưu trữ và thao tác chung chạ, không phân rẽ rõ ràng các chức năng đọc (Read) và ghi (Write) dữ liệu.
 **Hậu quả:** Khó mở rộng, không thể linh hoạt thay thế kho dữ liệu (như đổi từ `InMemory` sang `Entity Framework` hay `ReadOnly`) mà không làm ảnh hưởng đến các lớp khác.

## 4. Dependency Inversion Principle (DIP) - Nguyên lý đảo ngược phụ thuộc
 **Vị trí trong code:** Các thao tác gọi trực tiếp `File.AppendAllText(_logFilePath, ...)` và `Console.WriteLine(...)` nằm ngay bên trong logic nghiệp vụ của `ClubManager`.
 **Hành vi vi phạm:** Lớp cấp cao (`ClubManager`) phụ thuộc trực tiếp vào các chi tiết cụ thể ở tầng thấp (hệ thống file `File` của hệ điều hành và luồng xuất `Console`). Nó không thông qua các abstraction (interface như `IAppLogger` hay `INotificationService`).
 **Hậu quả:** 
   Cực kỳ khó viết unit test tự động vì mỗi lần chạy test sẽ làm sinh ra file rác (`club-log.txt`) hoặc dính chặt vào thiết bị ngoại vi.
   Không thể dễ dàng thay đổi cách ghi log (ví dụ đổi từ ghi file sang ghi database hoặc gửi lên cloud) mà không sửa code nghiệp vụ.
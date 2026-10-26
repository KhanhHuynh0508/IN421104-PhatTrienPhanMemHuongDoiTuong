using EduHub.Ch02;
using EduHub.Ch02.Domain;

// ═══════════════════════════════════════════════════════════════
//  EduHub Ch02 — Refactoring với SOLID và Design Patterns
// ═══════════════════════════════════════════════════════════════

// 1. Composition Root: Khởi tạo các thành phần và tiêm phụ thuộc (DI)
IAppLogger logger = new ConsoleLogger();
IClubRepository clubRepo = new LoggingClubRepository(new InMemoryClubRepository(), logger);
IMemberRepository memberRepo = new InMemoryMemberRepository();
INotificationServiceFactory notificationFactory = new NotificationServiceFactory();

var clubService = new ClubService(clubRepo, memberRepo, logger);
var eventService = new EventService(clubRepo);
var reportService = new ReportService(clubRepo, memberRepo);

// 2. Đăng ký Observer cho EventService (Observer Pattern)
eventService.Subscribe(new LogNotifier(logger));
eventService.Subscribe(new EmailNotifier(memberRepo, new EmailNotificationService()));

// 3. Chạy thử nghiệm các chức năng sau khi Refactor
Console.WriteLine("== 1. Tạo CLB (Áp dụng Factory & Decorator) ==");
var club = clubService.CreateClub("Tech Club", "Câu lạc bộ công nghệ", "president@gmail.com", NotificationChannel.Email, notificationFactory);

clubService.AddMember(club.Id, "Nguyễn Văn A", "a@student.dthu.edu.vn");

Console.WriteLine("\n== 2. Tạo sự kiện (Áp dụng Observer) ==");
eventService.CreateEvent(club.Id, "Workshop SOLID", new DateTime(2026, 11, 20));

Console.WriteLine("\n== 3. Xuất báo cáo (Áp dụng Strategy - CSV) ==");
string csvReport = reportService.Generate(new CsvReportStrategy());
Console.WriteLine(csvReport);

Console.WriteLine("\n🎉 Chúc mừng! Refactor và áp dụng SOLID/Patterns thành công!");
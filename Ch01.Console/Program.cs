using EduHub.Ch01.Domain;

// ═══════════════════════════════════════════════════════════════
//  EduHub Ch01 — Console demo (luồng nghiệp vụ đầy đủ)
// ═══════════════════════════════════════════════════════════════

Console.WriteLine("=== EduHub — Club & Event Management ===\n");

try
{
    // Bước 1: Tạo CLB "Tech Club"
    Console.WriteLine("1️⃣ Đang tạo Câu lạc bộ...");
    var techClub = Club.Create("Tech Club", "Câu lạc bộ Công nghệ thông tin Đại học Đồng Tháp");
    Console.WriteLine($"   ➔ Đã tạo CLB: {techClub.Name} (Mã: {techClub.Id})");

    // Bước 2: Tạo 3 Member và thêm vào CLB
    Console.WriteLine("\n2️ Đang thêm thành viên vào CLB...");
    var member1 = Member.Create("Huỳnh Thanh C", "c.huynh@student.dthu.edu.vn", "DH24001");
    var member2 = Member.Create("Nguyễn Văn A", "a.nguyen@student.dthu.edu.vn", "DH24002");
    var member3 = Member.Create("Trần Thị B", "b.tran@student.dthu.edu.vn", "DH24003");

    techClub.AddMember(member1);
    techClub.AddMember(member2);
    techClub.AddMember(member3);
    Console.WriteLine($"   ➔ Tổng số thành viên hiện tại của CLB: {techClub.MemberCount()}");

    // Bước 3: Tạo sự kiện "Workshop AI"
    Console.WriteLine("\n3️ Đang tạo sự kiện...");
    var aiEvent = Event.Create(techClub.Id, "Workshop AI & .NET 10", DateTime.Now.AddDays(7), capacity: 50, isOnline: true);
    Console.WriteLine($"   ➔ Đã tạo sự kiện: '{aiEvent.Title}' (Sức chứa tối đa: {aiEvent.Capacity})");

    // Bước 4: Thành viên đăng ký sự kiện và xác nhận phiếu (Pending -> Confirmed)
    Console.WriteLine("\n4️ Thành viên đăng ký tham gia sự kiện...");

    // Kiểm tra sức chứa trước khi đăng ký
    int currentRegisteredCount = 2; // Giả lập số lượng đang đăng ký
    if (aiEvent.HasCapacity(currentRegisteredCount))
    {
        Console.WriteLine("   ➔ Sự kiện vẫn còn chỗ trống. Tiến hành tạo phiếu đăng ký...");
    }

    var reg1 = Registration.Create(member1.Id, aiEvent.Id);
    var reg2 = Registration.Create(member2.Id, aiEvent.Id);

    Console.WriteLine($"   ➔ Phiếu của {member1.Name} khởi tạo ở trạng thái: {reg1.Status}");

    // Xác nhận phiếu đăng ký
    reg1.Confirm();
    reg2.Confirm();
    Console.WriteLine($"   ➔ Đã xác nhận phiếu đăng ký thành công! Trạng thái mới: {reg1.Status}");

    // Bước 5: Thực hiện Check-in (Điểm danh)
    Console.WriteLine("\n5️⃣ Thực hiện điểm danh (Check-in)...");
    var checkIn1 = CheckIn.Create(reg1, "QrCode");
    Console.WriteLine($"   ➔ {checkIn1.Summary()}");

    // Bước 6: In báo cáo tham dự
    Console.WriteLine("\n6️⃣ BÁO CÁO THAM DỰ SỰ KIỆN:");
    Console.WriteLine($"   - Tên sự kiện: {aiEvent.Title}");
    Console.WriteLine($"   - Tổng số lượng đăng ký đã xác nhận: 2");
    Console.WriteLine($"   - Tổng số lượng đã Check-in thực tế: 1");
    Console.WriteLine("   - Danh sách thành viên tham gia:");
    Console.WriteLine($"     + {member1} - Trạng thái: Đã Check-in");
    Console.WriteLine($"     + {member2} - Trạng thái: Chưa Check-in");

    // Bước 7: Thử nghiệm vi phạm quy tắc nghiệp vụ (Bắt lỗi ngoại lệ)
    Console.WriteLine("\n7️⃣ Kiểm tra quy tắc nghiệp vụ ngoại lệ (Test Exception)...");
    try
    {
        // Thử hủy phiếu rồi xác nhận lại xem hệ thống có chặn không
        reg1.Cancel();
        Console.WriteLine($"   ➔ Đã hủy phiếu của {member1.Name}. Trạng thái hiện tại: {reg1.Status}");

        // Cố tình xác nhận lại phiếu đã hủy (sẽ sinh lỗi)
        reg1.Confirm();
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"   [Bắt lỗi thành công] ➔ {ex.Message}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($" Lỗi không mong đợi: {ex.Message}");
}

Console.WriteLine("\n Demo complete!");

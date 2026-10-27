# User Stories — EduHub

# User Stories — EduHub

## US-01 · Sinh viên: Tham gia câu lạc bộ
Là **Sinh viên**, tôi muốn tham gia vào một câu lạc bộ yêu thích, để có thể kết nối với các bạn sinh viên khác và tham gia các hoạt động chung.

- **Tiêu chí 1:** 
  - **Given** sinh viên chưa là thành viên của câu lạc bộ
  - **When** sinh viên thực hiện yêu cầu tham gia
  - **Then** tên của sinh viên được thêm vào danh sách thành viên (`club.Members`) của câu lạc bộ đó.
- **Tiêu chí 2:** 
  - **Given** sinh viên đã là thành viên của câu lạc bộ từ trước
  - **When** sinh viên tiếp tục gửi yêu cầu tham gia lại câu lạc bộ đó
  - **Then** hệ thống sẽ từ chối để tránh trùng lặp thành viên.

## US-02 · Sinh viên: Đăng ký tham gia sự kiện
Là **Sinh viên**, tôi muốn đăng ký tham gia một sự kiện do câu lạc bộ tổ chức, để cập nhật kiến thức và tham gia trải nghiệm.

- **Tiêu chí 1:** 
  - **Given** sự kiện vẫn còn chỗ trống (`Capacity > số lượng đăng ký hiện tại`)
  - **When** sinh viên tiến hành đăng ký sự kiện
  - **Then** hệ thống tạo một phiếu đăng ký (`Registration`) mới với trạng thái chờ xác nhận (`Pending`).
- **Tiêu chí 2:** 
  - **Given** sự kiện đã hết chỗ (`Capacity` đã đạt giới hạn tối đa)
  - **When** sinh viên cố gắng đăng ký
  - **Then** hệ thống thông báo sự kiện đã hết chỗ và từ chối đăng ký.

## US-03 · Sinh viên: Check-in (Điểm danh) tại sự kiện
Là **Sinh viên**, tôi muốn thực hiện check-in khi đến tham gia sự kiện, để được ghi nhận sự hiện diện chính thức.

- **Tiêu chí 1:** 
  - **Given** sinh viên có phiếu đăng ký đã được ban tổ chức xác nhận (`Status == Confirmed`)
  - **When** sinh viên tiến hành quét mã hoặc điểm danh
  - **Then** hệ thống tạo thành công bản ghi `CheckIn` mới.
- **Tiêu chí 2:** 
  - **Given** phiếu đăng ký đang ở trạng thái chờ (`Pending`) hoặc đã hủy (`Cancelled`)
  - **When** sinh viên thực hiện check-in
  - **Then** hệ thống báo lỗi và từ chối điểm danh.

## US-04 · Ban CLB: Tạo sự kiện mới
Là **Ban chủ nhiệm CLB**, tôi muốn tạo mới một sự kiện cho câu lạc bộ của mình, để thông báo và thu hút các thành viên tham gia.

- **Tiêu chí 1:** 
  - **Given** thông tin tiêu đề hợp lệ và sức chứa lớn hơn 0 (`Capacity > 0`)
  - **When** ban chủ nhiệm tạo sự kiện
  - **Then** sự kiện được tạo thành công và liên kết chính xác với mã câu lạc bộ (`ClubId`).
- **Tiêu chí 2:** 
  - **Given** tiêu đề bị để trống hoặc sức chứa nhỏ hơn hoặc bằng 0 (`Capacity <= 0`)
  - **When** ban chủ nhiệm thực hiện tạo sự kiện
  - **Then** hệ thống ném ngoại lệ báo lỗi dữ liệu không hợp lệ.

## US-05 · Admin: Quản lý và ngừng hoạt động thành viên
Là **Admin hệ thống**, tôi muốn tạm dừng tư cách hoạt động của một thành viên vi phạm quy định, để đảm bảo kỷ luật chung của câu lạc bộ.

- **Tiêu chí 1:** 
  - **Given** thành viên đang ở trạng thái hoạt động (`Active`)
  - **When** admin thực hiện thao tác khóa/ngừng thành viên
  - **Then** trạng thái của thành viên chuyển sang không hoạt động (`IsActive() = false`).
- **Tiêu chí 2:** 
  - **Given** thành viên đã bị ngừng hoạt động
  - **When** hệ thống lưu trữ hoặc tra cứu lịch sử các sự kiện cũ
  - **Then** lịch sử đăng ký và tham gia trước đó của thành viên đó vẫn được giữ nguyên vẹn, không bị xóa mất.
# ASPNET-DK24TTC2-DUONGTUANCANH-CANH
# 🛒 Website Bán Linh Kiện Điện Tử
**Đồ án môn học: Chuyên đề ASP.NET – Năm học 2024–2025**

**Sinh viên thực hiện: Dương Tuấn Cảnh – MSSV: 170124250 – Lớp DK24TTC2**

**Giảng viên hướng dẫn: TS. Đoàn Phước Miền**

## 📌 Giới thiệu

Đề tài **Xây dựng website bán linh kiện điện tử** được thực hiện nhằm đáp ứng nhu cầu mua sắm trực tuyến đang ngày càng phát triển, đặc biệt trong lĩnh vực linh kiện điện tử – nơi có số lượng sản phẩm lớn, đa dạng và thay đổi liên tục.

Hệ thống được xây dựng bằng **ASP.NET MVC**, kết hợp **Entity Framework**, **SQL Server**, **Bootstrap** và các công nghệ web hiện đại, giúp mang đến một nền tảng thương mại điện tử trực quan, dễ sử dụng và có khả năng quản lý khoa học.

---

## 🎯 Mục tiêu

* Xây dựng website bán linh kiện điện tử hoàn chỉnh với các chức năng cơ bản.
* Áp dụng mô hình MVC vào phát triển ứng dụng web.
* Thực hành thiết kế cơ sở dữ liệu với SQL Server.
* Nâng cao kỹ năng lập trình C#, ASP.NET, HTML/CSS/JS, Bootstrap.
* Tạo ra một công cụ giúp quản trị viên dễ dàng quản lý sản phẩm, khách hàng và đơn hàng.

---

## 🏗️ Kiến trúc & Công nghệ sử dụng

| Công nghệ                   | Vai trò                               |
| --------------------------- | ------------------------------------- |
| **ASP.NET MVC**             | Xây dựng web theo mô hình MVC         |
| **C#**                      | Xử lý logic nghiệp vụ                 |
| **Entity Framework Core**   | ORM quản lý dữ liệu                   |
| **SQL Server**              | Lưu trữ cơ sở dữ liệu hệ thống        |
| **Bootstrap 5**             | Thiết kế giao diện responsive         |
| **HTML / CSS / JavaScript** | Xây dựng giao diện và xử lý tương tác |

---

## ⚙️ Chức năng hệ thống

### 👨‍💼 Admin

* Đăng nhập quản trị.
* Quản lý **sản phẩm**: thêm / sửa / xóa.
* Quản lý **danh mục sản phẩm**.
* Quản lý **đơn hàng**: xác nhận, cập nhật trạng thái.
* Quản lý **tài khoản người dùng**.
* Xem thống kê hệ thống.

### 👤 User (Khách hàng)

* Đăng ký và đăng nhập.
* Xem danh sách sản phẩm theo danh mục.
* Tìm kiếm, lọc sản phẩm.
* Xem thông tin chi tiết sản phẩm.
* Thêm vào giỏ hàng, cập nhật giỏ hàng.
* Đặt hàng (thanh toán COD).
* Xem lịch sử mua hàng.
* Cập nhật thông tin cá nhân.

### 👥 Guest (Khách vãng lai)

* Xem sản phẩm, danh mục.
* Xem chi tiết sản phẩm.
* Đăng ký / đăng nhập.

---

## 🗂️ Cấu trúc dữ liệu

Hệ thống sử dụng các bảng chính:

* **Users** – quản lý tài khoản khách hàng & admin
* **Products** – danh sách sản phẩm linh kiện
* **Categories** – danh mục sản phẩm
* **Orders** – thông tin đơn hàng
* **OrderDetails** – chi tiết từng sản phẩm trong đơn hàng

Sơ đồ ERD thể hiện rõ mối quan hệ giữa các bảng (trích từ báo cáo).

---

## 🖥️ Giao diện tiêu biểu

Hệ thống bao gồm các giao diện chính:

* Trang chủ
* Trang sản phẩm
* Chi tiết sản phẩm
* Giỏ hàng
* Trang quản trị admin
* Quản lý sản phẩm, đơn hàng, khách hàng
* Lịch sử mua hàng
* Đăng ký / đăng nhập

(Tất cả hình ảnh giao diện đã được trình bày trong báo cáo)

---

## 📦 Cài đặt dự án

### 1️⃣ Công cụ cần thiết

* Visual Studio 2022
* SQL Server + SQL Server Management Studio
* .NET Core SDK (phiên bản phù hợp)

### 2️⃣ Khởi chạy dự án

* Clone project
* Restore database từ file backup hoặc script
* Cập nhật chuỗi kết nối trong `appsettings.json`
* Chạy ứng dụng trên Visual Studio

---

## 🔍 Kết quả đạt được

* Xây dựng website thương mại điện tử cơ bản.
* Tạo cơ sở dữ liệu và thiết kế giao diện trực quan.
* Hoàn thiện các chức năng CRUD.
* Ứng dụng được mô hình MVC và Entity Framework Core.

---

## 🚀 Hướng phát triển

* Tích hợp thanh toán online (Momo, ZaloPay, VNPAY).
* Bổ sung bảo mật nâng cao (JWT, phân quyền chi tiết).
* Thêm API hỗ trợ mobile app.
* Cải tiến UX/UI hiện đại hơn.
* Thêm chức năng đánh giá sản phẩm, bình luận, chatbot hỗ trợ.

---

## 📚 Tài liệu tham khảo

Danh sách tài liệu tham khảo đã được tổng hợp từ Microsoft Docs, W3Schools, TutorialsPoint… (đầy đủ trong báo cáo gốc).



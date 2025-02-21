# Space Explorer

## Giới thiệu

**Space Explorer** là một game 2D thể loại Endless Runner, nơi người chơi điều khiển một phi thuyền vũ trụ di chuyển trong không gian, né tránh thiên thạch, thu thập vật phẩm và bắn hạ chướng ngại vật để sống sót lâu nhất có thể.

## Thành viên

- SE173039 - Phạm Nguyễn Trọng Tuấn
- SE170585 - Phạm Bích Ngọc
- SE183124 - Phạm Xuân Hoàng
- SE183870 - Ngô Lê Thảo Nguyên
- SE183122 - Lê Đặng Minh Trí

## Thông tin game

- **Tên game:** Space Explorer
- **Thể loại:** 2D Endless Runner
- **Nền tảng:** PC

## Quy tắc chơi

### Điều khiển phi thuyền

- **Di chuyển:** Sử dụng phím `A/D/W/S` hoặc phím mũi tên để điều khiển trái, phải, lên, xuống.
- **Bắn đạn:** Nhấn `Space` để bắn đạn tiêu diệt thiên thạch.

### Va chạm thiên thạch

- Mỗi lần va chạm với thiên thạch, người chơi sẽ mất 1 tim.
- Nếu mất hết 3 tim, trò chơi kết thúc (Game Over).
- Nếu có **Shield**, nó sẽ bảo vệ phi thuyền khỏi 1 lần va chạm.

### Bắn thiên thạch

- Khi bắn trúng thiên thạch, nó sẽ phát nổ và rơi **sao**.

### Thu thập vật phẩm

- **Heart (Tim):** Hồi 1 tim (tối đa 3 tim).
- **Shield (Khiên bảo vệ):** Chặn 1 lần va chạm với thiên thạch.
- **Bullet (Đạn):** Cung cấp thêm 3 viên đạn.

### Tăng độ khó theo điểm số

- Điểm càng cao, tốc độ rơi của thiên thạch càng nhanh.
- Số lượng thiên thạch xuất hiện tăng theo thời gian.

## Các màn chơi

### **Scene 1: Menu**

- Nút bấm: **Play, Instruction, Quit**
- Hiển thị **Best Score** của người chơi.
- Hình nền không gian với các ngôi sao và nhạc nền.

### **Scene 2: Level 1**

- Màn chơi đầu tiên.
- Bắt đầu với **3 tim** và điểm số **0**.
- Thu thập **20 sao** để sang **Level 2**.

### **Scene 3: Level 2**

- Tốc độ thiên thạch tăng lên.
- Background thay đổi.
- Thu thập **40 sao** để sang **Level 3**.

### **Scene 4: Level 3**

- Level cuối cùng.
- Thiên thạch rơi nhanh hơn và nhiều hơn.
- Chơi đến khi **Game Over**.

### **Scene 5: End Game**

- Hiển thị **Best Score** và **Current Score**.
- Nút bấm: **Menu, Exit**.

## Vật phẩm trong game

### **Shield (Khiên bảo vệ)**

- **Chức năng:** Chặn 1 lần va chạm với thiên thạch.
- **Cách sử dụng:** Tự động kích hoạt khi nhặt được.

### **Heart (Tim - Tăng máu)**

- **Chức năng:** Tăng 1 tim.
- **Cách sử dụng:** Tự động cộng vào thanh máu.
- **Giới hạn:** Tối đa 3 tim.

### **Bullet (Đạn - Bổ sung đạn)**

- **Chức năng:** Tăng số lượng đạn để bắn thiên thạch.
- **Cách sử dụng:** Nhặt vật phẩm để nhận thêm **3 viên đạn** mỗi lần.

## Hướng dẫn cài đặt và chơi

1. Clone repository:
   ```sh
   git clone https://github.com/tuanpnt17/SpaceExplorer.git
   ```
2. Mở dự án trong Unity.
3. Chạy game trên **PC**.

## Liên hệ

Nếu có bất kỳ câu hỏi hoặc đóng góp nào, hãy liên hệ qua email: **tuanpntse173039@fpt.edu.vn**

---

Cảm ơn bạn đã chơi **Space Explorer**! 🚀

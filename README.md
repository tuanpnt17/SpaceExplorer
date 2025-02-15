# Đây là nhánh zollie

- Màn load game, màn hình chính (highest scored, play button, instruction button)
- Màn hình end game (Current score, return to main menu, quit game)
- Trong lúc chơi: Số ngôi sao (score) hiện tại
- Âm thanh chung

# Cấu trúc file thư mục của dự án

```Arduino
Assets/
├── Animations/
├── Audio/
├── Materials/
├── Prefabs/
├── Scenes/
├── Scripts/
│ ├── Managers/
│ ├── Player/
│ ├── UI/
│ ├── Enemies/
│ └── Utilities/
├── Sprites/
│ ├── Spaceship/
│ ├── Asteroids/
│ ├── Stars/
│ └── UI/
├── UI/
│ ├── Fonts/
│ ├── Icons/
│ └── Prefabs/
├── VFX/
├── Plugins/
├── Textures/
└── ThirdParty/
```

## Trong đó

### Animations/

- Chứa các file animation (.anim) và animator controller (.controller).
- Ví dụ: Animation cho Spaceship bay, Asteroids xoay, Stars sáng.

### Audio/

- Chứa tất cả các âm thanh và nhạc nền (format như .wav, .mp3).
- Ví dụ: Âm thanh laser bắn, va chạm với Asteroids, nhạc nền gameplay.

### Materials/

- Chứa các vật liệu (Materials) để áp dụng hiệu ứng ánh sáng, texture.
- Ví dụ: Material cho Spaceship, Asteroids, background.

### Prefabs/

- Chứa các prefab tái sử dụng (Spaceship, Asteroids, Stars, UI buttons).

### Scenes/

- Chứa tất cả các scene của game.
- Ví dụ:MainMenu.unity, Gameplay.unity, EndGame.unity

### Scripts/

> Chứa toàn bộ mã nguồn game, phân chia thành các nhóm:

#### Managers/:

- Các script quản lý chung, ví dụ: GameManager, SceneManager, AudioManager.

#### Player/:

- Các script liên quan đến Spaceship, ví dụ: PlayerController, LaserController.

#### UI/:

- Các script quản lý UI như điểm số, menu, popup.

#### Enemies/:

- Các script điều khiển Asteroids.

#### Utilities/:

- Các script hỗ trợ như InputHandler, RandomSpawner.

### Sprites/

> Chứa các sprite dùng trong game, phân chia theo đối tượng:

#### Spaceship/: Sprite Spaceship.

#### Asteroids/: Sprite Asteroids.

#### Stars/: Sprite Stars.

#### UI/: Icon và hình ảnh liên quan đến UI.

### UI/

#### Fonts/: Chứa font chữ cho UI.

#### Icons/: Icon như nút bấm, mũi tên.

#### Prefabs/: Tạo prefab UI như menu, popup.

### VFX/

- Chứa các hiệu ứng đặc biệt (particle effect).
- Ví dụ: Hiệu ứng laser bắn, vụ nổ Spaceship.

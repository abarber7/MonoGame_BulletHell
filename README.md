# 🔫 MonoGame Bullet Hell

**Developer:** Antonio Barber  
**Framework:** [MonoGame](https://www.monogame.net/)  
**Language:** C#  
**Repository:** [MonoGame_BulletHell](https://github.com/abarber7/MonoGame_BulletHell)

---

## 🎮 Overview

This project is a **top-down bullet hell shooter** built using **MonoGame and C#**. The game features intense action, projectile-heavy gameplay, enemy waves, and polished game states — all wrapped in a clean, modular game architecture.

One standout addition is a **secret gameplay mechanic** — a bullet type that can push other bullets upon collision — designed to add dynamic chaos and emergent strategies to the battlefield.

---

## ✨ Key Features

- 🔫 Classic arcade-style bullet hell gameplay
- 👾 Enemy waves with pattern-based projectile firing
- 🧠 Modular architecture with extensible patterns and projectile types
- 🎮 Multiple game states: menu, gameplay, and game over
- 🧪 **Secret Feature B**: player-fired bullets that push other bullets
- 🔉 Integrated sound effects and background music
- 🎨 Retro visuals and smooth movement handling

---

## 🔍 Secret Feature B: Bullet-on-Bullet Physics

A secret weapon class introduces **a new type of bullet** that collides with other enemy bullets and **pushes them** rather than destroying them. This results in chaotic and strategic on-screen effects, making the gameplay feel more dynamic and reactive.

### 🔧 How It Works

- A **new concrete `Projectile` subclass** is introduced that overrides the `OnCollision` method.
- Instead of ignoring collisions with other projectiles (the default behavior), this bullet checks if the other object is an **enemy projectile**.
- Upon collision, it applies **its own velocity or movement pattern** to the enemy bullet, pushing it in a new direction.

### 🏗️ Architecture Support

- `Projectile` and `MovementPattern` are both abstract classes — allowing easy extension.
- Collision detection was already in place, but originally skipped projectile-vs-projectile checks.
- A new **movement pattern** or **showcase test level** may be introduced to better demonstrate radial or angular movement interactions, like those seen in bullet deflection videos.

This secret mechanic introduces an element of **environmental manipulation** — letting the player redirect the bullet storm rather than simply dodging it.

---

## 🕹️ Controls

| Action         | Key                  |
|----------------|----------------------|
| Move           | Arrow Keys or WASD   |
| Fire           | Spacebar             |
| Pause/Menu     | Escape               |
| Activate Secret| Unlocked in-game     |

---

## 🚀 Getting Started

### Requirements

- [MonoGame 3.8+](https://www.monogame.net/downloads/)
- Visual Studio 2019 or later
- .NET Desktop Development workload

### Run the Game

1. Clone the repository:
   ```bash
   git clone https://github.com/abarber7/MonoGame_BulletHell.git
    cd MonoGame_BulletHell

## 📁 Repository Structure

```bash
MonoGame_BulletHell/
├── Content/                # Game assets: sprites, fonts, sounds
├── Game1.cs                # Main MonoGame class
├── Player.cs               # Player movement and shooting
├── Enemy.cs                # Enemy behavior and spawning
├── Bullet.cs               # Base projectile class
├── DeflectorBullet.cs      # New secret feature bullet type
├── MovementPattern.cs      # Abstract base for bullet movement patterns
├── GameStateManager.cs     # Manages Menu, Play, and Game Over states
├── Program.cs              # Entry point
└── README.md               # This file

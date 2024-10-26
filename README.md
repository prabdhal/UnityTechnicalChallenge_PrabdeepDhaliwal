# Unity Developer Technical Interview Challenge

## Overview
This project is a 3D action game where the player navigates a platform-based environment, fighting through minion enemies to reach and defeat a boss. The game features smooth and responsive controls, unique enemy behaviors, and a variety of combat mechanics. The goal is to defeat the final boss, with an option to enhance the player's stats by defeating minions along the way.

## How to Download and Play
To download the project and play, follow these steps:

- Click on the "Code" button on the project's GitHub repository.
- Select "Download ZIP" from the dropdown menu.
- Unzip the downloaded file and navigate to the UnityTechnicalChallenge_PrabdeepDhaliwal-master -> Builds -> Standalone_Build folder.
- Open the .exe file to start the game.

## How to Play
- **Movement**: Use the WASD keys (keyboard) or left stick (gamepad) to move.
- **Camera Look**: Control the camera with the mouse (keyboard) or right joystick (gamepad).
- **Jump**: Press the spacebar (keyboard) or the south button (gamepad).
- **Basic Attack**: Click the left mouse button (keyboard) or pull the right trigger (gamepad).
- **Special Attack**: Click the right mouse button (keyboard) or pull the left trigger (gamepad).
- **Target Lock**: Click the shift key (keyboard) or the left bumper (gamepad).
- **Pause**: Press the escape key (keyboard) or the start button (gamepad).

The game ends when the player defeats the boss, with a "Victory" message displayed. If the player is defeated, a "Game Over" message appears, both offering a quit or retry option.

## Game Implementation and Bonus Features
- **Player Movement and Combat**: Smooth and responsive controls for basic movement, jumping, and attacks.
- **Environment**: A 3D platform-based map featuring moving platforms over lava and obstacles with falling metal bars.
- **Enemies**:
  - **Minions**: Three types of enemies with distinct abilities:
    - **Cube Enemy**: Ranged and melee attacks.
    - **Disc Enemy**: Damage-over-time and multi-projectile attacks.
    - **Cylinder Enemy**: Cannon-based attacks and a combo sequence.
  - **Boss**: Three unique abilities, including melee, projectile, and area-of-effect attacks.
- **Enemy Spawner System**: Each enemy spawns with a maximum number of enemies set, respawning after death. A timer controls the spawn delay between each spawn.
- **Game Over Condition**: Victory on defeating the boss, and Game Over on player defeat, with appropriate sound effects.
- **Sound Effects**: Custom sound effects for actions like projectile firing, hits, and the game over/victory.
- **Target Lock System**: Assists with locking onto enemies for smoother gameplay.
- **Loot System**: Enemies drop health, mana, and permanent stat boosts for attributes like damage, resistances and movement speed.

## Assets
### Unity Asset Store Assets
Mostly all assets, models, animations, particles, and sound effects were created myself except:
- **Cinemachine**: Used for player camera control.
- **NavMesh Agent**: Utilized for enemy pathfinding.
- **Terrain Asset**: To create the map.
- **Sound Effects**: Victory and Game Over sounds only.
- **Environment Rocks**: Rock prefabs.
- **Lava Texture**: Water texture for the lava.

## Known Issues
- None known as of yet.

## Potential Optimizations
- **Target Lock System**: Improve the target lock system by ensuring the camera rotates to face the target immediately. Additionally, limit the horizontal view to keep the target always in sight, enhancing gameplay clarity.
- **Performance Optimization**: Implement object pooling for projectiles and frequently instantiated objects to reduce memory allocation and improve overall performance.
- **Sound Management**: Optimize audio playback by using an audio manager that controls volume levels and prioritizes important sounds during intense combat situations.
- **Dynamic Difficulty Adjustment**: Introduce a system that scales enemy difficulty based on player performance, ensuring a balanced challenge throughout the game.


Enjoy navigating through the game, defeating enemies, and claiming victory over the boss!

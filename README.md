# Merging Game Prototype

## Project Description
This is a **Merging Game Prototype** inspired by [Infinite Craft](https://neal.fun/infinite-craft/), built entirely from scratch by **Anusheel Soni**. The project focuses on **dynamic object merging mechanics** and a **custom modular UI system**, with an additional **spider movement game mode**.

## Features

### Merging System:
- **Game world with circles and rectangles** as core interactive elements.
- **Rectangle movement using mouse drag** for intuitive interaction.
- **Customizable circular collision detection** for rectangles.
- **Dynamic line rendering system**:
  - Connects points within a configurable radius.
  - Deletes lines when points move out of range.
- **Modular side menu system** with a flexible slot allocation.
- **Text overlay on player prefab** for element identification.
- **Script-driven rectangle data retrieval** from a future JSON-based repository.
- **Elements Manager**:
  - Defines required game elements dynamically.
  - Uses a class structure for unique object creation.
- **Merging Manager**:
  - Handles logic for merging two rectangles into a new rectangle.
  - Correctly transfers data, maintaining element properties.
- **Improved line rendering system**, now more structured and optimized.
- **Fetching player stats from a JSON-derived list**, storing data in newly instantiated objects.
- **Initial work for complex merging recipes**, requiring advanced mapping and proportional logic.

### Spider Movement Mode:
- **WASD-based player movement** for controlling a spider.
- **Spider body attachment** to the player sprite.
- **Player rotation and translation system** with customizable movement/rotation speeds.
- **New Input System integration**:
  - Future-proof for control remapping.
  - Enables virtual joystick support for mobile implementation.

## Unity Version
This project was developed using **Unity 6 (6000.0.37f1)**.

## Project Structure
The repository contains only three folders:
- `Assets`
- `Packages`
- `ProjectSettings`

Anyone can clone this repository and open it directly in Unity 6 to try out the project.

## License
All assets used in this project are licensed under **Creative Commons CC0**.

---

# **Stranded - Scripts**

## **Overview**
This repository contains all necessary scripts for the Unity game, *Stranded*. *Stranded* is a first-person, survival shooter game where the player's goal is to defend watchtowers they construct from sea monsters long enough to escape an island. These scripts control player mechanics, enemy AI behavior, UI interaction, and overall game management systems.

## **Folder Structure**
```plaintext
/Scripts/
|-- Enemies/
│   ├── EnemyHealth.cs                    # Handles enemy health and UI
│   ├── EnemyMovement.cs                  # Handles enemy AI behavior
|-- Management/
│   ├── GameManager.cs                    # Manages current game state and flow between levels
|-- Player/
│   ├── FireAmmo.cs                       # Handles ammo shooting mechanics
│   ├── PlayerMovement.cs                 # Handles player controls
|-- Systems/
│   ├── CameraMovement.cs                 # Handles camera controls
│   ├── DestroyGameObject.cs              # Destroys game objects from scene
│   ├── PickupAmmo.cs                     # Allows player to pickup ammo
│   ├── Rotate.cs                         # Rotates a game object
│   ├── SpawnAmmo.cs                      # Instantiates ammo at set intervals
│   ├── SpawnEnemny.cs                    # Instantiates random enemies
│   ├── TowerHealth.cs                    # Handles tower health and UI
|-- UI/
│   ├── LevelSelect.cs                    # Displays description for selected game mode
│   ├── MainMenu.cs                       # Handles UI for the title screen
│   ├── VictoryProgress.cs                # Handles UI for progress bar
└── README.md
```

## **Dependencies**
Unity Version: 2021.3.17

## **Installation & Usage**
1. Clone the repository:
```sh
git clone https://github.com/Jalen-Moore/stranded-scripts.git
```

2. Copy the scripts into your Unity project's 'Assets/Scripts/' folder
   
## **Credits**
Developed by Jalen Moore

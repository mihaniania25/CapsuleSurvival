# CAPSULE SURVIVAL
**Capsule Survival** is a simple game with the following rules:
- Player is supposed to live as long as he/she can, avoiding moving enemies and idle bombs
- When Player touches enemy or bomb, the game ends with proposal to restart.
- Enemies and bombs are spawned gradually.




## Architecture

### Business Logic
All business logic is in **_CapsuleSurvival.Core_** namespace. Its scripts are in the _Assets/Scripts/GameCore_ folder.

GameCore scripts describe most important interfaces and abstract classes that supposed to be implemented according to view requirements.
Because of this, dependency is directed from view to business logic and not vice versa.

The main facade script of game process is **GameManager**.

### Launcher
**Launcher** is the MonoBehaviour class (and GameObject in the game scene) that is designed to set all needed references to interfaces' implementations. As well, it launches logic that gives ability to start/restart the game. **Launcher** plays a role of the most 'dirty' class in the project, but, at the same moment, it is the entrance point of the application.




## Configuration
Except animation, all important game parameters can be tweaked using config files that are in the _Assets/Resources_ folder.

**InGameGeneratorConfig**:
Generation settings (prefabs, delays, intervals, quantity constraints)

**ArenaParticipantsSettings**:
Speed of enemies, speed of Player

**PlayerConfig**:
Prefab of the Player
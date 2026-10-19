# 001 — Hranice modulov

## Status
**Proposed** | Accepted | Superseded by XXX

## Context
I am starting to work on a school mini-project, Horde Protocol. The game flow is as follows: Player spawns -> Wave of enemies spawns -> Player starts killing enemies -> Once the wave is complete, the player chooses a upgrade card and moves on to the next wave -> When player dies, it's game over and the game restarts at wave 1 with no progress saved, however the player learned new skills to use in the next game.

All decisions need to be written as an ADR. Once an ADR is Accepted, it is immutable and cannot be changed. The only thing that can change is it's status from Accepted to Superseded by another ADR.

The project will need to be modular due to it's scale and each "Script" or "System" should have one purpose, making it easily readable.

Today I'll have to fork the project, finish 001-ADR and create a Arena scene and implement the movement.

## Decision
Project will be split into 5 modules, each having it's own folder inside (Assets/_Project/) with a assembly definition (.asmdef)

**Data** - ScriptableObjects, information and settings about loot, waves, enemies, player go here. It will not depend on anything
**Gameplay** - Runtime code. Functionality of the player, waves, enemies. It will depend on Data to achieve this.
**Presentation** - Everything the player will see: UI, Camera, Effects. Can depend on Data and Gameplay so that it can show the appropriate information on screen.
**Editor** - Everything that only lives in the Unity Editor. Does not compile to the build. Custom Editors, Property Drawers, etc... It can depend on Gameplay and Data.
**Tests** - They will test (Assert) certain conditions. They will not be compiled to the build. They will depend on whatever they are testing.

Scenes will live in (Assets/Scenes/)
Project-wide settings will live in (Assets/Settings/) 

## Outcome
- Everything will have one purposea and it will only depend on what it needs, therefore reducing spaghetti code.
- Data and Logic is nicely separated, therefore the project will be easily scalable.
- Separation of code to it's purpose (Gameplay, UI/Presentation, Editor) makes it easy to identify what they do just based on their names.
- Tests will give a sanity check to make sure everything is in order and catch simple mistakes or improper assumptions.
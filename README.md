# Horde Protocol

Copyright © IAmAliceee. All rights reserved. No AI training This repository is licensed for viewing only (see LICENSE for details). Do not use any content of this repo for AI/ML training, tuning, or evaluation.

Class template for Advanced Unity 3/4 (S1 teaching vehicle). This remote is **read-only for students**. Fork it and work on your own fork. Do not push here.

This is **not** a playable horde game. The Hello scene exists so that open → Play works, with no red errors in the console.

## Hub / editor

Pin this exact editor. Do not open the project in a different 6.x patch.

| | |
|--|--|
| Editor | **Unity `6000.3.10f1`** (6.3 LTS) |
| Template | 2D (URP) |
| Company / product | Skyro / Horde Protocol |
| Color space | Hub URP 2D default (Linear) — do not change mid-year |
| Input | **Input System Package** (not the old Input Manager) |
| Player build | Windows Standalone · scene `Hello` |

If Hub offers an upgrade dialog, **Cancel**. Wrong patch = not the class toolchain.

## Clone / open

1. Fork this repository and clone **your fork**.
2. Unity Hub → **Add** → select this folder.
3. Open with **`6000.3.10f1`**. Wait for the first import.
4. Open `Assets/Scenes/Hello.unity`.
5. **Play.** Console must have no red errors. One `Horde Protocol` log from `HelloBanner` is expected.
6. **Player Build** → local Windows Standalone into `Builds/` (gitignored). Run the built player once.

Do not upgrade packages from Package Manager. If import fails, re-clone; do not “Update all”.

## Folder map + asmdef

`Assets/Scenes/` and `Assets/Settings/` stay outside `_Project/` on purpose. They are not a gameplay module. Runtime code lives under `Assets/_Project/`:

| Folder | Assembly | Allowed to reference |
|--------|----------|----------------------|
| `Gameplay/` | `HordeProtocol.Gameplay` | Data |
| `Data/` | `HordeProtocol.Data` | — |
| `Presentation/` | `HordeProtocol.Presentation` | Gameplay, Data |
| `Editor/` | `HordeProtocol.Editor` | Gameplay, Data (Editor platform only) |
| `Tests/` | `HordeProtocol.Tests` | Gameplay, Data, Test Runner |

**Dependency rule:** Presentation may use Gameplay and Data. Gameplay must **not** reference Editor or Presentation. Tests reference what they test. Art and UI prefabs go under Presentation later; ScriptableObject wave/loot data under Data (week 10). Empty assemblies are intentional. Week 6 writes this map down as an ADR (`docs/adr/001-hranice-modulov.md`). That week does not add an assembly and does not move scripts. `HelloBanner` already lives in Gameplay.

Input Actions: `Assets/Settings/Horde.inputactions` — map `Player` with `Move` (Vector2), `Fire` (Button), `Pause` (Button). Bindings are the GDD contract; they are not wired to a player object in Hello.

## DoD convention (git)

Weekly proof is a PR named `week-XX-kebab` on **your fork**, merged into **that fork’s** `main`.

Do **not** push to this class `main`. This repo stays the template. Tags on this remote are mentor-only.

Repository: https://github.com/teo-sk/skyro-horde-protocol

## Packages — frozen

`Packages/manifest.json` and `Packages/packages-lock.json` are locked to this Hub patch.

Present on purpose (even if unused until later weeks): Test Framework, Addressables, Burst, Collections, Mathematics, VContainer (git, commit-pinned). Do not add NGO, Entities, or Cinemachine “because we might need them”. Do not click **Update all**.

VContainer is the package only. No `LifetimeScope` in the Hello scene (that is week 9).

## CI

GitHub Actions workflow `.github/workflows/player-build.yml` exists with `if: false`. Player-build CI stays off until **week 13**. Do not enable it to “make the badge green”.

## GDD

Horde Protocol one-pager: [`docs/gdd-horde-protocol.md`](docs/gdd-horde-protocol.md). Pillars and slice scope live there — this repo does not invent a different game.

## What this repo is not

No wave manager, player combat, Addressable groups, Git LFS, or `Library/` in git. Commit `.meta` files with the assets they belong to. Local Player Builds go in `Builds/`.

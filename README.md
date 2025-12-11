# Boomerang Game (Boomerang Game Core) - LTU: Programvaruteknik - Refactor Exam

Summary
-------
This project was developed as my solution to a remote exam I completed during my time at LTU. It remains in an unfinished state: core components such as the deck, scoring rules, and 
edition loader are implemented, while the main game loop is still incomplete. Below is a detailed description of my solution.

Boomerang Game is a .NET 9 core library that models a travel-themed card/board game inspired by an Australian edition 
called "Boomerang Australia". The project provides domain models, game flow controllers, 
a configurable deck and region map, and a scoring engine driven by a JSON edition/configuration file.

Quick highlights
- Target framework: .NET 9
- Main configuration: `BoomerangGame.Core\Configurations\BoomerangConfig.json`
- Core code areas: 
	- game boot (`Program.cs`), 
	- application controllers (`RoundController`, `GameServices`), 
	- scoring (`ScoreEngine`), 
	- and map states (e.g. `AustraliaMapState`).
- Extensible scoring strategies and edition-based configuration loading.

Repository layout (important files)
- `BoomerangGame.Core\Program.cs` — application entry / composition root.
- `BoomerangGame.Core\Application\RoundController.cs` — orchestrates round/turn behavior.
- `BoomerangGame.Core\Scoring\ScoreEngine.cs` — scoring logic and strategy orchestration.
- `BoomerangGame.Core\Domain\States\MapStates\AustraliaMapState.cs` — region/map state for the Australia edition.
- `BoomerangGame.Core\Config\EditionConfig.cs` and `EditionLoader.cs` — edition/config loading abstractions.
- `BoomerangGame.Core\Application\Builders\IGameServiceBuilder.cs` and `GameServices.cs` — service composition for game runtime.
- `BoomerangGame.Core\Configurations\BoomerangConfig.json` — complete edition definition: deck, regionMap, scoring strategies, scoring constants.

Configuration (`BoomerangConfig.json`)
- `game.version` — edition version.
- `name` — edition name (e.g., "Boomerang Australia").
- `turnOrderIdentifier`, `regionTrackingIdentifier`, `tieBreakerIdentifier` — behaviour tokens used by game logic.
- `deck` — list of card/site objects (name, site code, region, quantity `number`, and `symbols` such as `collection`, `animal`, `blueIcon`).
- `regionMap` — mapping of region -> site codes.
- `regionCompletionPoints` — points awarded when a region is completed.
- `scoringStrategies` — ordered list of scoring strategies used by `ScoreEngine` (e.g., `ThrowCatchAbsolute`, `RegionBonusPoints`, `Collection`, `Animal`, `Activity`).
- `animalPointsPerPair` — points awarded per matched animal pair.

Build & run (Visual Studio 2022)
1. Ensure .NET 9 SDK is installed.
2. Open the solution in Visual Studio 2022.
3. Restore NuGet packages: right-click solution -> __Manage NuGet Packages for Solution__ or use __Restore NuGet Packages__.
4. Build: use __Build Solution__.
5. Run/debug: use __Debug > Start Debugging__ or __Debug > Start Without Debugging__.

Editing / Extending
- To change game content: edit `BoomerangGame.Core\Configurations\BoomerangConfig.json`. The loader reads edition properties at startup via `EditionLoader`.
- To add a scoring rule: implement a new strategy compatible with the `ScoreEngine` patterns and add its identifier to the `scoringStrategies` array in the config.
- To add or change map logic: update or extend `Domain\States\MapStates\*` classes (example: `AustraliaMapState`).
- To hook new services or change composition: modify builders or `GameServices` and `IGameServiceBuilder`.

Development notes
- The project is designed for edition-driven behavior; many runtime behaviors are driven by the JSON config.
- Keep config schema consistent (deck item properties and region map keys) — mismatches may cause runtime errors in loaders.
- If adding packages or changing frameworks, verify the solution TFM remains `.NET 9`.

Contributing
- Fork, create a feature branch, add tests (if present), and open a pull request.
- Describe changes in the PR and update `BoomerangConfig.json` examples when adding or changing edition content.

Notes
- This README summarizes the core intent and structure. For specific implementation details, open the relevant source files listed above (they are present in the current workspace).

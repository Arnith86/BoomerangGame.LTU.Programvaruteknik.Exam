using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Scoring;

namespace BoomerangGame.Core.Config;

/// <summary>
/// Represents an immutable configuration for a Boomerang card game edition.
/// </summary>
public sealed record EditionConfig(
	string Name,
	IReadOnlyList<BoomerangCardDefinition> Deck,
	IReadOnlyDictionary<string, List<string>> RegionMap,
	//IReadOnlyList<string> Regions,
	IRegionProgressTracker RegionProgressTracker,
	IReadOnlyList<string> ScoringStrategies,
	string TieBreakerIdentifier, 
	string TurnOrderIdentifier
);

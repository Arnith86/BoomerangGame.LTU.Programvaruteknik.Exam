using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Scoring;

namespace BoomerangGame.Core.Config;

/// <summary>
/// Represents an immutable configuration for a Boomerang card game edition.
/// </summary>
public sealed record EditionConfig(
	string Name,
	IReadOnlyList<BoomerangCardDefinition<string>> Deck,
	IReadOnlyDictionary<string, List<string>> RegionMap,
	IRegionProgressTracker RegionProgressTracker,
	IReadOnlyList<string> ScoringStrategies,
	string TieBreakerIdentifier, 
	string TurnOrderIdentifier,
	IReadOnlyDictionary<string, int> RegionPointsPerPair,
	
	/// <summary>
	/// Optional mapping of animal names to points per pair for editions that include animals.
	/// </summary>
	IReadOnlyDictionary<string, int>? AnimalPointsPerPair = null
);

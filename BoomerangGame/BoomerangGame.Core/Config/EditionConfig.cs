using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Domain.States.MapStates;
using BoomerangGame.Core.Scoring;

namespace BoomerangGame.Core.Config;

/// <summary>
/// Represents an immutable configuration for a Boomerang card game edition.
/// </summary>
public sealed record EditionConfig(
	string Name,
	IReadOnlyList<BoomerangCardDefinition<string>> Deck,
	IMapState RegionMap,
	IRegionProgressTracker RegionProgressTracker,
	List<IScoreCategory> ScoringStrategies,
	string TieBreakerIdentifier, 
	string TurnOrderIdentifier,
	IReadOnlyDictionary<string, int> RegionCompletionPoints,
	
	/// <summary>
	/// Optional mapping of animal names to points per pair for editions that include animals.
	/// </summary>
	IReadOnlyDictionary<string, int>? AnimalPointsPerPair = null
);

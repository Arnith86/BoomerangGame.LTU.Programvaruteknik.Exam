using BoomerangGame.Core.Domain.Cards;

namespace BoomerangGame.Core.Config;

/// <summary>
/// Represents an immutable configuration for a Boomerang card game edition.
/// </summary>
public sealed record EditionConfig(
	string Name,
	IReadOnlyList<BoomerangCardDefinition> Deck,
	IReadOnlyList<string> Regions,
	string RegionTrackingIdentifier,
	IReadOnlyList<string> ScoringStrategies,
	string TieBreakerIdentifier, 
	string TurnOrderIdentifier
);

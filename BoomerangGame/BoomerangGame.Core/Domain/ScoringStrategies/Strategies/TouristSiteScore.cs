using BoomerangGame.Core.Domain.ScoringStrategies.Utilities;
using BoomerangGame.Core.Domain.States.PlayerState;

namespace BoomerangGame.Core.Domain.ScoringStrategies.Strategies;

/// <summary>
/// Calculates the score for a player based on the number of tourist sites
/// they have visited during the round.
/// </summary>
public class TouristSiteScore : IScoreCategory
{
	public string Name { get; init; }

	public TouristSiteScore(string name)
	{
		Name = name ?? throw new ArgumentNullException(nameof(name));
	}

	public int CalculateScore(IBoomerangPlayerState playerState)
	{
		ScoringStrategyPreConditionNullCheck.Check(playerState);

		if (playerState.VisitedSites is null)
			throw new InvalidOperationException("Player does not have a visited sites collection.");

		return playerState.VisitedSites.Count;
	}
}

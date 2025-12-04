using BoomerangGame.Core.Domain.States.PlayerState;

namespace BoomerangGame.Core.Domain.ScoringStrategies;

public class TouristSiteScore : IScoreCategory
{
	public int CalculateScore(IBoomerangPlayerState playerState)
	{
		ScoringStrategyPreConditionNullCheck.Check(playerState);

		if (playerState.VisitedSites is null)
			throw new InvalidOperationException("Player does not have a visited sites collection.");

		return playerState.VisitedSites.Count;
	}
}

using BoomerangGame.Core.Domain.States.MapStates;
using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Domain.States.RoundStates;

namespace BoomerangGame.Core.Domain.ScoringStrategies;


/// <summary>
/// Calculates bonus points awarded for completing specific map regions during a round. <br/>
/// 
/// This scoring strategy uses an <see cref="IRegionProgressTracker"/> to determine whether a region is <br/>
/// eligible to grant a completion bonus, ensuring points are only awarded once per region across all rounds. <br/>
/// The bonus values for each region are configured through a provided dictionary, allowing different regions <br/>
/// to yield different point amounts.
/// </summary>
public class RegionBonusPointScore : IRoundScoreCategory
{
	private readonly IRegionProgressTracker _regionProgressTracker;
	private readonly Dictionary<string, int> _regionCompletionPoints;


	public RegionBonusPointScore(
		IRegionProgressTracker regionProgressTracker, 
		Dictionary<string, int> regionCompletionPoints)
	{
		_regionProgressTracker = regionProgressTracker;
		_regionCompletionPoints = regionCompletionPoints;
	}



	public int CalculateScore(IBoomerangPlayerState playerState, IRoundState roundState)
	{
		ScoringStrategyPreConditionNullCheck.Check(playerState);

		if (playerState.MapState is null)
			throw new InvalidOperationException("Player does not have a MapState instance.");

		return GetPoints(playerState.MapState, roundState.RoundNumber);
	}



	public int CalculateScore(IBoomerangPlayerState playerState)
		=> throw new NotSupportedException($"{nameof(IRoundState)} required.");



	private int GetPoints(IMapState mapState, int roundNumber)
	{
		int points = 0;

		if (mapState.CurrentMapState is null)
			throw new InvalidOperationException("MapState does not have a RegionState instance.");

		foreach (KeyValuePair<string, IRegionsState> kvp in mapState.CurrentMapState)
		{
			string regionName = kvp.Key;
			IRegionsState region = kvp.Value;

			if (region.IsComplete() && 
				_regionProgressTracker.EligibleForRegionBonus(regionName, roundNumber))
			{
				points += _regionCompletionPoints[regionName];
				_regionProgressTracker.MarkRegionAsCompleted(regionName, roundNumber);
			}
		}

		return points;
	}
}

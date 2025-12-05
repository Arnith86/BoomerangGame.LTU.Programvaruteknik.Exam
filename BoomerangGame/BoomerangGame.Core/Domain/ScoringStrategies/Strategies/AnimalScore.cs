using BoomerangGame.Core.Domain.ScoringStrategies.Utilities;
using BoomerangGame.Core.Domain.States.PlayerState;

namespace BoomerangGame.Core.Domain.ScoringStrategies.Strategies;

/// <summary>
/// Represents a scoring strategy that calculates points based on drafted animal cards. Points are <br/>
/// awarded for pairs of the same animal type, with different animal types having different point values.
/// </summary>
/// <remarks>Can be used with bort the Australian and American editions.</remarks>
public class AnimalScore : IScoreCategory
{
	private Dictionary<string, int> _pointsPerPair;
	public string Name { get; init; }

	public AnimalScore(Dictionary<string, int> pointsPerPair, string name)
	{
		_pointsPerPair = pointsPerPair ?? throw new ArgumentNullException(nameof(pointsPerPair));
		Name = name ?? throw new ArgumentNullException(nameof(name));
	}


	public int CalculateScore(IBoomerangPlayerState playerState)
	{
		ScoringStrategyPreConditionNullCheck.Check(playerState);

		if (playerState.DraftedCards is null)
			throw new InvalidOperationException("Player does not have a drafted cards");

		Dictionary<string, int> countedDictionerty =
			SymbolInstanceCounter.CountInstances(playerState.DraftedCards, "animal");

		return CalculatePoints(countedDictionerty); 
	}


	private int CalculatePoints(Dictionary<string, int> countedDictionerty)
	{
		int points = 0;

		foreach (KeyValuePair<string, int> collectionType in countedDictionerty)
		{
			if(_pointsPerPair.TryGetValue(collectionType.Key, out int pointPerPair))
			{
				points += pointPerPair * GetNrOfPairs(collectionType.Value);
			}
		}

		return points;
	}

	private int GetNrOfPairs(int value) => value / 2; 
}

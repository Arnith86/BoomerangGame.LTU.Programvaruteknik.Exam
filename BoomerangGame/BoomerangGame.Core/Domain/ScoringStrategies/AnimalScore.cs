using BoomerangGame.Core.Domain.ScoringStrategies.Utilities;
using BoomerangGame.Core.Domain.States.PlayerState;

namespace BoomerangGame.Core.Domain.ScoringStrategies;

/// <summary>
/// Represents a scoring strategy that calculates points based on drafted animal cards. Points are <br/>
/// awarded for pairs of the same animal type, with different animal types having different point values.
/// </summary>
public class AnimalScore : IScoreCategory
{
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
			switch (collectionType.Key)
			{
				case "Kangaroos":
					points += 3 * GetNrOfPairs(collectionType.Value);
					break;
				case "Emus":
					points += 4 * GetNrOfPairs(collectionType.Value);
					break;
				case "Wombats":
					points += 5 * GetNrOfPairs(collectionType.Value);
					break;
				case "Koalas":
					points += 7 * GetNrOfPairs(collectionType.Value);
					break;
				case "Platypuses":
					points += 9 * GetNrOfPairs(collectionType.Value);
					break;
				default:
					break;
			}
		}

		return points;
	}

	private int GetNrOfPairs(int value) => value / 2; 
}

using BoomerangGame.Core.Domain.ScoringStrategies.Utilities;
using BoomerangGame.Core.Domain.States.PlayerState;

namespace BoomerangGame.Core.Domain.ScoringStrategies;

public class CollectionScore : IScoreCategory
{
	public int CalculateScore(IBoomerangPlayerState playerState)
	{
		ScoringStrategyPreConditionNullCheck.Check(playerState);

		if (playerState.DraftedCards is null)
			throw new InvalidOperationException("Player does not have a drafted cards cards");


		Dictionary<string, int> countedDictionerty = 
			SymbolInstanceCounter.CountInstances(playerState.DraftedCards, "collection");

		int tempPoints = CalculateTempPoints(countedDictionerty);


		return tempPoints < 8 ? tempPoints * 2 : tempPoints;  
	}

	private int CalculateTempPoints(Dictionary<string, int> countedDictionerty)
	{
		int points = 0;

		foreach( KeyValuePair<string, int> collectionType in countedDictionerty)
		{
			switch (collectionType.Key)
			{
				case "Leaves":
					points += 1 * collectionType.Value;			
					break;
				case "Shells":
					points += 2 * collectionType.Value;			
					break;
				case "Wildflowers":
					points += 3 * collectionType.Value;			
					break;
				case "Souvenirs":
					points += 5 * collectionType.Value;			
					break;
				default:
					break;
			}
		}

		return points;
	}
}

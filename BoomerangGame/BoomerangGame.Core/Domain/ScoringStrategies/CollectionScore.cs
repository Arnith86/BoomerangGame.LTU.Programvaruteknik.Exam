using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.States.PlayerState;

namespace BoomerangGame.Core.Domain.ScoringStrategies;

public class CollectionScore : IScoreCategory
{
	public int CalculateScore(IBoomerangPlayerState playerState)
	{
		ScoringStrategyPreConditionNullCheck.Check(playerState);

		if (playerState.DraftedCards is null)
			throw new InvalidOperationException("Player does not have a drafted cards cards");


		Dictionary<string, int> countedDictionerty = InstanceCounter(playerState.DraftedCards, "collection");

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

	public Dictionary<string, int> InstanceCounter(	List<IBoomerangCard<string>> cards, string type) 
	{
		if (cards is null)
			throw new ArgumentNullException(nameof(cards));

		var result = new Dictionary<string, int>();
		
		foreach (var card in cards)
		{
			if (card.Symbols is null) continue;

			var symbol = card.Symbols.GetByCategory(type);

			string key = symbol!.Value;

			if (result.ContainsKey(key))
				result[key]++;
			else
				result[key] = 1;
		}

		return result;
	}
}

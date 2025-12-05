using BoomerangGame.Core.Domain.ScoringStrategies.Utilities;
using BoomerangGame.Core.Domain.States.PlayerState;

namespace BoomerangGame.Core.Domain.ScoringStrategies.Strategies;

/// <summary>
/// Represents a scoring strategy that awards points based on how many times a specific <br/>
/// activity symbol appears among the player's drafted cards.
/// </summary>
/// <remarks>
/// This scoring rule is used in Boomerang editions that include activity icons (blue icons).
/// </remarks>
public class ActivityScore : IScoreCategory
{
	public string Name { get; init; }
	
	private string _category;


	public ActivityScore(string category, string name)
	{
		_category = category ?? throw new ArgumentNullException(nameof(category));
		Name = name ?? throw new ArgumentNullException(nameof(name));
	}


	public int CalculateScore(IBoomerangPlayerState playerState)
	{
		ScoringStrategyPreConditionNullCheck.Check(playerState);

		if (playerState.DraftedCards is null)
			throw new InvalidOperationException("Player does not have a drafted cards");


		Dictionary<string, int> countedDictionerty =
			SymbolInstanceCounter.CountInstances(playerState.DraftedCards, "blueIcon");

		return MapPointsToActivityInstances(_category, countedDictionerty);
	}


	private int MapPointsToActivityInstances(
		string category, 
		Dictionary<string, int> countedDictionerty)
	{
		int instances = countedDictionerty[category];
		int points = 0;		

		switch (instances)
		{
			case 1:
				points = 0;
				break;
			case 2:
				points = 2;
				break;
			case 3:
				points = 4;
				break;
			case 4:
				points = 7;
				break;
			case 5:
				points = 10;
				break;
			case >= 6:
				points = 15;
				break;
			default:
				break;
		}
		
		return points;
	}
}

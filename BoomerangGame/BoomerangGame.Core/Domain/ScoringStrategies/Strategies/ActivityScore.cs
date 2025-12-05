using BoomerangGame.Core.Domain.ScoringStrategies.Utilities;
using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Domain.States.RoundStates;

namespace BoomerangGame.Core.Domain.ScoringStrategies.Strategies;

/// <summary>
/// Represents a scoring strategy that awards points based on how many times a specific <br/>
/// activity symbol appears among the player's drafted cards.
/// </summary>
/// <remarks>
/// This scoring rule is used in Boomerang editions that include activity icons (blue icons).
/// </remarks>
public class ActivityScore : IBlueIconScoreCategory
{
	public string Name { get; init; }
	
	public ActivityScore(string name)
	{
		Name = name ?? throw new ArgumentNullException(nameof(name));
	}


	public int CalculateScore(IBoomerangPlayerState playerState, string? category)
	{
		if (category is null) return 0;

		ScoringStrategyPreConditionNullCheck.Check(playerState);

		if (playerState.DraftedCards is null)
			throw new InvalidOperationException("Player does not have a drafted cards");


		Dictionary<string, int> countedDictionerty =
			SymbolInstanceCounter.CountInstances(playerState.DraftedCards, "blueIcon");

		return MapPointsToActivityInstances(category, countedDictionerty);
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

	public int CalculateScore(IBoomerangPlayerState playerState)
		=> CalculateScore(playerState, null);
}

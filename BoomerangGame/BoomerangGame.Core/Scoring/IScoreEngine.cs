using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Domain.States.RoundStates;

namespace BoomerangGame.Core.Scoring;

/// <summary>
/// Provides functionality for calculating player scores and determining winner <br/>
/// based on active scoring strategies within a Boomerang round.
/// </summary>
public interface IScoreEngine
{
	/// <summary>
	/// Calculates the total score for a player during a round using all registered scoring categories
	/// </summary>
	/// <param name="playerState">The player's current state, containing collected cards, values, and progression data.</param>
	/// <param name="roundState">The state of the round, providing contextual information needed for round-based scoring.</param>
	/// <param name="chosenBlueIcon">The identifier of the blue icon category selected by the player for scoring this round.</param>
	/// <returns>The total score the player earns for the round after all strategies are applied.</returns>
	int CalculateRoundScore(
		IBoomerangPlayerState playerState, 
		IRoundState roundState, 
		string chosenBlueIcon
	);

	/// <summary>
	/// Determines the winning player from a collection of player states. <br/>
	/// If multiple players share the highest score, a tie-breaking strategy is applied.
	/// </summary>
	/// <param name="playerStates">The collection of players participating in the comparison.</param>
	/// <returns>The player state representing the winner after applying tie-breaking rules if needed.</returns>
	IPlayerState DecideWinner(IEnumerable<IPlayerState> playerStates);
}
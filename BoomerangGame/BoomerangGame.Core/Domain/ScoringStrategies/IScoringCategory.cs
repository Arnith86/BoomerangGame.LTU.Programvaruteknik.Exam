using BoomerangGame.Core.Application;
using BoomerangGame.Core.Domain.States.RoundStates;

namespace BoomerangGame.Core.Domain.ScoringStrategies;

/// <summary>
/// Defines a scoring rule used to calculate points for a player based on <br/>
/// the round state or player state.
/// </summary>
/// <remarks>
/// Implementations of this interface represent individual scoring categories in the Boomerang game. <br/>
/// Each scoring category applies its own logic to evaluate player performance for the given round or <br/>
/// across the whole game.
/// </remarks>
public interface IScoringCategory
{
	/// <summary>
	/// Calculates the score for the specified player, using values from the provided round or player state.
	/// </summary>
	/// <param name="player">The player whose score is being calculated.</param>
	/// <param name="roundState">The state of the round containing relevant card and turn data.</param>
	/// <returns>The score computed for the player.</returns>
	/// <exception cref="ArgumentNullException">
	/// Thrown if <paramref name="player"/> or <paramref name="roundState"/> is null.
	/// </exception>
	/// <exception cref="InvalidOperationException">
	/// Thrown if the scoring category requires data that is missing from the round state.
	/// </exception>
	int CalculateScore(IPlayer player, IRoundState roundState);
}

using BoomerangGame.Core.Domain.States.PlayerState;

namespace BoomerangGame.Core.Domain.ScoringStrategies;

/// <summary>
/// Defines a scoring rule used to calculate points for a player based on <see cref="IPlayerState"/>.
/// </summary>
/// <remarks>
/// Implementations of this interface represent individual scoring categories in the Boomerang game. <br/>
/// Each scoring category applies its own logic to evaluate player performance for the given round or <br/>
/// across the whole game.
/// </remarks>
public interface IScoringCategory
{
	/// <summary>
	/// Calculates the score for the specified player, using values from <see cref="IPlayerState"/>.
	/// </summary>
	/// <param name="playerState">The player whose score is being calculated.</param>
	/// <returns>The score computed for the player.</returns>
	/// <exception cref="ArgumentNullException">
	/// Thrown if <paramref name="playerState"/> is null.
	/// </exception>
	/// <exception cref="InvalidOperationException">
	/// Thrown when data required for the scoring rule is missing
	/// (for example, if necessary cards have not been recorded).
	/// </exception>
	int CalculateScore(IPlayerState playerState);
}

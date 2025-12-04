using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Domain.States.RoundStates;

namespace BoomerangGame.Core.Domain.ScoringStrategies;

/// <summary>
/// Represents a scoring category that calculates points for a player based <br/>
/// on information from a specific round.
/// </summary>
/// <remarks>
/// This interface extends <see cref="IScoreCategory"/> by specializing the scoring behavior <br/>
/// for scenarios where round-based data is required. <br/>
///
/// Implementations define scoring rules that depend on card selections, turn order outcomes, or <br/>
/// other round-level state. Categories that rely only on player-level state should instead <br/>
/// implement <see cref="IScoreCategory"/> directly.
/// </remarks>
public interface IRoundScoreCategory : IScoreCategory
{
	/// <summary>
	/// Calculates the score for the specified player using data from the provided round state.
	/// </summary>
	/// <param name="playerState">The player whose score is being evaluated.</param>
	/// <param name="roundState">The round state containing relevant card, turn, and draft information.</param>
	/// <returns>The score computed for the player.</returns>
	/// <exception cref="ArgumentNullException">
	/// Thrown when <paramref name="playerState"/> or <paramref name="roundState"/> is <c>null</c>.
	/// </exception>
	/// <exception cref="InvalidOperationException">
	/// Thrown when the scoring rule requires round data that is missing
	/// (for example, if necessary cards have not been recorded).
	/// </exception>
	int CalculateScore(IBoomerangPlayerState playerState, IRoundState roundState);
}


using BoomerangGame.Core.Domain.ScoringStrategies.Utilities;
using BoomerangGame.Core.Domain.States.PlayerState;

namespace BoomerangGame.Core.Scoring;

/// <summary>
/// Represents a service responsible for managing scores by category
/// and handling tie-breaking logic between players.
/// </summary>
public interface ITieBreaker
{
	Dictionary<string, List<IScoreEntry>> ScoreByCategory { get; }

	/// <summary>
	/// Registers a player's score for a specific scoring category.
	/// </summary>
	/// <param name="playerName">The name of the player whose score is being registered.</param>
	/// <param name="strategy">The scoring category or strategy being applied.</param>
	/// <param name="points">The number of points to add for the player in this category.</param>
	void RegisterScoreByCategory(string playerName, string strategy, int points);

	/// <summary>
	/// Determines the winner among players for a specific scoring category in case of a tie.
	/// </summary>
	/// <param name="strategy">The scoring category for which the winner is being decided.</param>
	/// <param name="playerStates">The list of tied playerStates.</param>
	/// <returns>The name of the winning player.</returns>
	List<IPlayerState> DecideTieWinner(string strategy, List<IPlayerState> playerStates);
}
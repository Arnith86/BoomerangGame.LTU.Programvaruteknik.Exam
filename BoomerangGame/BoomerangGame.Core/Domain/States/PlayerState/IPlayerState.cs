namespace BoomerangGame.Core.Domain.States.PlayerState;

/// <summary>
/// Represents the public-facing state of a player within the Boomerang game. </br>
/// Provides access to basic player information and scoring functionality.
/// </summary>
public interface IPlayerState
{
	string Name { get; }
	int Score { get; }

	/// <summary>
	/// Adds the specified number of points to the player's score.
	/// </summary>
	/// <param name="points">The number of points to add. Must be a non-negative value.</param>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown when <paramref name="points"/> is negative.
	/// </exception>
	void AddToScore(int points);
}
namespace BoomerangGame.Core.Domain.ScoringStrategies.Utilities;


/// <summary>
/// Represents a named scoring entry used to accumulate and track points <br/>
/// for a specific scoring category or component within the game.
/// </summary>
/// <param name="Name"></param>
/// <param name="Points"></param>
public record ScoreEntry(string Name, int Points) : IScoreEntry
{
	public int Points { get; private set; } = Points;

	/// <inheritdoc/>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown if <paramref name="points"/> is less than zero.
	/// </exception>
	public void AddToPoints(int points)
	{
		if (points < 0)
			throw new ArgumentOutOfRangeException(nameof(points), "Cannot have a lower value than 1.");
		Points += points;
	}
}


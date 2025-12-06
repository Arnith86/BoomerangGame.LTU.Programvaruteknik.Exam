namespace BoomerangGame.Core.Domain.ScoringStrategies.Utilities
{
	/// <summary>
	/// Represents a named scoring entry used to accumulate and track points
	/// for a specific scoring category or component within the game.
	/// </summary>
	public interface IScoreEntry
	{
		string Name { get; init; }
		int Points { get; }

		/// <summary>
		/// Increases the current point total for this entry.
		/// </summary>
		/// <param name="points">
		/// The number of additional points to add.
		/// </param>
		void AddToPoints(int points);
	}
}
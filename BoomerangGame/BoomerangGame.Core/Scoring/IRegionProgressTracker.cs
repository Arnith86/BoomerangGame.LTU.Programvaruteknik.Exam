namespace BoomerangGame.Core.Scoring;

/// <summary>
/// Defines a contract for tracking region completion throughout the game. <br/>
/// Implementations determine when a player is eligible to receive region-based bonuses <br/>
/// and maintain state across rounds.
/// </summary>
public interface IRegionProgressTracker
{
	/// <summary>
	/// Determines whether the specified region is eligible for a bonus in the given round.
	/// </summary>
	/// <param name="region">The region being checked.</param>
	/// <param name="currentRoundNumber">The current round number of the game.</param>
	/// <returns>
	/// <c>true</c> if the region is considered eligible for a bonus, otherwise, <c>false</c>.
	/// </returns>
	bool EligibleForRegionBonus(string region, int currentRoundNumber);

	/// <summary>
	/// Attempts to mark the specified region as completed in the given round.
	/// </summary>
	/// <param name="region">The region to mark as completed.</param>
	/// <param name="roundNumber">The round number in which the region was completed.</param>
	/// <returns>
	/// <c>true</c> if the region did not already exist and was successfully recorded;  
	/// <c>false</c> if the region had already been marked previously.
	/// </returns>
	bool MarkRegionAsCompleted(string region, int roundNumber);

	/// <summary>
	/// Clears all tracked region progress, resetting the internal state.
	/// </summary>
	void Reset();
}
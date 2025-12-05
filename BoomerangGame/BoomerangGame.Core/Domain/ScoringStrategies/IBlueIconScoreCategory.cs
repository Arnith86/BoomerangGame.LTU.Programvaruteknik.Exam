using BoomerangGame.Core.Domain.States.PlayerState;

namespace BoomerangGame.Core.Domain.ScoringStrategies;

/// <summary>
/// Represents a scoring category that awards points based on blue icon tiles.
/// </summary>
/// <remarks>
/// This category handles score calculation for blue icon-related mechanics within the Boomerang <br/>
/// game, using the player's current state and the specific scoring category being evaluated.
/// </remarks>
public interface IBlueIconScoreCategory : IScoreCategory
{
	/// <summary>
	/// Calculates the score for the blue icon scoring category.
	/// </summary>
	/// <param name="playerState">The state of the player for whom the score is being calculated.</param>
	/// <param name="category">The represents which type of blue iconis to be calculated</param>
	/// <returns>The number of points earned for this category.</returns>
	int CalculateScore(IBoomerangPlayerState playerState, string? category);
}

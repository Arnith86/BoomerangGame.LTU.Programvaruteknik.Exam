namespace BoomerangGame.Core.Domain.ScoringStrategies;

/// <summary>
/// Tracks region completion progress during the game and determines whether a player is <br/>
/// eligible for a region bonus.
/// </summary>
/// <remarks>Implemented as a simple in-memory tracker. Not thread-safe.</remarks>
public class RegionProgressTracker : IRegionProgressTracker
{
	private readonly Dictionary<string, int> _completedRegions = new();

	/// <inheritdoc/>
	public bool MarkRegionAsCompleted(string region, int roundNumber)
	{
		if (!_completedRegions.ContainsKey(region))
		{
			_completedRegions[region] = roundNumber;
			return true;
		}

		return false;
	}

	/// <inheritdoc/>
	public bool EligibleForRegionBonus(string region, int currentRoundNumber)
	{
		bool eligible = true;

		if (_completedRegions.TryGetValue(region, out int completedRoundNumber))
			return currentRoundNumber.Equals(completedRoundNumber);
		
		return eligible;
	}

	/// <inheritdoc/>
	public void Reset()
	{
		_completedRegions.Clear();
	}
}

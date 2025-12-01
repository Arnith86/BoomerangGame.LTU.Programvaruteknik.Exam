namespace BoomerangGame.Core.Domain.States;

/// <summary>
/// Represents the state of regions in a game, tracking which sites have been visited.
/// </summary>
public interface IRegionsState
{
	/// <summary>
	/// Marks the specified site as visited in the region state.
	/// </summary>
	/// <param name="site">The identifier of the site that has been visited.</param>
	void UpdateRegionsState(string site);

	/// <summary>
	/// Checks whether all sites in the region have been visited.
	/// </summary>
	/// <returns>
	/// <c>true</c> if all sites have been visited; otherwise, <c>false</c>.
	/// </returns>
	bool IsComplete();
}
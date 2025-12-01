namespace BoomerangGame.Core.Domain.States;

/// <summary>
/// Represents the state of the game's map associated with a single player, <br/>
/// containing regions and their associated site-tracking states.
/// </summary>
public interface IMapState
{
	/// <summary>
	/// Gets a read-only dictionary that maps each region name to its corresponding <see cref="IRegionsState"/>.
	/// </summary>
	/// <remarks>
	/// The dictionary reflects the current state of all regions in the map but cannot be modified directly. <br/>
	/// Updates should be performed through <see cref="UpdateMapState"/>.
	/// </remarks>
	IReadOnlyDictionary<string, IRegionsState> CurrentMapState { get; }

	/// <summary>
	/// Updates the state of a specific region by marking the given site as visited, and performs <br/>
	/// any other updates that are needed.
	/// </summary>
	/// <param name="region">The name of the region to update.</param>
	/// <param name="site">The site within the region to mark as visited.</param>
	/// <exception cref="ArgumentException">
	/// Thrown if the specified region does not exist in the current map state.
	/// </exception>
	void UpdateMapState(string region, string site);
}

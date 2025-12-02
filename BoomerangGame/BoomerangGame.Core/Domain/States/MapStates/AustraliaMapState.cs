namespace BoomerangGame.Core.Domain.States.MapStates;

/// <summary>
/// Represents the map state for a single player in the Australia game board, where each region tracks <br/>
/// the progress of visited sites. Provides functionality to initialize and update region states.
/// </summary>
public class AustraliaMapState : IMapState
{
	private readonly Dictionary<string, IRegionsState> _currentMapState = new();

	/// <inheritdoc/>
	public IReadOnlyDictionary<string, IRegionsState> CurrentMapState => 
		_currentMapState;


	/// <summary>
	/// Initializes a new instance of the <see cref="AustraliaMapState"/> class using the provided region-to-sites <br/> 
	/// mapping. Each region is initialized with a corresponding <see cref="RegionState"/> based on its site list. 
	/// </summary>
	/// <param name="map">
	/// A dictionary where each key is a region name and each value is a list of site names associated with that region.
	/// </param>
	/// <exception cref="ArgumentException">
	/// Thrown when <paramref name="map"/> is <c>null</c> or contains no regions.
	/// </exception>
	public AustraliaMapState(Dictionary<string, List<string>> map)
	{
		InitializeMapState(map);
	}


	private void InitializeMapState(Dictionary<string, List<string>> map) 
	{
		if (map is null || map.Count == 0)
			throw new ArgumentException("Map cannot be null or empty.");

		foreach (KeyValuePair<string, List<string>> region in map)
		{
			_currentMapState[region.Key] = new RegionState(region.Value);
		}
	}

	/// <inheritdoc/>
	public void UpdateMapState(string region, string site)
	{
		if (!_currentMapState.TryGetValue(region, out IRegionsState? regionState) || regionState is null)
				throw new ArgumentException($"Region '{region}' does not exist in the map state.");
		
		regionState.UpdateRegionsState(site);
	}
}


namespace BoomerangGame.Core.Domain.States;

public class AustraliaMapState : IMapState
{
	private readonly Dictionary<string, IRegionsState> _currentMapState = new();

	public IReadOnlyDictionary<string, IRegionsState> CurrentMapState => 
		_currentMapState;

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

	public void UpdateMapState(string region, string site)
	{
		if (!_currentMapState.TryGetValue(region, out IRegionsState? regionState) || regionState is null)
				throw new ArgumentException($"Region '{region}' does not exist in the map state.");
		
		regionState.UpdateRegionsState(site);
	}
}

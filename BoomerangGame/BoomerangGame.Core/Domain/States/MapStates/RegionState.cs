namespace BoomerangGame.Core.Domain.States.MapStates;

/// <summary>
/// Tracks the state of a set of sites within a region, recording which sites have been visited.
/// Implements <see cref="IRegionsState"/>.
/// </summary>
public class RegionState : IRegionsState
{
	private readonly List<string> _sites;
	private HashSet<string> _visitedSites = new HashSet<string>();

	/// <summary>
	/// Initializes a new instance of the <see cref="RegionState"/> class with the specified sites.
	/// </summary>
	/// <param name="sites">A list of site identifiers that belong to this region.</param>
	public RegionState(List<string> sites)
	{
		_sites = sites;
	}

	/// <inheritdoc/>
	public bool IsComplete()
	{
		return _sites.All(site => _visitedSites.Contains(site));
	}

	/// <inheritdoc/>
	public void UpdateRegionsState(string site) =>
		_visitedSites.Add(site);
	
}

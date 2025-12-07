namespace BoomerangGame.Core.Domain.States.MapStates.Builder;

/// <summary>
/// Defines a factory for creating <see cref="IMapState"/> instances based on
/// the selected edition and its region configuration.
/// </summary>
public interface IMapStateBuilder
{

	IMapState CreateMapState(string editionName, Dictionary<string, List<string>> regionMap);
}
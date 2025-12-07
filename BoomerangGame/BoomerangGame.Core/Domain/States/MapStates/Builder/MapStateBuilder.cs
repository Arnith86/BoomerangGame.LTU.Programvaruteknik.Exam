namespace BoomerangGame.Core.Domain.States.MapStates.Builder;

public class MapStateBuilder : IMapStateBuilder
{
	public IMapState CreateMapState(
		string editionName,
		Dictionary<string, List<string>> regionMap
	)
	{
		IMapState returnState = default;

		switch (editionName)
		{
			case "Boomerang Australia":
				returnState = new AustraliaMapState(regionMap);
				break;
			default:
				break;
		}

		return returnState;
	}
}

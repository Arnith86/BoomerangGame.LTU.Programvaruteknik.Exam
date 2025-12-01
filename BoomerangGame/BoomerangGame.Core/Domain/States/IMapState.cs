namespace BoomerangGame.Core.Domain.States;

public interface IMapState
{
	void UpdateMapState(string region, string site);
	IReadOnlyDictionary<string, IRegionsState> CurrentMapState { get; }
}

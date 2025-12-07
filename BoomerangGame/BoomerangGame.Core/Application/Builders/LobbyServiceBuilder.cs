using BoomerangGame.Core.Domain.States.MapStates;

namespace BoomerangGame.Core.Application.Builders;

public class LobbyServiceBuilder : ILobbyServiceBuilder
{
	public ILobbyService CreateLobbyService(IMapState mapState)
		=> new LobbyService(mapState);
}

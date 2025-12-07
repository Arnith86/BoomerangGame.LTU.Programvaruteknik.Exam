using BoomerangGame.Core.Domain.States.MapStates;

namespace BoomerangGame.Core.Application.Builders
{
	public interface ILobbyServiceBuilder
	{
		ILobbyService CreateLobbyService(IMapState mapState);
	}
}
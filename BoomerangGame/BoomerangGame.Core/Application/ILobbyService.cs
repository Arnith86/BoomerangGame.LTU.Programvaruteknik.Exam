using BoomerangGame.Core.Application;
using BoomerangGame.Core.Port;

public interface ILobbyService
{
	IPlayer AddPlayer(IPlayerChannel channel);
	bool ValidatePlayerCount();
	List<IPlayer> GetPlayers();
	Task<bool> IsReady();
}
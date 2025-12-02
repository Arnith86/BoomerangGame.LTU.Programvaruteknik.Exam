using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Port;

namespace BoomerangGame.Core.Application;

public interface IPlayer
{
	IPlayerState PlayerState { get; }
	IPlayerChannel PlayerChannel { get; }
}

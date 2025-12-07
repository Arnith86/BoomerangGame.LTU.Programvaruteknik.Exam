using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Port;

namespace BoomerangGame.Core.Application;

public record Player(
	IBoomerangPlayerState PlayerState,
	IPlayerChannel PlayerChannel
) : IPlayer;
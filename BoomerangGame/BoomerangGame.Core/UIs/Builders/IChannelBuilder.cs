using BoomerangGame.Core.Port;

namespace BoomerangGame.Core.UIs.Builders;

/// <summary>
/// Defines a factory abstraction responsible for creating different types of 
/// <see cref="IPlayerChannel"/> instances used throughout the game.
/// <para>
/// Implementations of this interface provide channel configurations for bots, 
/// human players, and server-side player sessions, allowing the UI layer to 
/// obtain the appropriate communication channel without knowing the concrete types.
/// </para>
/// </summary>
public interface IChannelBuilder
{
	IPlayerChannel GetBotPlayerChannel();
	IPlayerChannel GetPlayerSessionChannel();
	IPlayerChannel GetServerPlayerChannel();
}
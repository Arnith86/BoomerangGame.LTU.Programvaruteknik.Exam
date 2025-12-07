using BoomerangGame.Core.Port;

namespace BoomerangGame.Core.UIs.Builders;

public class ChannelBuilder : IChannelBuilder
{
	private readonly IUiBuilder _uiBuilder;

	public ChannelBuilder(IUiBuilder uiBuilder)
	{
		_uiBuilder = uiBuilder;
	}

	public IPlayerChannel GetServerPlayerChannel() => new ConsolePlayerChannel(_uiBuilder.GetUi());
	public IPlayerChannel GetPlayerSessionChannel() => new ClientSession(_uiBuilder.GetUi());
	public IPlayerChannel GetBotPlayerChannel() => new BotPlayerChannel(_uiBuilder.GetUi());
}

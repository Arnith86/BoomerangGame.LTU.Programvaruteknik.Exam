using BoomerangGame.Core.UIs;

namespace BoomerangGame.Core.Port;

public class BotPlayerChannel : IPlayerChannel  
{
	private readonly IUI _uI;

	public BotPlayerChannel(IUI uI)
	{
		_uI = uI;
	}
}

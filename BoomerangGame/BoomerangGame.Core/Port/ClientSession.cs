using BoomerangGame.Core.UIs;

namespace BoomerangGame.Core.Port;

public class ClientSession : IPlayerChannel
{
	private readonly IUI _uI;

	public ClientSession(IUI uI)
	{
		_uI = uI;
	}
}

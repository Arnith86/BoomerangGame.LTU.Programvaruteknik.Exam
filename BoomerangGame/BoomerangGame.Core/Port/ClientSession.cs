using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.UIs;

namespace BoomerangGame.Core.Port;

public class ClientSession : IPlayerChannel
{
	private readonly IUI _uI;

	public ClientSession(IUI uI)
	{
		_uI = uI;
	}

	public Task<IBoomerangCard<string>> PromptForCard(List<IBoomerangCard<string>> hand)
	{
		throw new NotImplementedException();
	}

	public Task<string?> ReceiveMessageAsync()
	{
		throw new NotImplementedException();
	}

	public Task SendMessageAsync(string msg)
	{
		throw new NotImplementedException();
	}
}

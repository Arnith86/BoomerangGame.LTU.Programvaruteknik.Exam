using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.UIs;

namespace BoomerangGame.Core.Port;

public class BotPlayerChannel : IPlayerChannel  
{
	private readonly IUI _uI;

	public BotPlayerChannel(IUI uI)
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

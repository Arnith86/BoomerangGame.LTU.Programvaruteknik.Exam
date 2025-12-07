using BoomerangGame.Core.Domain.Cards;

namespace BoomerangGame.Core.Port
{
	public interface IPlayerChannel
	{
		Task<IBoomerangCard<string>> PromptForCard(List<IBoomerangCard<string>> hand);
		Task SendMessageAsync(string msg);

		Task<string?> ReceiveMessageAsync();
		
	}
}
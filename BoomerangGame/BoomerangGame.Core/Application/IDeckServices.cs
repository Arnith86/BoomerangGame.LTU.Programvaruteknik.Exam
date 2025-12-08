using BoomerangGame.Core.Domain.Cards;

namespace BoomerangGame.Core.Application
{
	public interface IDeckServices
	{
		Dictionary<string, List<IBoomerangCard<string>>> GetPlayerHands(List<IBoomerangCard<string>> deck);
	}
}
using BoomerangGame.Core.Domain.Cards;

namespace BoomerangGame.Core.Application;

public class DeckServices : IDeckServices
{
	public Dictionary<string, List<IBoomerangCard<string>>> GetPlayerHands(List<IBoomerangCard<string>> deck)
	{
		throw new NotImplementedException();
	}

	private List<IBoomerangCard<string>> ShuffleDeck(List<IBoomerangCard<string>> deck)
	{
		throw new NotImplementedException();
	}
}

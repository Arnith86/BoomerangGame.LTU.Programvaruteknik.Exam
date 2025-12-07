// Ignore Spelling: dto

namespace BoomerangGame.Core.Config.Factories.Decks;

/// <summary>
/// Mapper that maps deck from one type of card to another.
/// </summary>
public class BoomerangDeckMapper : IDeckMapper
{
	/// <inheritdoc/>
	public IEnumerable<TOut> MapDeck<TIn, TOut>(
		List<TIn> inputCards,
		Func<TIn, TOut> converter)
	{
		if (inputCards is null)
			throw new ArgumentNullException(nameof(inputCards), "cards cannot be null.");

		var deck = new List<TOut>(inputCards.Count);

		foreach (var card in inputCards)
		{
			if (card is null) continue;

			deck.Add(converter(card));
		}

		return deck;
	}
}

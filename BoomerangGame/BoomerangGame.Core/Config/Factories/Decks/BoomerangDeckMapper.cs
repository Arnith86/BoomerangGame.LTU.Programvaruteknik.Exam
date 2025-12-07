// Ignore Spelling: dto

using BoomerangGame.Core.Config.ConfigurationDTOs;
using BoomerangGame.Core.Config.Factories.Symbols;
using BoomerangGame.Core.Domain.Cards;

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


	///// <summary>
	///// Converts a DTO to a Boomerang card definition, keeping all symbols.
	///// </summary>
	//public Func<BoomerangCardDefinitionDto, BoomerangCardDefinition<string>> CreateDtoToDefinitionMapper(
	//	ISymbolSetMapper mapper)
	//{
	//	if (mapper is null) throw new ArgumentNullException(nameof(mapper));

	//	return dto => new BoomerangCardDefinition<string>(
	//		name: dto.Name,
	//		region: dto.Region,
	//		site: dto.Site,
	//		number: dto.Number,
	//		symbols: mapper.MapSymbols(dto.Symbols)
	//	);
	//}

	///// <summary>
	///// Converts a Boomerang card definition to a Boomerang card.
	///// </summary>
	//public Func<BoomerangCardDefinition<string>, IBoomerangCard<string>> CreateDefinitionToBoomerangCardMapper(
	//	ISymbolSetMapper mapper)
	//{
	//	if (mapper is null) throw new ArgumentNullException(nameof(mapper));

	//	return def => new BoomerangCard<string>(
	//		name: def.Name,
	//		region: def.Region,
	//		site: def.Site,
	//		number: def.Number,
	//		symbols: def.Symbols
	//	);
	//}
}

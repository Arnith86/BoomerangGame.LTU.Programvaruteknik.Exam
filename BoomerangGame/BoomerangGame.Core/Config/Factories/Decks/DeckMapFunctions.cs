// Ignore Spelling: Dto

using BoomerangGame.Core.Config.ConfigurationDTOs;
using BoomerangGame.Core.Config.Factories.Symbols;
using BoomerangGame.Core.Domain.Cards;

namespace BoomerangGame.Core.Config.Factories.Decks;

public class DeckMapFunctions : IDeckMapFunctions
{

	/// <summary>
	/// Converts a DTO to a Boomerang card definition, keeping all symbols.
	/// </summary>
	public Func<BoomerangCardDefinitionDto, BoomerangCardDefinition<string>> CreateDtoToDefinitionMapper(
		ISymbolSetMapper mapper)
	{
		if (mapper is null) throw new ArgumentNullException(nameof(mapper));

		return dto => new BoomerangCardDefinition<string>(
			name: dto.Name,
			region: dto.Region,
			site: dto.Site,
			number: dto.Number,
			symbols: mapper.MapSymbols(dto.Symbols)
		);
	}

	/// <summary>
	/// Converts a Boomerang card definition to a Boomerang card.
	/// </summary>
	public Func<BoomerangCardDefinition<string>, IBoomerangCard<string>> CreateDefinitionToBoomerangCardMapper(
		ISymbolSetMapper mapper)
	{
		if (mapper is null) throw new ArgumentNullException(nameof(mapper));

		return def => new BoomerangCard<string>(
			name: def.Name,
			region: def.Region,
			site: def.Site,
			number: def.Number,
			symbols: def.Symbols
		);
	}
}

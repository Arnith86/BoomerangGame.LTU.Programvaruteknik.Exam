using BoomerangGame.Core.Config.ConfigurationDTOs;
using BoomerangGame.Core.Config.Factories.Symbols;
using BoomerangGame.Core.Domain.Cards;

namespace BoomerangGame.Core.Config.Factories.Decks
{
	public interface IDeckMapFunctions
	{
		Func<BoomerangCardDefinition<string>, IBoomerangCard<string>> CreateDefinitionToBoomerangCardMapper(ISymbolSetMapper mapper);
		Func<BoomerangCardDefinitionDto, BoomerangCardDefinition<string>> CreateDtoToDefinitionMapper(ISymbolSetMapper mapper);
	}
}
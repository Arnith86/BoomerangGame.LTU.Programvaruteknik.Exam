// Ignore Spelling: Json Dto RQ

using BoomerangGame.Core.Config;
using BoomerangGame.Core.Config.ConfigurationDTOs;
using BoomerangGame.Core.Config.Factories.Decks;
using BoomerangGame.Core.Config.Factories.Symbols;
using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.States.MapStates.Builder;
using BoomerangGame.Core.Scoring;
using BoomerangGame.Core.Tests.HelperClasses;

namespace BoomerangGame.Core.Tests.ConfigTests;

public class BoomerangDeckMapperTests
{
	private readonly BoomerangDeckMapper _sut;
	private readonly IEditionLoader _editionLoader;
	private readonly IRegionProgressTracker _regionProgressTracker;
	private readonly EditionConfigDto _editionConfigDto;
	private readonly ISymbolSetMapper _symbolSetMapper;
	private readonly IDeckMapFunctions _deckMapFunctions;
	private readonly IDeckMapper _deckMapper;
	private readonly IMapStateBuilder _mapStateBuilde;

	private readonly IEnumerable<BoomerangCardDefinition<string>> _deckDefinition;

	public BoomerangDeckMapperTests()
	{
		_deckMapFunctions = new DeckMapFunctions();
		_deckMapper = new BoomerangDeckMapper();
		_regionProgressTracker = new RegionProgressTracker();
		_mapStateBuilde = new MapStateBuilder();

		_editionLoader = new EditionLoader(
			_regionProgressTracker, 
			_deckMapper, 
			_deckMapFunctions, 
			_mapStateBuilde
		);

		_editionConfigDto =
			_editionLoader.LoadEditionDto(GetEditionJSON.GetValidEditionConfigJSON());

		_symbolSetMapper = SymbolSetMapperFactory.GetMapper(_editionConfigDto.Name);

		_sut = new BoomerangDeckMapper();

		_deckDefinition = _sut.MapDeck(
			_editionConfigDto.Deck,
			_deckMapFunctions.CreateDtoToDefinitionMapper(_symbolSetMapper)
		);
	}

	[Fact]
	public void MapDeck_From_BoomerangCardDefinitionDto_To_BoomerangCardDefinition_28ConvertedCardsShouldBeReturned_RQ2()
	{
		// Arrange & Act 
		var result = _sut.MapDeck(
			_editionConfigDto.Deck,
			_deckMapFunctions.CreateDtoToDefinitionMapper(_symbolSetMapper)
		);

		// Assert
		Assert.Equal(28, result.Count());
		Assert.IsType<List<BoomerangCardDefinition<string>>>(result);
	}

	[Fact]
	public void MapDeck_From_BoomerangCardDefinition_To_IBoomerangCard_28ConvertedCardsShouldBeReturned_RQ2()
	{
		// Act
		var result = _sut.MapDeck(
			_deckDefinition.ToList(),
			_deckMapFunctions.CreateDefinitionToBoomerangCardMapper(_symbolSetMapper)
		);

		// Assert
		Assert.Equal(28, result.Count());
		Assert.IsType<List<IBoomerangCard<string>>>(result);
	}

	[Fact]
	public void EachCard_ShouldHaveNameAndSite_RQ2a()
	{
		var deck = _sut.MapDeck(
			_deckDefinition.ToList(),
			_deckMapFunctions.CreateDefinitionToBoomerangCardMapper(_symbolSetMapper)
		);

		foreach (var card in deck)
		{
			Assert.False(string.IsNullOrWhiteSpace(card.Name));
			Assert.True(!string.IsNullOrWhiteSpace(card.Site)
				&& card.Site.Length == 1 && char.IsLetter(card.Site[0]));
		}

		Assert.Equal(_deckDefinition.First().Name, deck.First().Name);
		Assert.Equal(_deckDefinition.First().Site, deck.First().Site);
	}

	[Fact]
	public void EachCard_ShouldHaveValidNumber_RQ2b()
	{
		var deck = _sut.MapDeck(
			_deckDefinition.ToList(),
			_deckMapFunctions.CreateDefinitionToBoomerangCardMapper(_symbolSetMapper)
		);

		foreach (var card in deck)
		{
			Assert.True(card.Number >= 0);
		}
	}


	[Fact]
	public void EachCard_ShouldHaveTwoValidSymbols_RQ2c_i_ii_iii()
	{
		var validCollections = new[] { "Leaves", "Wildflowers", "Shells", "Souvenirs" };
		var validAnimals = new[] { "Kangaroos", "Emus", "Wombats", "Koalas", "Platypuses" };
		var validActivities = new[] { "Swimming", "Bushwalking", "Indigenous Culture", "Sightseeing" };
		var validCategories = validCollections.Concat(validAnimals).Concat(validActivities).ToList();

		var deck = _sut.MapDeck(
			_deckDefinition.ToList(),
			_deckMapFunctions.CreateDefinitionToBoomerangCardMapper(_symbolSetMapper));

		foreach (var card in deck)
		{
			var symbols = card.Symbols.Symbols;

			Assert.Equal(2, symbols.Count());

			foreach (var symbol in symbols)
			{
				Assert.Contains(symbol.Value, validCategories);
			}
		}
	}
}

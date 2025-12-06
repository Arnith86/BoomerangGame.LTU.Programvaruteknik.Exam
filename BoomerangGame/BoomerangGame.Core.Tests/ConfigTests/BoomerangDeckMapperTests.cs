// Ignore Spelling: Json Dto

using BoomerangGame.Core.Config;
using BoomerangGame.Core.Config.ConfigurationDTOs;
using BoomerangGame.Core.Config.Factories;
using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Scoring;
using BoomerangGame.Core.Tests.HelperClasses;

namespace BoomerangGame.Core.Tests.ConfigTests;

public class BoomerangDeckMapperTests
{
	private readonly IEditionLoader _editionLoader;
	private readonly IRegionProgressTracker _regionProgressTracker;
	private readonly EditionConfigDto _editionConfigDto;

	public BoomerangDeckMapperTests()
	{
		_regionProgressTracker = new RegionProgressTracker();
		_editionLoader = new EditionLoader(_regionProgressTracker);
		_editionConfigDto = 
			_editionLoader.LoadEditionDto(GetEditionJSON.GetValidEditionConfigJSON());

	}

	[Fact]
	public void MapDeck_From_BoomerangCardDefinitionDto_To_BoomerangCardDefinition_28ConvertedCardsShouldBeReturned()
	{
		// Arrange & Act 
		var sut = new BoomerangDeckMapper();
		var result = sut.MapDeck(_editionConfigDto.Deck, sut.dtoToDefinition);

		// Assert
		Assert.Equal(28, result.Count());
		Assert.IsType<List<BoomerangCardDefinition<string>>>(result);
	}

	[Fact]
	public void MapDeck_From_BoomerangCardDefinition_To_IBoomerangCard_28ConvertedCardsShouldBeReturned()
	{
		// Arrange 
		var sut = new BoomerangDeckMapper();
		var firstConversion = sut.MapDeck(_editionConfigDto.Deck, sut.dtoToDefinition);

		// Act
		var result = sut.MapDeck(
			inputCards: (List<BoomerangCardDefinition<string>>)firstConversion,
			sut.definitionToCard
		);

		// Assert
		Assert.Equal(28, result.Count());
		Assert.IsType<List<IBoomerangCard<string>>>(result);
	}
}

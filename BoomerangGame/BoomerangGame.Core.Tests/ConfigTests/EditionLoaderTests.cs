// Ignore Spelling: Json Dto

using BoomerangGame.Core.Config;
using BoomerangGame.Core.Config.Factories.ScoreCategories;
using BoomerangGame.Core.Scoring;
using BoomerangGame.Core.Tests.HelperClasses;
using Moq;

namespace BoomerangGame.Core.Tests.ConfigTests;

public class EditionLoaderTests
{
	private IEditionLoader _editionLoader;
	private readonly IScoreCategoryFactory _scoreCategoryFactory;
	private readonly IRegionProgressTracker _regionProgressTracker;

	public EditionLoaderTests()
	{
		_scoreCategoryFactory = Mock.Of<IScoreCategoryFactory>();
		_regionProgressTracker = Mock.Of<RegionProgressTracker>();
		_editionLoader = new EditionLoader(_regionProgressTracker);
	}  

	
	[Fact]
	public void LoadEdition_FileDoesNotExist_ShouldThrowFileNotFoundException()
	{
		// Arrange 
		var invalidPath = Path.Combine(Path.GetTempPath(), "none_existant_file.");

		// Act & Assert 
		Assert.Throws<FileNotFoundException>(() => _editionLoader.LoadEditionDto(invalidPath));
	}

	[Fact]
	public void LoadEdition_ValidJson_ShouldReturnEditionConfigDto()
	{
		// Arrange & Act 
		var editionConfigDto = _editionLoader.LoadEditionDto(GetEditionJSON.GetValidEditionConfigJSON());

		// Assert
		Assert.NotNull(editionConfigDto);
		Assert.Equal("Western Australia", editionConfigDto.RegionMap.Keys.First());
		Assert.Equal("Tasmania", editionConfigDto.RegionMap.Keys.Last());
		Assert.Equal("HighestThrowCatchTotal", editionConfigDto.TieBreakerIdentifier);
		Assert.Equal("Left", editionConfigDto.TurnOrderIdentifier);
	}

	[Fact]
	public void LoadEdition_ValidJson_ShouldCreateEditionName()
	{
		// Arrange & Act 
		var editionConfigDto = _editionLoader.LoadEditionDto(GetEditionJSON.GetValidEditionConfigJSON());

		// Assert
		Assert.Equal("Boomerang Australia", editionConfigDto.Name);
	}

	[Fact]
	public void LoadEdition_ValidJson_ShouldCreate28CardForDeck_RQ2()
	{
		// Arrange & Act 
		var editionConfigDto = _editionLoader.LoadEditionDto(GetEditionJSON.GetValidEditionConfigJSON());

		// Assert
		Assert.Equal(28, editionConfigDto.Deck.Count);
	}

	[Fact]
	public void LoadEdition_ValidJson_ShouldCreateCorrectRegionPointMapping()
	{
		// Arrange
		var regionCompletionPoints = new Dictionary<string, int>
		{
			{ "Western Australia", 3 },
			{ "Northern Territory", 3 },
			{ "Queensland", 3 },
			{ "South Australia", 3 },
			{ "New South Whales", 3 },
			{ "Victoria", 3 },
			{ "Tasmania", 3 }
		};

		// Act 
		var editionConfigDto = _editionLoader.LoadEditionDto(GetEditionJSON.GetValidEditionConfigJSON());

		// Assert
		Assert.Equal(regionCompletionPoints, editionConfigDto.RegionCompletionPoints);
	}

	[Fact]
	public void LoadEdition_ValidJson_ShouldCreateCorrectAnimalPointMapping()
	{
		// Arrange
		var animalPointsPerPair = new Dictionary<string, int>
		{
			{ "Kangaroos", 3 },
			{ "Emus", 4 },
			{ "Wombats", 5 },
			{ "Koalas", 7 },
			{ "Platypuses", 9 }
		};

		// Act 
		var editionConfigDto = _editionLoader.LoadEditionDto(GetEditionJSON.GetValidEditionConfigJSON());

		// Assert
		Assert.Equal(animalPointsPerPair, editionConfigDto.AnimalPointsPerPair);
	}
}

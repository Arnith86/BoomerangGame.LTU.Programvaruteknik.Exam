using BoomerangGame.Core.Config;

namespace BoomerangGame.Core.Tests.ConfigTests;

public class EditionLoaderTests
{
	private IEditionLoader _editionLoader;

	public EditionLoaderTests() => _editionLoader = EditionLoader.Instance;

	private string GetValidEditionJson()
	{
		return @"{
			""deck"": 
			[
				{
					""name"": ""The Bungle Bungles"",
					""site"": ""A"",
					""region"": ""Western Australia"",
					""number"": 1,
					""symbols"": 
					{
						""collection"": ""Leaves"",
						""animal"": null,
						""activity"": ""Indigenous Culture""
					},
				},   
				{
					""name"": ""Daintree Rainforest"",
					""site"": ""K"",
					""region"": ""Queensland"",
					""number"": 6,
					""symbols"":
					{
						""collection"": ""Souvenirs"",
						""animal"": null,
						""activity"": ""Bushwalking""
					},
				}
			],
			""regions"": [
				""Western Australia"",
				""Northern Territory"",
				""Queensland"",
				""South Australia"",
				""New South Whales"",
				""Victoria"",
				""Tasmania""
			],
			""scoringStrategies"": [
				""ThrowCatchAbsolute"",
				""RegionBonus3points"",
				""Collection"",
				""Animal"",
				""Activity""
			],
			""tieBreakerIdentifier"": ""HighestThrowCatchTotal"",
			""turnOrderIdentifier"": ""Left"",
		}";
	}

	[Fact]
	public void LoadEdition_FileDoesNotExist_ShouldThrowFileNotFoundException()
	{
		// Arrange 
		var invalidPath = Path.Combine(Path.GetTempPath(), "none_existant_file.");

		// Act & Assert 
		Assert.Throws<FileNotFoundException>(() => _editionLoader.LoadEdition(invalidPath));
	}

	[Fact]
	public void LoadEdition_ValidJson_ShouldReturnEditionConfig()
	{
		// Arrange
		var tempFile = Path.GetTempFileName(); 
		File.WriteAllText(tempFile, GetValidEditionJson());

		// Act 
		var config = _editionLoader.LoadEdition(tempFile);

		// Assert
		Assert.NotNull(config);
		Assert.Single(config.Deck);
		Assert.Equal(2, config.Deck.Count);
		Assert.Equal("Western Australia", config.Regions[0]);
		Assert.Equal("Tasmania", config.Regions[6]);
		Assert.Equal("HighestThrowCatchTotal", config.TieBreakerIdentifier);
		Assert.Equal("left", config.TurnOrderIdentifier);

	}
}

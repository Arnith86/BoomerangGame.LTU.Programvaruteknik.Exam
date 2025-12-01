// Ignore Spelling: Json Dto

using BoomerangGame.Core.Config;

namespace BoomerangGame.Core.Tests.ConfigTests;

public class EditionLoaderTests
{
	private IEditionLoader _editionLoader;

	public EditionLoaderTests() => _editionLoader = EditionLoader.Instance;

	
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
		var editionConfigDto = _editionLoader.LoadEditionDto(GetEditionJSON.GetValidEditionConfigJSON()/*tempFile*/);

		// Assert
		Assert.NotNull(editionConfigDto);
		Assert.Equal(28, editionConfigDto.Deck.Count);
		Assert.Equal("Western Australia", editionConfigDto.RegionMap.Keys.First());
		Assert.Equal("Tasmania", editionConfigDto.RegionMap.Keys.Last());
		Assert.Equal("HighestThrowCatchTotal", editionConfigDto.TieBreakerIdentifier);
		Assert.Equal("Left", editionConfigDto.TurnOrderIdentifier);

	}
}

using BoomerangGame.Core.Config.ConfigurationDTOs;


namespace BoomerangGame.Core.Config;

public interface IEditionLoader
{
	EditionConfigDto LoadEditionDto(string Path);

	/// <summary>
	/// Creates a runtime RuleSet from an EditionConfigDto.
	/// </summary>
	/// <param name="config">Edition configuration dto</param>
	/// <returns>EditionConfig ready for game engine</returns>
	EditionConfig CreateDomain(EditionConfigDto config);
}

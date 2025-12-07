using BoomerangGame.Core.Config.ConfigurationDTOs;


namespace BoomerangGame.Core.Config;

/// <summary>
/// Provides functionality for loading and converting edition configuration files
/// used by the Boomerang game engine.
/// </summary>
public interface IEditionLoader
{
	/// <summary>Loads and deserializes an edition configuration JSON file into an<see cref="EditionConfigDto"/>.</summary>
	/// <param name="path">The full file system path to the edition configuration JSON file.</param>
	/// <returns>A fully populated <see cref="EditionConfigDto"/> representing the data contained in the configuration file.</returns>
	/// <exception cref="FileNotFoundException">Thrown if the provided path does not exist.</exception>
	/// <exception cref="JsonException">Thrown if the JSON file contains invalid or unexpected format.</exception>
	EditionConfigDto LoadEditionDto(string Path);

	/// <summary>
	/// Creates a runtime RuleSet from an EditionConfigDto.
	/// </summary>
	/// <param name="config">Edition configuration dto</param>
	/// <returns>EditionConfig ready for game engine</returns>
	EditionConfig CreateDomain(EditionConfigDto config);
}

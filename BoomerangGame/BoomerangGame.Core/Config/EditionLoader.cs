using BoomerangGame.Core.Config.ConfigurationDTOs;
using BoomerangGame.Core.Domain.RuleSets;
using System.Text.Json;

namespace BoomerangGame.Core.Config;

/// <summary>
/// Singleton loader for game editions.
/// Responsible for reading JSON edition files and creating RuleSets.
/// </summary>
public sealed class EditionLoader : IEditionLoader
{
	
	// Singleton instance
	private static readonly Lazy<EditionLoader> _instance =
		new Lazy<EditionLoader>(() => new EditionLoader());

	public static EditionLoader Instance => _instance.Value;

	// Private constructor for singleton
	private EditionLoader() { }



	/// <summary>
	/// Loads an edition configuration from a JSON file.
	/// </summary>
	/// <param name="path">Path to the edition JSON file</param>
	/// <returns>EditionConfig object with the data</returns>
	public EditionConfigDto LoadEdition(string path)
	{
		if (!File.Exists(path))
			throw new FileNotFoundException($"Edition file not found: {path}");

		var json = File.ReadAllText(path);
		return ParseJson(json);
	}

	/// <summary>
	/// Creates a runtime RuleSet from an EditionConfigDto.
	/// </summary>
	/// <param name="config">Edition configuration</param>
	/// <returns>IRuleSet ready for game engine</returns>
	public IRuleSet CreateRuleSet(EditionConfigDto configDto)
	{
		/*return new RuleSet(config);*/ throw new NotImplementedException();
	}


	/// <summary>
	/// Parses the JSON string into an EditionConfigDto.
	/// </summary>
	/// <param name="json">JSON string</param>
	/// <returns>EditionConfigDto instance</returns>
	private EditionConfigDto ParseJson(string json)
	{
		var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };

		try
		{
			return JsonSerializer.Deserialize<EditionConfigDto>(json, options)
				?? throw new InvalidOperationException("Failed to deserialize EditionConfig.");
		}
		catch (JsonException ex)
		{
			throw new InvalidOperationException("Invalid JSON format for EditionConfig.", ex);
		}
	}
}


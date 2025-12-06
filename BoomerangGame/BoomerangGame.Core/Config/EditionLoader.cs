// Ignore Spelling: Dto

using BoomerangGame.Core.Config.ConfigurationDTOs;
using BoomerangGame.Core.Config.Factories.ScoreCategories;
using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Scoring;
using System.Text.Json;

namespace BoomerangGame.Core.Config;

/// <summary>
/// Singleton loader for game editions.
/// Responsible for reading JSON edition files and creating RuleSets.
/// </summary>
public sealed class EditionLoader : IEditionLoader
{
	
	private readonly IScoreCategoryFactory _scoreCategoryFactory;
	private readonly IRegionProgressTracker _regionProgressTracker;

	public EditionLoader(IRegionProgressTracker regionProgressTracker)
	{
		_regionProgressTracker = regionProgressTracker;
	}

	/// <summary>
	/// Loads an edition configuration from a JSON file.
	/// </summary>
	/// <param name="path">Path to the edition JSON file</param>
	/// <returns>EditionConfig object with the data</returns>
	public EditionConfigDto LoadEditionDto(string path)
	{
		if (!File.Exists(path))
			throw new FileNotFoundException($"Edition file not found: {path}");

		var json = File.ReadAllText(path);
		return ParseJson(json);
	}


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


	public EditionConfig CreateDomain(EditionConfigDto config)
	{
		IScoreCategoryFactory scoreCategoryFactory 
			= AbstractScoreCategoryFactoryProducer.GetFactory(config.Name);


		IEnumerable<IScoreCategory> scoreCategories 
			= scoreCategoryFactory.Create(config, _regionProgressTracker); 

		
		throw new NotImplementedException();
	}
}


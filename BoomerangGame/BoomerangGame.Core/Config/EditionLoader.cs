// Ignore Spelling: Dto

using BoomerangGame.Core.Config.ConfigurationDTOs;
using BoomerangGame.Core.Config.Factories.Decks;
using BoomerangGame.Core.Config.Factories.ScoreCategories;
using BoomerangGame.Core.Config.Factories.Symbols;
using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Domain.States.MapStates.Builder;
using BoomerangGame.Core.Scoring;
using System.Text.Json;

namespace BoomerangGame.Core.Config;

/// <summary>
/// Singleton loader for game editions.
/// Responsible for reading JSON edition files and creating RuleSets.
/// </summary>
public sealed class EditionLoader : IEditionLoader
{
	
	private readonly IRegionProgressTracker _regionProgressTracker;
	private readonly IDeckMapper _deckMapper;
	private readonly IDeckMapFunctions _deckMapFunctions;
	private readonly IMapStateBuilder _mapStateBuilder;

	public EditionLoader(
		IRegionProgressTracker regionProgressTracker, 
		IDeckMapper deckMapper,
		IDeckMapFunctions deckMapFunctions,
		IMapStateBuilder mapStateBuilder)
	{
		_regionProgressTracker = regionProgressTracker;
		_deckMapper = deckMapper;
		_deckMapFunctions = deckMapFunctions;
		_mapStateBuilder = mapStateBuilder;
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


		ISymbolSetMapper symbolSetMapper = SymbolSetMapperFactory.GetMapper(config.Name);
		var dtoToDefinition = _deckMapFunctions.CreateDtoToDefinitionMapper(symbolSetMapper);

		IEnumerable<BoomerangCardDefinition<string>> deck 
			= _deckMapper.MapDeck(
				config.Deck,
				dtoToDefinition);

		return new EditionConfig(
			Name: config.Name,
			Deck: (IReadOnlyList<BoomerangCardDefinition<string>>)deck,
			RegionMap: _mapStateBuilder.CreateMapState(config.Name, config.RegionMap),
			RegionProgressTracker: _regionProgressTracker,
			ScoringStrategies: scoreCategories.ToList(),
			TieBreakerIdentifier: config.TieBreakerIdentifier,
			TurnOrderIdentifier: config.TurnOrderIdentifier,
			RegionCompletionPoints: config.RegionCompletionPoints,
			AnimalPointsPerPair: config.AnimalPointsPerPair
		);	
	}
}


// Ignore Spelling: Dto

namespace BoomerangGame.Core.Config.ConfigurationDTOs;

public class EditionConfigDto
{
	public string Name { get; set; } = default!;
	public List<BoomerangCardDefinitionDto> Deck { get; set; } = new();
	public Dictionary<string, List<string>> RegionMap { get; set; } = new();
	public string RegionTrackingIdentifier { get; set; } = default!;
	public List<string> ScoringStrategies { get; set; } = new();
	public string TieBreakerIdentifier { get; set; } = default!;
	public string TurnOrderIdentifier { get; set; } = default!;
}




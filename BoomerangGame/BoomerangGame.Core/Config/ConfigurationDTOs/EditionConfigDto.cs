// Ignore Spelling: Dto

namespace BoomerangGame.Core.Config.ConfigurationDTOs;

public class EditionConfigDto
{
	public string Name { get; set; } = default!;
	public List<BoomerangCardDefinitionDto> Deck { get; set; } = new();
	public List<string> Regions { get; set; } = new();
	public List<string> ScoringStrategies { get; set; } = new();
	public string TieBreakerIdentifier { get; set; } = default!;
	public string TurnOrderIdentifier { get; set; } = default!;
}




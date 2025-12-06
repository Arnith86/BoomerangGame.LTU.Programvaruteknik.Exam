// Ignore Spelling: Dto

namespace BoomerangGame.Core.Config.ConfigurationDTOs;

public sealed class BoomerangCardDefinitionDto
{
	public string Name { get; set; } = default!;
	public string Region { get; set; } = default!;
	public string Site { get; set; } = default!;
	public int Number { get; set; }
	public object Symbols { get; set; } = default!;
}

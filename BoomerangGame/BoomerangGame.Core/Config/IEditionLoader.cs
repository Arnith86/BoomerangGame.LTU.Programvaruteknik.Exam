using BoomerangGame.Core.Config.ConfigurationDTOs;
using BoomerangGame.Core.Domain.RuleSets;

namespace BoomerangGame.Core.Config;

public interface IEditionLoader
{
	EditionConfigDto LoadEditionDto(string Path);

	
	IRuleSet CreateEditionConfig(EditionConfigDto config);

	/// <summary>
	/// Creates a runtime RuleSet from an EditionConfigDto.
	/// </summary>
	/// <param name="config">Edition configuration</param>
	/// <returns>RuleSet ready for game engine</returns>
	EditionConfig CreateDomain(EditionConfigDto config);
}

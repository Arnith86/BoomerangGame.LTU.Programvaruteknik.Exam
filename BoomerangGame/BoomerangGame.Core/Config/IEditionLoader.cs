using BoomerangGame.Core.Config.ConfigurationDTOs;
using BoomerangGame.Core.Domain.RuleSets;

namespace BoomerangGame.Core.Config;

public interface IEditionLoader
{
	EditionConfigDto LoadEdition(string Path);

	IRuleSet CreateRuleSet(EditionConfigDto config);
}

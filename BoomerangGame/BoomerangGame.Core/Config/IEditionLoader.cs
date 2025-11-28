using BoomerangGame.Core.Domain.RuleSets;

namespace BoomerangGame.Core.Config;

public interface IEditionLoader
{
	EditionConfig LoadEdition(string Path);

	IRuleSet CreateRuleSet(EditionConfig config);
}

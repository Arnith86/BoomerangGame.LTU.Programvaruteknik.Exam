using BoomerangGame.Core.Config.ConfigurationDTOs;
using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Scoring;

namespace BoomerangGame.Core.Config.Factories.ScoreCategories;

public interface IScoreCategoryFactory
{
	IEnumerable<IScoreCategory> Create(
		EditionConfigDto config, 
		IRegionProgressTracker regionProgressTracker
	);
}

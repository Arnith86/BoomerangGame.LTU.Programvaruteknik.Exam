using BoomerangGame.Core.Config.ConfigurationDTOs;
using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Domain.ScoringStrategies.Strategies;
using BoomerangGame.Core.Scoring;

namespace BoomerangGame.Core.Config.Factories.ScoreCategories
{
	public class AustraliaScoreCategoryFactory : IScoreCategoryFactory
	{
		public IEnumerable<IScoreCategory> Create(
			EditionConfigDto config, 
			IRegionProgressTracker regionProgressTracker
		)
		{
			List<IScoreCategory> scoreCategories = new List<IScoreCategory>();
			
			scoreCategories.Add(CreateThrowCatchAbsolute(config.Name));
			scoreCategories.Add(CreateTouristSiteScore(config.Name));
			scoreCategories.Add(CreateRegionBonusPoints(
				regionProgressTracker, 
				config.RegionCompletionPoints, 
				config.Name)
			);
			scoreCategories.Add(CreateCollection(config.Name));
			scoreCategories.Add(CreateAnimal(
				config.AnimalPointsPerPair!, 
				config.Name)
			);
			scoreCategories.Add(CreateActivity(config.Name));

			return scoreCategories;
		}

		private IScoreCategory CreateThrowCatchAbsolute(string name) 
			=> new ThrowCatchAbsoluteScore(name);
		private IScoreCategory CreateTouristSiteScore(string name)
			=> new TouristSiteScore(name);

		private IScoreCategory CreateRegionBonusPoints(
			IRegionProgressTracker regionProgressTracker,
			Dictionary<string, int> regionCompletionPoints,
			string name) 
			=> new RegionBonusPointScore(regionProgressTracker, regionCompletionPoints, name);
		
		private IScoreCategory CreateCollection(string name) 
			=> new CollectionScore(name);
		
		private IScoreCategory CreateAnimal( Dictionary<string, int> pointsPerPair, string name ) 
			=> new AnimalScore(pointsPerPair, name);

		private IScoreCategory CreateActivity(string name) 
			=> new ActivityScore(name);
	}
}
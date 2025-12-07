using BoomerangGame.Core.Domain.ScoringStrategies;

namespace BoomerangGame.Core.Scoring.Builder;

public class ScoreEngineBuilder : IScoreEngineBuilder
{
	public IScoreEngine CreateScoreEngine(
		IEnumerable<IScoreCategory> scoreCategories, 
		ITieBreaker tieBreaker, 
		string tieBreakerIdentifier
	)
	{
		return new ScoreEngine(scoreCategories, tieBreaker, tieBreakerIdentifier);
	}
}

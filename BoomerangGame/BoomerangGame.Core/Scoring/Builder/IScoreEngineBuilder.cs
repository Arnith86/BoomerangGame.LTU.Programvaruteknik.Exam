using BoomerangGame.Core.Domain.ScoringStrategies;

namespace BoomerangGame.Core.Scoring.Builder;

public interface IScoreEngineBuilder
{
	IScoreEngine CreateScoreEngine(
		IEnumerable<IScoreCategory> scoreCategories,
		ITieBreaker tieBreaker,
		string tieBreakerIdentifier
	);
}
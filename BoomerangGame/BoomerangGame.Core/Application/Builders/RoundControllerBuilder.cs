using BoomerangGame.Core.Config;
using BoomerangGame.Core.Domain.TurnOrders.Builder;
using BoomerangGame.Core.Scoring;
using BoomerangGame.Core.Scoring.Builder;

namespace BoomerangGame.Core.Application.Builders;


public class RoundControllerBuilder : IRoundControllerBuilder
{
	private readonly ITieBreaker _tieBreaker;
	private readonly ITurnOrderBuilder _turnOrderBuilder;
	private readonly EditionConfig _editionConfig;

	public RoundControllerBuilder(
		ITieBreaker tieBreaker, 
		ITurnOrderBuilder turnOrderBuilder,
		EditionConfig editionConfig
	)
	{
		_tieBreaker = tieBreaker;
		_turnOrderBuilder = turnOrderBuilder;
		_editionConfig = editionConfig;
	}

	public IRoundController CreateRoundController(IScoreEngineBuilder scoreEngineBuilder, EditionConfig editionConfig)
		=> new RoundController(
			scoreEngineBuilder.CreateScoreEngine(
				editionConfig.ScoringStrategies,
				_tieBreaker,
				editionConfig.TieBreakerIdentifier),
			_turnOrderBuilder.CreateTurnOrder(_editionConfig.TurnOrderIdentifier)	
		);
}

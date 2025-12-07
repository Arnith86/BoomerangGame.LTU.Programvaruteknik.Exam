using BoomerangGame.Core.Config;
using BoomerangGame.Core.Scoring.Builder;

namespace BoomerangGame.Core.Application.Builders;

public class GameServiceBuilder : IGameServiceBuilder
{
	public IGameServices CreateGameServices(
		EditionConfig editionConfig,
		IScoreBoardService scoreBoardService,
		IScoreEngineBuilder scoreEngineBuilder,
		IRoundControllerBuilder roundControllerBuilder,
		ITieBreakerBuilder tieBreakerBuilder)
		=> new GameServices(
			editionConfig,
			scoreBoardService,
			scoreEngineBuilder,
			roundControllerBuilder,
			tieBreakerBuilder);
}

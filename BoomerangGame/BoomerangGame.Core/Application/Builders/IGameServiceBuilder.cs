using BoomerangGame.Core.Config;
using BoomerangGame.Core.Scoring.Builder;

namespace BoomerangGame.Core.Application.Builders
{
	public interface IGameServiceBuilder
	{
		IGameServices CreateGameServices(EditionConfig editionConfig, IScoreBoardService scoreBoardService, IScoreEngineBuilder scoreEngineBuilder, IRoundControllerBuilder roundControllerBuilder, ITieBreakerBuilder tieBreakerBuilder);
	}
}
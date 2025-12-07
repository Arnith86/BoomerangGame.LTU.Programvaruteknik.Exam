using BoomerangGame.Core.Config;
using BoomerangGame.Core.Scoring.Builder;

namespace BoomerangGame.Core.Application.Builders;

public interface IRoundControllerBuilder
{
	IRoundController CreateRoundController(
		IScoreEngineBuilder scoreEngineBuilder,
		EditionConfig editionConfig
	);


}


using BoomerangGame.Core.Application.Builders;
using BoomerangGame.Core.Config;
using BoomerangGame.Core.Scoring;
using BoomerangGame.Core.Scoring.Builder;

namespace BoomerangGame.Core.Application;

public class GameServices : IGameServices
{
	private List<IPlayer> _players;

	private IScoreEngine _scoreEngine;
	private IRoundController _roundController;
	private readonly EditionConfig _editionConfig;
	private readonly IScoreBoardService _scoreBoardService;
	
	public GameServices(
		EditionConfig editionConfig,
		IScoreBoardService scoreBoardService,
		IScoreEngineBuilder scoreEngineBuilder,
		IRoundControllerBuilder roundControllerBuilder,
		ITieBreakerBuilder tieBreakerBuilder,
		IDeckServiceBuilder deckServicesBuilder
	)
	{
		_editionConfig = editionConfig;
		_scoreBoardService = scoreBoardService;
		_roundController = roundControllerBuilder.CreateRoundController(
			scoreEngineBuilder,
			editionConfig,
			deckServicesBuilder
		);
	}

	public void RunGame()
	{
		throw new NotImplementedException();
	}
}

using BoomerangGame.Core.Application;
using BoomerangGame.Core.Application.Builders;
using BoomerangGame.Core.Config;
using BoomerangGame.Core.Scoring.Builder;
using BoomerangGame.Core.UIs;
using BoomerangGame.Core.UIs.Builders;
using System.Net;
using System.Net.Sockets;

namespace BoomerangGame.Core.Network;

public class ServerApp
{
	private IGameServices _gameServices;
	private readonly IGameServiceBuilder _gameServiceBuilder;
	private readonly ILobbyServiceBuilder _lobbyServiceBuilder;
	private readonly IScoreEngineBuilder _scoreEngineBuilder;
	private readonly IRoundControllerBuilder _roundControllerBuilder;
	private readonly ITieBreakerBuilder _tieBreakerBuilder;
	private ILobbyService _lobbyService;
	private readonly IEditionLoader _editionLoader;
	private readonly IChannelBuilder _channelBuilder;
	private readonly IUiBuilder _uiBuilder;
	private readonly IUI _ui;
	private int _totalPlayers;
	private int _humanPlayers;

	public ServerApp(
		IGameServiceBuilder gameServiceBuilder,
		ILobbyServiceBuilder lobbyServiceBuilder,
		IScoreEngineBuilder scoreEngineBuilder,
		IRoundControllerBuilder roundControllerBuilder,
		ITieBreakerBuilder tieBreakerBuilder,
		IEditionLoader editionLoader,
		IChannelBuilder channelBuilder,
		IUiBuilder uiBuilder
	)
	{
		_editionLoader = editionLoader ?? throw new ArgumentNullException(nameof(editionLoader));
		_gameServiceBuilder = gameServiceBuilder;
		_lobbyServiceBuilder = lobbyServiceBuilder ?? throw new ArgumentNullException(nameof(lobbyServiceBuilder));
		_scoreEngineBuilder = scoreEngineBuilder;
		_roundControllerBuilder = roundControllerBuilder;
		_tieBreakerBuilder = tieBreakerBuilder;
		_channelBuilder = channelBuilder;
		_uiBuilder = uiBuilder;
		_totalPlayers = 0;
		_humanPlayers = 0;

		_ui = uiBuilder.GetUi();
	}

	/// <summary>
	/// Entry point for the server application.
	/// Initializes required resources and starts listening.
	/// </summary>
	public async Task Main()
	{
		StartServer();
		await SetupLobbyAndWaitForPlayers();
	}

	/// <summary>
	/// Starts the server and loads any necessary game edition data.
	/// </summary>
	public void StartServer()
	{
		var basePath = AppContext.BaseDirectory;
		var path = Path.Combine(basePath, "Configurations", "BoomerangConfig.json");
		var jsonData = _editionLoader.LoadEditionDto(path);
		
		var edition = _editionLoader.CreateDomain(jsonData);

		_gameServices = _gameServiceBuilder.CreateGameServices(
			edition,
			new ScoreBoardService(),
			_scoreEngineBuilder,
			_roundControllerBuilder,
			_tieBreakerBuilder
		);


		_lobbyService = _lobbyServiceBuilder.CreateLobbyService(edition.RegionMap);
		//_gameService.Initialize(edition);

		_ui.DisplayMessage("Server started and game initialized.");
	}

	public void SetPlayerCounts(int totalPlayers, int humanPlayers)
	{
		_totalPlayers = totalPlayers;
		_humanPlayers = humanPlayers;
	}


	public async Task SetupLobbyAndWaitForPlayers()
	{

		_lobbyService.AddPlayer(_channelBuilder.GetServerPlayerChannel());
		_ui.DisplayMessage($"Console player added!");

		int numberOfBots = AddBots();

		// Set up the socket for player connections
		TcpListener listener = new TcpListener(IPAddress.Any, 2048);
		listener.Start();
		
		_ui.DisplayMessage("Waiting for players to connect...");

		while (_lobbyService.GetPlayers().Count < _totalPlayers + numberOfBots)
		{
			var client = await listener.AcceptTcpClientAsync();
			var player = _lobbyService.AddPlayer(_channelBuilder.GetPlayerSessionChannel());

			_ui.DisplayMessage($"Remote player connected!");
		}

		listener.Stop();
		Console.WriteLine("All players connected. Game lobby ready.");
	}

	private int AddBots()
	{
		int numberOfBots = _totalPlayers - _humanPlayers;

		// Add bots to the lobby
		for (int i = 0; i < numberOfBots; i++)
			_lobbyService.AddPlayer(_channelBuilder.GetBotPlayerChannel());

		return numberOfBots;
	}
}

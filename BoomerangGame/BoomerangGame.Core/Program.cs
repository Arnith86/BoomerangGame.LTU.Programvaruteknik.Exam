using BoomerangGame.Core.Application.Builders;
using BoomerangGame.Core.Config;
using BoomerangGame.Core.Config.Factories.Decks;
using BoomerangGame.Core.Domain.States.MapStates.Builder;
using BoomerangGame.Core.Network;
using BoomerangGame.Core.Scoring;
using BoomerangGame.Core.UIs;
using BoomerangGame.Core.UIs.Builders;
using BoomerangGame.Core.UIs.CWritePrint;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;

namespace BoomerangGame.Core;

public class Program
{
	public static async Task Main(string[] args)
	{
		var services = new ServiceCollection();
		ConfigureServices(services);

		var serviceProvider = services.BuildServiceProvider();
		IUiBuilder uiBuilder = serviceProvider.GetService<IUiBuilder>()!;
		IUI ui = uiBuilder.GetUi();

		try
		{
			if (args.Length == 2)
			{
				// Server mode: <totalPlayers> <numberOfBots>
				int totalPlayers = int.Parse(args[0]);
				int numberOfBots = int.Parse(args[1]);

				if (!ValidatePlayerNumbers(totalPlayers, numberOfBots))
				{
					ui.DisplayMessage("Invalid player/bot numbers. Total players must be 2-4, bots cannot exceed total players.");
					return;
				}

				int humanPlayers = totalPlayers - numberOfBots;
				ui.DisplayMessage($"Starting server with {humanPlayers} human(s) and {numberOfBots} bot(s)...");

				var serverApp = serviceProvider.GetRequiredService<ServerApp>();
				serverApp.SetPlayerCounts(totalPlayers, humanPlayers); // pass counts
				await serverApp.Main();
			}
			else if (args.Length == 1)
			{
				// Client mode: <serverIP>
				string serverIp = args[0];
				await StartClient(serverIp, ui);
			}
			else
			{
				PrintUsage(ui);
			}
		}
		catch (Exception ex)
		{
			ui.DisplayMessage($"Error: {ex.Message}");
		}
	}

	private static async Task StartClient(string serverIp, IUI ui)
	{
		try
		{
			using TcpClient client = new TcpClient();
			await client.ConnectAsync(serverIp, 2048);

			using NetworkStream stream = client.GetStream();
			using var reader = new StreamReader(stream);
			using var writer = new StreamWriter(stream) { AutoFlush = true };

			string message = "";
			while (!message.Contains("winner"))
			{
				message = await reader.ReadLineAsync();
				ui.DisplayMessage(message);

				if (message.Contains("Type") || message.Contains("keep"))
				{
					string response = Console.ReadLine();
					await writer.WriteLineAsync(response);
				}
			}
		}
		catch (Exception ex)
		{
			ui.DisplayMessage($"Client error: {ex.Message}");
		}
	}

	private static bool ValidatePlayerNumbers(int totalPlayers, int numberOfBots)
	{
		return totalPlayers >= 2 
			&& totalPlayers <= 4 
			&& numberOfBots >= 0 
			&& numberOfBots <= totalPlayers;
	}


	private static void PrintUsage(IUI ui)
	{
		ui.DisplayMessage("Usage:");
		ui.DisplayMessage("Server: dotnet run <totalPlayers> <numberOfBots>");
		ui.DisplayMessage("Client: dotnet run <serverIP>");
	}

	private static void ConfigureServices(IServiceCollection services)
	{
		// Console UI / Port 
		services.AddSingleton<IConsoleWritePrint, ConsoleWritePrint>();
		services.AddSingleton<IChannelBuilder, ChannelBuilder>();
		services.AddSingleton<IUiBuilder, UiBuilder>();

		// Domain
		services.AddScoped<IMapStateBuilder, MapStateBuilder>();

		// Lobby
		services.AddSingleton<ILobbyServiceBuilder, LobbyServiceBuilder>();

		// Scoring
		services.AddSingleton<IRegionProgressTracker, RegionProgressTracker>();

		// Edition loader
		services.AddSingleton<IEditionLoader, EditionLoader>();
		services.AddSingleton<IDeckMapper, BoomerangDeckMapper>();
		services.AddSingleton<IDeckMapFunctions, DeckMapFunctions>();

		// Game server
		services.AddTransient<ServerApp>();
	}
}

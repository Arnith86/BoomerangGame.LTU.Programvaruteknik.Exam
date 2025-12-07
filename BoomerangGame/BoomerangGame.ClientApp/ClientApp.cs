using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Network;
using BoomerangGame.Core.Port;
using BoomerangGame.Core.UIs;
using BoomerangGame.Core.UIs.Builders;
using System.Net.Sockets;

namespace BoomerangGame.ClientApp;

public class ClientApp : IPlayerChannel
{

	private readonly IUI _ui;
	private readonly IChannelBuilder _channelBuilder;

	private readonly TcpClient _tcpClient = new();
	private NetworkStream? _stream;
	private StreamReader? _reader;
	private StreamWriter? _writer;

	public ClientApp(IUI ui, IChannelBuilder channelBuilder)
	{
		_ui = ui ?? throw new ArgumentNullException(nameof(ui));
		_channelBuilder = channelBuilder ?? throw new ArgumentNullException(nameof(channelBuilder));
	}

	public async Task ConnectAsync(string host, int port)
	{
		try
		{
			await _tcpClient.ConnectAsync(host, port);
			_stream = _tcpClient.GetStream();
			_reader = new StreamReader(_stream);
			_writer = new StreamWriter(_stream) { AutoFlush = true };

			_ui.DisplayMessage($"Connected to server at {host}:{port}");

			await ListenToServerAsync();
		}
		catch (Exception ex)
		{
			_ui.DisplayMessage($"Connection failed: {ex.Message}");
		}
	}

	private async Task ListenToServerAsync()
	{
		throw new NotImplementedException();
	}



	public async Task SendMessageAsync(string msg)
	{
		throw new NotImplementedException();
		//if (_writer != null)
		//	await _writer.WriteLineAsync(msg);
	}

	public async Task<string?> ReceiveMessageAsync()
	{
		throw new NotImplementedException();
		//return _reader != null ? await _reader.ReadLineAsync() : null;
	}

	public Task<IBoomerangCard<string>> PromptForCard(List<IBoomerangCard<string>> hand)
	{
		throw new NotImplementedException();
	}
}

using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.UIs;

namespace BoomerangGame.Core.Port;

/// <summary>
/// Represents a console-based player channel.Implements IPlayerChannel for local human player input/output.
/// </summary>
public class ConsolePlayerChannel : IPlayerChannel
{
	private readonly Queue<string> _inputQueue = new Queue<string>();
	private IUI _ui;

	public ConsolePlayerChannel(IUI ui)
	{
		_ui = ui;
	}

	/// <summary>Prompts the human player to select a card from their hand.</summary>
	/// <param name="hand">The list of cards available to pick from.</param>
	/// <returns>The selected card.</returns>
	public Task<IBoomerangCard<string>> PromptForCard(List<IBoomerangCard<string>> hand)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// Sends a message to the console player.
	/// </summary>
	/// <param name="msg">Message text</param>
	public async Task SendMessageAsync(string msg)
	{
		 _ui.DisplayMessage(msg);
	}

	/// <summary>
	/// Receives a message from the console player.
	/// </summary>
	/// <returns>The input string.</returns>
	public string ReceiveMessage()
	{
		throw new NotImplementedException();
	}

	Task IPlayerChannel.SendMessageAsync(string msg)
	{
		throw new NotImplementedException();
	}

	public Task<string?> ReceiveMessageAsync()
	{
		throw new NotImplementedException();
	}
}
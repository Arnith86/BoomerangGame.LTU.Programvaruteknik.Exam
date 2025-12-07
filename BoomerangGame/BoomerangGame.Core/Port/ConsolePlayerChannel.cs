using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.UIs;

namespace BoomerangGame.Core.Port;

/// <summary>
/// Represents a console-based player channel.Implements IPlayerChannel for local human player input/output.
/// </summary>
public class ConsolePlayerChannel : IPlayerChannel
{
	private readonly Queue<string> _inputQueue = new Queue<string>();

	public ConsolePlayerChannel(IUI uI)
	{
		
	}

	/// <summary>Prompts the human player to select a card from their hand.</summary>
	/// <param name="hand">The list of cards available to pick from.</param>
	/// <returns>The selected card.</returns>
	public IBoomerangCard<string> PromptForCard(List<IBoomerangCard<string>> hand)
	{
		if (hand == null || hand.Count == 0)
			throw new InvalidOperationException("Hand is empty. Cannot prompt for card.");

		Console.WriteLine("\nYour current hand:");
		for (int i = 0; i < hand.Count; i++)
		{
			var card = hand[i];
			Console.WriteLine($"{i + 1}: {card.Name} [{card.Site}] ({card.Number}) - {card.Region}");
		}

		int selection = -1;
		while (selection < 1 || selection > hand.Count)
		{
			Console.Write("Select a card by number: ");
			string input = Console.ReadLine();
			if (!int.TryParse(input, out selection) || selection < 1 || selection > hand.Count)
			{
				Console.WriteLine("Invalid selection, try again.");
			}
		}

		return hand[selection - 1];
	}

	/// <summary>
	/// Sends a message to the console player.
	/// </summary>
	/// <param name="msg">Message text</param>
	public void SendMessage(string msg)
	{
		Console.WriteLine(msg);
	}

	/// <summary>
	/// Receives a message from the console player.
	/// </summary>
	/// <returns>The input string.</returns>
	public string ReceiveMessage()
	{
		if (_inputQueue.Count > 0)
			return _inputQueue.Dequeue();

		string input = Console.ReadLine() ?? string.Empty;
		return input.Trim();
	}

	/// <summary>
	/// Optional helper to enqueue input programmatically (useful for testing).
	/// </summary>
	public void EnqueueInput(string input)
	{
		_inputQueue.Enqueue(input);
	}
}
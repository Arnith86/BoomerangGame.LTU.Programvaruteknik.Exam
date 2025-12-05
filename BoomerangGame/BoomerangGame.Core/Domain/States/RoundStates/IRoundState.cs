using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.TurnOrders;

namespace BoomerangGame.Core.Domain.States.RoundStates;

/// <summary>
/// Represents the state of a single round in the game, including the hands of each <br/>
/// player, cards thrown and caught, the draft sequence, and the direction of passing.
/// </summary>
public interface IRoundState
{
	int RoundNumber { get; }
	IReadOnlyDictionary<string, IBoomerangCard<string>> ThrowCards { get; }
	IReadOnlyDictionary<string, IBoomerangCard<string>> CatchCards { get; }
	IReadOnlyDictionary<string, List<IBoomerangCard<string>>> Hands { get; }
	IReadOnlyList<IDraftPick<string, IBoomerangCard<string>>> DraftSequence { get; }
	PassDirection PassDirection { get; }

	/// <summary>
	/// Adds a draft pick to the round sequence for a given player.
	/// </summary>
	/// <param name="playerName">The player making the draft pick.</param>
	/// <param name="card">The card being picked.</param>
	void AddDraftPick(string playerName, IBoomerangCard<string> card);

	/// <summary>
	/// Records a card that a player has either thrown or caught during the round.
	/// </summary>
	/// <param name="playerName">The player performing the action.</param>
	/// <param name="card">The card being thrown or caught.</param>
	/// <param name="cardIndex">
	/// The index indicating the type of action:
	/// i=0 for throw, i>0 for catch.
	/// </param>
	void RecordThrowOrCatchCard(string playerName, IBoomerangCard<string> card, int cardIndex);
}
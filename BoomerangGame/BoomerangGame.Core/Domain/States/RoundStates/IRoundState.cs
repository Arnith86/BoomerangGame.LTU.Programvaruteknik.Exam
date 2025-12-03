using BoomerangGame.Core.Application;
using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.TurnOrders;

namespace BoomerangGame.Core.Domain.States.RoundStates;

/// <summary>
/// Represents the state of a single round in the game, including the hands of each <br/>
/// player, cards thrown and caught, the draft sequence, and the direction of passing.
/// </summary>
public interface IRoundState
{
	Dictionary<IPlayer, IBoomerangCard> CatchCards { get; }
	List<IDraftPick<IPlayer, IBoomerangCard>> DraftSequence { get; }
	Dictionary<IPlayer, List<IBoomerangCard>> Hands { get; }
	PassDirection PassDirection { get; }
	Dictionary<IPlayer, IBoomerangCard> ThrowCards { get; }

	/// <summary>
	/// Adds a draft pick to the round sequence for a given player.
	/// </summary>
	/// <param name="player">The player making the draft pick.</param>
	/// <param name="card">The card being picked.</param>
	void AddDraftPick(IPlayer player, IBoomerangCard card);

	/// <summary>
	/// Records a card that a player has either thrown or caught during the round.
	/// </summary>
	/// <param name="player">The player performing the action.</param>
	/// <param name="card">The card being thrown or caught.</param>
	/// <param name="cardIndex">
	/// The index indicating the type of action:
	/// i=0 for throw, i>0 for catch.
	/// </param>
	void RecordThrowOrCatchCard(IPlayer player, IBoomerangCard card, int cardIndex);
}
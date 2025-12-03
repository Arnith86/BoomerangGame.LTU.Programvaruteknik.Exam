using BoomerangGame.Core.Application;
using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.TurnOrders;

namespace BoomerangGame.Core.Domain.States.RoundStates;

/// <summary>
/// Represents the state of a single round in the game, tracking players' hands, thrown <br/>
/// and caught cards, the draft sequence, and the direction of passing.
/// </summary>
public class RoundState : IRoundState
{
	private int _roundNumber = 0;

	private PassDirection _passDirection;

	private readonly Dictionary<string, IBoomerangCard> _throwCards = new();
	private readonly Dictionary<string, IBoomerangCard> _catchCards = new();
	private readonly Dictionary<string, List<IBoomerangCard>> _hands;
	private readonly List<IDraftPick<string, IBoomerangCard>> _draftSequence = new();

	/// <summary>
	/// Initializes a new instance of <see cref="RoundState"/>.
	/// </summary>
	/// <param name="roundNumber">The round number.</param>
	/// <param name="hands">The hands of each player. Cannot be null.</param>
	/// <param name="direction">The pass direction for the round, if none passed default of <see cref="PassDirection.CLOCKWISE"/> is chosen />.</param>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="hands"/> is <c>null<c>.</exception>
	public RoundState(
		int roundNumber,
		Dictionary<string, List<IBoomerangCard>> hands,
		PassDirection direction = PassDirection.CLOCKWISE)
	{
		_roundNumber = roundNumber;
		_hands = hands ?? throw new ArgumentNullException(nameof(hands), "Must have a value.");
		_passDirection = direction;
	}

	public Dictionary<string, IBoomerangCard> ThrowCards => _throwCards;
	public Dictionary<string, IBoomerangCard> CatchCards => _catchCards;
	public Dictionary<string, List<IBoomerangCard>> Hands => _hands;
	public List<IDraftPick<string, IBoomerangCard>> DraftSequence => _draftSequence;
	public PassDirection PassDirection => _passDirection;


	public void RecordThrowOrCatchCard(string playerName, IBoomerangCard card, int cardIndex)
	{
		if (!_hands.ContainsKey(playerName))
			throw new ArgumentException("Player not found in hands.");

		if (cardIndex == 0)
			_throwCards[playerName] = card;
		else
			_catchCards[playerName] = card;
	}


	public void AddDraftPick(string playerName, IBoomerangCard card)
	{
		_draftSequence.Add(DraftPickCreator.Create(playerName, card));
	}
}

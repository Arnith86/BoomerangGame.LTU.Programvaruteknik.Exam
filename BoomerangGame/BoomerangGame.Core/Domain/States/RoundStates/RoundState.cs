using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.TurnOrders;

namespace BoomerangGame.Core.Domain.States.RoundStates;

/// <summary>
/// Represents the state of a single round in the game, tracking players' hands, thrown <br/>
/// and caught cards, the draft sequence, and the direction of passing.
/// </summary>
public class RoundState : IRoundState
{
	public int RoundNumber { get; private set; }

	private PassDirection _passDirection;

	private readonly Dictionary<string, IBoomerangCard<string>> _throwCards = new();
	private readonly Dictionary<string, IBoomerangCard<string>> _catchCards = new();
	private readonly Dictionary<string, List<IBoomerangCard<string>>> _hands;
	private readonly List<IDraftPick<string, IBoomerangCard<string>>> _draftSequence = new();

	/// <summary>
	/// Initializes a new instance of <see cref="RoundState"/>.
	/// </summary>
	/// <param name="roundNumber">The round number.</param>
	/// <param name="hands">The hands of each player. Cannot be null.</param>
	/// <param name="direction">The pass direction for the round, if none passed default of <see cref="PassDirection.CLOCKWISE"/> is chosen />.</param>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="hands"/> is <c>null<c>.</exception>
	public RoundState(
		int roundNumber,
		Dictionary<string, List<IBoomerangCard<string>>> hands,
		PassDirection direction = PassDirection.CLOCKWISE)
	{
		if (roundNumber < 1 || roundNumber > 7)
			throw new ArgumentOutOfRangeException(nameof(roundNumber));

		RoundNumber = roundNumber;
		_hands = hands ?? throw new ArgumentNullException(nameof(hands), "Must have a value.");
		_passDirection = direction;
	}


	public IReadOnlyDictionary<string, IBoomerangCard<string>> ThrowCards => _throwCards;
	public IReadOnlyDictionary<string, IBoomerangCard<string>> CatchCards => _catchCards;
	public IReadOnlyDictionary<string, List<IBoomerangCard<string>>> Hands => _hands;
	public IReadOnlyList<IDraftPick<string, IBoomerangCard<string>>> DraftSequence => _draftSequence;
	public PassDirection PassDirection => _passDirection;


	public void RecordThrowOrCatchCard(string playerName, IBoomerangCard<string> card, int cardIndex)
	{
		if (!_hands.ContainsKey(playerName))
			throw new ArgumentException("Player not found in hands.");

		if (cardIndex == 0)
			_throwCards[playerName] = card;
		else
			_catchCards[playerName] = card;
	}


	public void AddDraftPick(string playerName, IBoomerangCard<string> card)
	{
		_draftSequence.Add(DraftPickCreator.Create(playerName, card));
	}
}

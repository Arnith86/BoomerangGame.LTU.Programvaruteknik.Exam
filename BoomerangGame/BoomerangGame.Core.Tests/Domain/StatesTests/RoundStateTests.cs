using BoomerangGame.Core.Application;
using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Domain.States.RoundStates;
using BoomerangGame.Core.Domain.TurnOrders;
using BoomerangGame.Core.Port;
using BoomerangGame.Core.Tests.HelperClasses;
using Moq;

namespace BoomerangGame.Tests.States.RoundStates;

public class RoundStateTests
{
	private MockPlayerCreator _mockPlayerCreator;
	private MockCardCreator _mockCardCreator;

	private readonly Mock<IPlayer> _mockPlayer;
	private readonly Mock<IBoomerangCard> _mockCard;

	public RoundStateTests()
	{
		_mockPlayerCreator = new MockPlayerCreator();
		_mockCardCreator = new MockCardCreator();
		_mockPlayer = _mockPlayerCreator.CreateSimpleMockPlayer();
		_mockCard = _mockCardCreator.CreateMockCardWithSetNumber(1);
	}

	private Dictionary<IPlayer, List<IBoomerangCard>> CreateHand()
	{
		return new Dictionary<IPlayer, List<IBoomerangCard>> {{
				_mockPlayer.Object,
				new List<IBoomerangCard>()
			}
		};
	}

	[Fact]
	public void Constructor_WithValidHands_SetsPassDirectionAndHands()
	{
		// Arrange
		var hands = CreateHand();
		
		var direction = PassDirection.CLOCKWISE;

		// Act
		var sut = new RoundState(1, hands, direction);

		// Assert
		Assert.Equal(direction, sut.PassDirection);
		Assert.Equal(hands, sut.Hands);
	}

	[Fact]
	public void Constructor_WithValidHands_NoPassDirectionChosen_ShouldReturnDefaultDirection()
	{
		// Arrange
		var hands = CreateHand();
		var direction = PassDirection.CLOCKWISE;

		// Act
		var sut = new RoundState(1, hands);

		// Assert
		Assert.Equal(direction, sut.PassDirection);
		Assert.Equal(hands, sut.Hands);
	}

	[Fact]
	public void Constructor_NullHands_ThrowsArgumentNullException()
	{
		// Act & Assert
		var ex = Assert.Throws<ArgumentNullException>(() => 
			new RoundState(1, null, PassDirection.CLOCKWISE)
		);
		
		Assert.Contains("hands", ex.ParamName);
	}

	[Fact]
	public void RecordThrowOrCatchCard_ThrowCard_SetsCardInThrowDictionary()
	{
		// Arrange
		var hands = CreateHand();
		var sut = new RoundState(1, hands, PassDirection.CLOCKWISE);

		// Act
		sut.RecordThrowOrCatchCard(_mockPlayer.Object, _mockCard.Object, 0);

		// Assert
		Assert.True(sut.ThrowCards.ContainsKey(_mockPlayer.Object));
		Assert.Equal(_mockCard.Object, sut.ThrowCards[_mockPlayer.Object]);
	}

	[Fact]
	public void RecordThrowOrCatchCard_CatchCard_SetsCardInCatchDictionary()
	{
		// Arrange
		var card = _mockCardCreator.CreateMockCardWithSetNumber(4);
		var hands = CreateHand();
		var sut = new RoundState(1, hands, PassDirection.CLOCKWISE);

		// Act
		sut.RecordThrowOrCatchCard(_mockPlayer.Object, card.Object, 1);

		// Assert
		Assert.True(sut.CatchCards.ContainsKey(_mockPlayer.Object));
		Assert.Equal(card.Object, sut.CatchCards[_mockPlayer.Object]);
	}

	[Fact]
	public void RecordThrowOrCatchCard_InvalidPlayer_ThrowsArgumentException()
	{
		// Arrange
		var card = _mockCardCreator.CreateMockCardWithSetNumber(4);
		var hands = new Dictionary<IPlayer, List<IBoomerangCard>>(); // Empty
		var sut = new RoundState(1, hands, PassDirection.CLOCKWISE);

		// Act & Assert
		var ex = Assert.Throws<ArgumentException>(() => 
			sut.RecordThrowOrCatchCard(_mockPlayer.Object, card.Object, 0)
		);

		Assert.Equal("Player not found in hands.", ex.Message);
	}

	[Fact]
	public void AddDraftPick_AddsDraftPickToSequence_SingleTuple()
	{
		// Arrange
		var hands = new Dictionary<IPlayer, List<IBoomerangCard>>();
		var sut = new RoundState(1, hands, PassDirection.CLOCKWISE);

		// Act
		sut.AddDraftPick(_mockPlayer.Object, _mockCard.Object);

		// Assert
		Assert.Single(sut.DraftSequence);
		var draftPick = sut.DraftSequence[0];
		Assert.Equal(_mockPlayer.Object, draftPick.Entity);
		Assert.Equal(_mockCard.Object, draftPick.Item);
	}

	[Fact]
	public void AddDraftPick_AddsDraftPickToSequence_TwoTuples()
	{
		// Arrange
		var hands = new Dictionary<IPlayer, List<IBoomerangCard>>();
		var sut = new RoundState(1, hands, PassDirection.CLOCKWISE);

		var player2 = _mockPlayerCreator.CreateSimpleMockPlayer().Object;
		var card2 = _mockCardCreator.CreateMockCardWithSetNumber(1).Object;

		// Act
		sut.AddDraftPick(_mockPlayer.Object, _mockCard.Object);
		sut.AddDraftPick(player2, card2);

		var draftPick = sut.DraftSequence[1];

		// Assert
		Assert.Equal(player2, draftPick.Entity);
		Assert.Equal(card2, draftPick.Item);
	}
}

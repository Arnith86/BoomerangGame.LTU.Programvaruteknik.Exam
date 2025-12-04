using BoomerangGame.Core.Application;
using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.States.RoundStates;
using BoomerangGame.Core.Domain.TurnOrders;
using BoomerangGame.Core.Tests.HelperClasses;
using Moq;

namespace BoomerangGame.Tests.States.RoundStates;

public class RoundStateTests
{
	private MockPlayerCreator _mockPlayerCreator;
	private MockCardCreator _mockCardCreator;

	private readonly Mock<IPlayer> _mockPlayer;
	private readonly Mock<IBoomerangCard<string>> _mockCard;
	private const string _c_PLAYER_NAME = "Player1";

	public RoundStateTests()
	{
		_mockPlayerCreator = new MockPlayerCreator();
		_mockCardCreator = new MockCardCreator();
		
		_mockPlayer = _mockPlayerCreator.CreateSimpleMockPlayer();
		_mockPlayer.SetupGet(p => p.PlayerState.Name).Returns(_c_PLAYER_NAME);

		_mockCard = _mockCardCreator.CreateMockCardWithSetNumber(1);
	}

	private Dictionary<string, List<IBoomerangCard<string>>> CreateHand()
	{
		return new Dictionary<string, List<IBoomerangCard<string>>> {{
				_mockPlayer.Name,
				new List<IBoomerangCard<string>>()
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
		sut.RecordThrowOrCatchCard(_mockPlayer.Name, _mockCard.Object, 0);

		// Assert
		Assert.True(sut.ThrowCards.ContainsKey(_mockPlayer.Name));
		Assert.Equal(_mockCard.Object, sut.ThrowCards[_mockPlayer.Name]);
	}

	[Fact]
	public void RecordThrowOrCatchCard_CatchCard_SetsCardInCatchDictionary()
	{
		// Arrange
		var card = _mockCardCreator.CreateMockCardWithSetNumber(4);
		var hands = CreateHand();
		var sut = new RoundState(1, hands, PassDirection.CLOCKWISE);

		// Act
		sut.RecordThrowOrCatchCard(_mockPlayer.Name, card.Object, 1);

		// Assert
		Assert.True(sut.CatchCards.ContainsKey(_mockPlayer.Name));
		Assert.Equal(card.Object, sut.CatchCards[_mockPlayer.Name]);
	}


	[Fact]
	public void AddDraftPick_AddsDraftPickToSequence_SingleTuple()
	{
		// Arrange
		var hands = new Dictionary<string, List<IBoomerangCard<string>>>();
		var sut = new RoundState(1, hands, PassDirection.CLOCKWISE);

		// Act
		sut.AddDraftPick(_mockPlayer.Name, _mockCard.Object);

		// Assert
		Assert.Single(sut.DraftSequence);
		var draftPick = sut.DraftSequence[0];
		Assert.Equal(_mockPlayer.Name, draftPick.Entity);
		Assert.Equal(_mockCard.Object, draftPick.Item);
	}

	[Fact]
	public void AddDraftPick_AddsDraftPickToSequence_TwoTuples()
	{
		// Arrange
		var hands = new Dictionary<string, List<IBoomerangCard<string>>>();
		var sut = new RoundState(1, hands, PassDirection.CLOCKWISE);

		Mock<IPlayer> player2 = _mockPlayerCreator.CreateSimpleMockPlayer();
		player2.SetupGet(p => p.PlayerState.Name).Returns("Player2");

		var card2 = _mockCardCreator.CreateMockCardWithSetNumber(1).Object;

		// Act
		sut.AddDraftPick(_mockPlayer.Name, _mockCard.Object);
		sut.AddDraftPick(player2.Name, card2);

		var draftPick = sut.DraftSequence[1];

		// Assert
		Assert.Equal(player2.Name, draftPick.Entity);
		Assert.Equal(card2, draftPick.Item);
	}
	
	
	
	[Fact]
	public void RecordThrowOrCatchCard_InvalidPlayer_ThrowsArgumentException()
	{
		// Arrange
		var card = _mockCardCreator.CreateMockCardWithSetNumber(4);
		var hands = new Dictionary<string, List<IBoomerangCard<string>>>(); // Empty
		var sut = new RoundState(1, hands, PassDirection.CLOCKWISE);

		// Act & Assert
		var ex = Assert.Throws<ArgumentException>(() =>
			sut.RecordThrowOrCatchCard(_mockPlayer.Name, card.Object, 0)
		);

		Assert.Equal("Player not found in hands.", ex.Message);
	}
}

using BoomerangGame.Core.Application;
using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Domain.States.RoundStates;
using BoomerangGame.Core.Domain.TurnOrders;
using BoomerangGame.Core.Port;
using Moq;

namespace BoomerangGame.Tests.States.RoundStates;

public class RoundStateTests
{
	private Mock<IPlayer> CreateMockPlayer()
	{
		var mockState = new Mock<IPlayerState>();
		var mockChannel = new Mock<IPlayerChannel>();

		var mockPlayer = new Mock<IPlayer>();
		mockPlayer.Setup(p => p.PlayerState).Returns(mockState.Object);
		mockPlayer.Setup(p => p.PlayerChannel).Returns(mockChannel.Object);

		return mockPlayer;
	}

	private Mock<IBoomerangCard> CreateMockCard()
	{
		return new Mock<IBoomerangCard>();
	}

	[Fact]
	public void Constructor_WithValidHands_SetsPassDirectionAndHands()
	{
		// Arrange
		var player = CreateMockPlayer().Object;
		var hands = new Dictionary<IPlayer, List<IBoomerangCard>> { { player, new List<IBoomerangCard>() } };
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
		var player = CreateMockPlayer().Object;
		var hands = new Dictionary<IPlayer, List<IBoomerangCard>> { { player, new List<IBoomerangCard>() } };
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
		var ex = Assert.Throws<ArgumentNullException>(() => new RoundState(1, null, PassDirection.CLOCKWISE));
		Assert.Contains("hands", ex.ParamName);
	}

	[Fact]
	public void RecordThrowOrCatchCard_ThrowCard_SetsCardInThrowDictionary()
	{
		// Arrange
		var player = CreateMockPlayer().Object;
		var card = CreateMockCard().Object;
		var hands = new Dictionary<IPlayer, List<IBoomerangCard>> { { player, new List<IBoomerangCard>() } };
		var roundState = new RoundState(1, hands, PassDirection.CLOCKWISE);

		// Act
		roundState.RecordThrowOrCatchCard(player, card, 0);

		// Assert
		Assert.True(roundState.ThrowCards.ContainsKey(player));
		Assert.Equal(card, roundState.ThrowCards[player]);
	}

	[Fact]
	public void RecordThrowOrCatchCard_CatchCard_SetsCardInCatchDictionary()
	{
		// Arrange
		var player = CreateMockPlayer().Object;
		var card = CreateMockCard().Object;
		var hands = new Dictionary<IPlayer, List<IBoomerangCard>> { { player, new List<IBoomerangCard>() } };
		var roundState = new RoundState(1, hands, PassDirection.CLOCKWISE);

		// Act
		roundState.RecordThrowOrCatchCard(player, card, 1);

		// Assert
		Assert.True(roundState.CatchCards.ContainsKey(player));
		Assert.Equal(card, roundState.CatchCards[player]);
	}

	[Fact]
	public void RecordThrowOrCatchCard_InvalidPlayer_ThrowsArgumentException()
	{
		// Arrange
		var player = CreateMockPlayer().Object;
		var card = CreateMockCard().Object;
		var hands = new Dictionary<IPlayer, List<IBoomerangCard>>(); // Empty
		var roundState = new RoundState(1, hands, PassDirection.CLOCKWISE);

		// Act & Assert
		var ex = Assert.Throws<ArgumentException>(() => roundState.RecordThrowOrCatchCard(player, card, 0));
		Assert.Equal("Player not found in hands.", ex.Message);
	}

	[Fact]
	public void AddDraftPick_AddsDraftPickToSequence_SingleTuple()
	{
		// Arrange
		var player = CreateMockPlayer().Object;
		var card = CreateMockCard().Object;
		var hands = new Dictionary<IPlayer, List<IBoomerangCard>>();
		var roundState = new RoundState(1, hands, PassDirection.CLOCKWISE);

		// Act
		roundState.AddDraftPick(player, card);

		// Assert
		Assert.Single(roundState.DraftSequence);
		var draftPick = roundState.DraftSequence[0];
		Assert.Equal(player, draftPick.Entity);
		Assert.Equal(card, draftPick.Item);
	}
	
	[Fact]
	public void AddDraftPick_AddsDraftPickToSequence_TwoTuples()
	{
		// Arrange
		var hands = new Dictionary<IPlayer, List<IBoomerangCard>>();
		var roundState = new RoundState(1, hands, PassDirection.CLOCKWISE);

		var player = CreateMockPlayer().Object;
		var card = CreateMockCard().Object;

		var player2 = CreateMockPlayer().Object;
		var card2 = CreateMockCard().Object;

		// Act
		roundState.AddDraftPick(player, card);
		roundState.AddDraftPick(player2, card2);

		var draftPick = roundState.DraftSequence[1];

		// Assert
		Assert.Equal(player2, draftPick.Entity);
		Assert.Equal(card2, draftPick.Item);
	}
}

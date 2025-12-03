namespace BoomerangGame.Core.Tests.Domain.ScoringStrategiesTests;

using BoomerangGame.Core.Application;
using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Domain.States.RoundStates;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using BoomerangGame.Core.Tests.HelperClasses;

public class ThrowCatchAbsoluteScoreTests
{
	private IRoundScoreCategory _sut;
	
	private MockPlayerCreator mockPlayerCreator = new MockPlayerCreator();
	private MockCardCreator mockCardCreator = new MockCardCreator();
	private Mock<IPlayer> _mockPlayer;
	private const string _c_PLAYER_NAME = "Player1";

	public ThrowCatchAbsoluteScoreTests()
	{
		_sut = new ThrowCatchAbsoluteScore();
		_mockPlayer = mockPlayerCreator.CreateSimpleMockPlayer();
		_mockPlayer.SetupGet(p => p.PlayerState.Name).Returns(_c_PLAYER_NAME);
	}


	private Mock<IRoundState> CreateMockRoundState(
		IPlayer player,
		IBoomerangCard? throwCard = null,
		IBoomerangCard? catchCard = null)
	{
		var mock = new Mock<IRoundState>();

		var throwDict = new Dictionary<string, IBoomerangCard>();
		var catchDict = new Dictionary<string, IBoomerangCard>();

		if (throwCard != null)
			throwDict[player.PlayerState.Name] = throwCard;

		if (catchCard != null)
			catchDict[player.PlayerState.Name] = catchCard;

		mock.Setup(r => r.ThrowCards).Returns(throwDict);
		mock.Setup(r => r.CatchCards).Returns(catchDict);

		return mock;
	}


	[Fact]
	public void CalculateScore_PlayerNull_ThrowsArgumentNullException()
	{
		// Arrange
		var roundState = new Mock<IRoundState>().Object;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			_sut.CalculateScore(null!, roundState)
		);
	}

	[Fact]
	public void CalculateScore_RoundStateNull_ThrowsArgumentNullException()
	{
		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			_sut.CalculateScore(_mockPlayer.Object.PlayerState, null!)
		);
	}

	[Fact]
	public void CalculateScore_PlayerMissingThrowCard_ThrowsInvalidOperationException()
	{
		// Arrange
		var catchCard = mockCardCreator.CreateMockCardWithSetNumber(5).Object;

		var roundState = CreateMockRoundState(
			_mockPlayer.Object,
			throwCard: null,
			catchCard: catchCard
		).Object;

		// Act & Assert
		Assert.Throws<InvalidOperationException>(() =>
			_sut.CalculateScore(_mockPlayer.Object.PlayerState, roundState));
	}

	[Fact]
	public void CalculateScore_PlayerMissingCatchCard_ThrowsInvalidOperationException()
	{
		// Arrange
		var throwCard = mockCardCreator.CreateMockCardWithSetNumber(3).Object;

		var roundState = CreateMockRoundState(
			_mockPlayer.Object,
			throwCard: throwCard,
			catchCard: null).Object;

		// Act & Assert
		Assert.Throws<InvalidOperationException>(() =>
			_sut.CalculateScore(_mockPlayer.Object.PlayerState, roundState));
	}

	[Theory]
	[InlineData(1, 7, 6)]
	[InlineData(7, 1, 6)]
	[InlineData(7, 7, 0)]
	[InlineData(0, 0, 0)]
	public void CalculateScore_ValidCards_ReturnsAbsoluteDifference(
		int throwNr,
		int catchNr,
		int absoluteExpected)
	{
		// Arrange
		var throwCard = mockCardCreator.CreateMockCardWithSetNumber(throwNr).Object;
		var catchCard = mockCardCreator.CreateMockCardWithSetNumber(catchNr).Object;

		var roundState = CreateMockRoundState(
			_mockPlayer.Object,
			throwCard: throwCard,
			catchCard: catchCard).Object;

		// Act
		int result = _sut.CalculateScore(_mockPlayer.Object.PlayerState, roundState);

		// Assert
		Assert.Equal(absoluteExpected, result);
	}
}

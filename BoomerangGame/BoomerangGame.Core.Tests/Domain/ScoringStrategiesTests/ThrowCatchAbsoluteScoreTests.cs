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
	private MockPlayerCreator mockPlayerCreator = new MockPlayerCreator();
	private MockCardCreator mockCardCreator = new MockCardCreator();
	private IScoringCategory _sut;

	public ThrowCatchAbsoluteScoreTests()
	{
		_sut = new ThrowCatchAbsoluteScore();
	}

	private Mock<IRoundState> CreateMockRoundState(
		IPlayer player,
		IBoomerangCard? throwCard = null,
		IBoomerangCard? catchCard = null)
	{
		var mock = new Mock<IRoundState>();

		var throwDict = new Dictionary<IPlayer, IBoomerangCard>();
		var catchDict = new Dictionary<IPlayer, IBoomerangCard>();

		if (throwCard != null)
			throwDict[player] = throwCard;

		if (catchCard != null)
			catchDict[player] = catchCard;

		mock.Setup(r => r.ThrowCards).Returns(throwDict);
		mock.Setup(r => r.CatchCards).Returns(catchDict);

		return mock;
	}

	// ------------------------------------------------------------
	// NUll TESTS
	// ------------------------------------------------------------

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
		// Arrange
		var player = mockPlayerCreator.CreateSimpleMockPlayer().Object;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			_sut.CalculateScore(player, null!)
		);
	}

	// ------------------------------------------------------------
	// MISSING CARD TESTS
	// ------------------------------------------------------------

	[Fact]
	public void CalculateScore_PlayerMissingThrowCard_ThrowsInvalidOperationException()
	{
		// Arrange
		var player = mockPlayerCreator.CreateSimpleMockPlayer().Object;
		var catchCard = mockCardCreator.CreateMockCardWithSetNumber(5).Object;

		var roundState = CreateMockRoundState(
			player,
			throwCard: null,
			catchCard: catchCard
		).Object;

		// Act & Assert
		Assert.Throws<InvalidOperationException>(() =>
			_sut.CalculateScore(player, roundState));
	}

	[Fact]
	public void CalculateScore_PlayerMissingCatchCard_ThrowsInvalidOperationException()
	{
		// Arrange
		var player = mockPlayerCreator.CreateSimpleMockPlayer().Object;
		var throwCard = mockCardCreator.CreateMockCardWithSetNumber(3).Object;

		var roundState = CreateMockRoundState(
			player,
			throwCard: throwCard,
			catchCard: null).Object;

		// Act & Assert
		Assert.Throws<InvalidOperationException>(() =>
			_sut.CalculateScore(player, roundState));
	}

	// ------------------------------------------------------------
	// VALID SCORE TEST
	// ------------------------------------------------------------

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
		var player = mockPlayerCreator.CreateSimpleMockPlayer().Object;
		var throwCard = mockCardCreator.CreateMockCardWithSetNumber(throwNr).Object;
		var catchCard = mockCardCreator.CreateMockCardWithSetNumber(catchNr).Object;

		var roundState = CreateMockRoundState(
			player,
			throwCard: throwCard,
			catchCard: catchCard).Object;

		// Act
		int result = _sut.CalculateScore(player, roundState);

		// Assert
		Assert.Equal(absoluteExpected, result); 
	}
}

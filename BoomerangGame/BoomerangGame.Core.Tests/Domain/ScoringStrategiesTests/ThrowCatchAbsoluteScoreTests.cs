using BoomerangGame.Core.Application;
using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Domain.States.RoundStates;
using Moq;
using BoomerangGame.Core.Tests.HelperClasses;
using BoomerangGame.Core.Domain.Cards;

namespace BoomerangGame.Core.Tests.Domain.ScoringStrategiesTests;

public class ThrowCatchAbsoluteScoreTests
{
	private IRoundScoreCategory _sut;

	private MockPlayerCreator _mockPlayerCreator;
	private MockCardCreator _mockCardCreator;
	private MockRoundStateCreator _mockRoundStateCreator;
	
	private Mock<IPlayer> _mockPlayer;
	private Mock<IRoundState> _mockRoundState;
	private const string _c_PLAYER_NAME = "Player1";

	public ThrowCatchAbsoluteScoreTests()
	{
		_sut = new ThrowCatchAbsoluteScore();
		_mockPlayerCreator = new MockPlayerCreator();
		_mockCardCreator = new MockCardCreator();
		_mockRoundStateCreator = new MockRoundStateCreator();

		_mockPlayer = _mockPlayerCreator.CreateSimpleMockPlayer();
		_mockPlayer.SetupGet(p => p.PlayerState.Name).Returns(_c_PLAYER_NAME);

		_mockRoundState = _mockRoundStateCreator.CreateMockRoundState(_mockPlayer.Object);
	}


	[Fact]
	public void CalculateScore_PlayerStateNull_ThrowsArgumentNullException()
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
		var catchCard = _mockCardCreator.CreateMockCardWithSetNumber(5).Object;

		var roundState = _mockRoundStateCreator.CreateMockRoundState(
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
		var throwCard = _mockCardCreator.CreateMockCardWithSetNumber(3).Object;

		var roundState = _mockRoundStateCreator.CreateMockRoundState(
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
		var throwCard = _mockCardCreator.CreateMockCardWithSetNumber(throwNr).Object;
		var catchCard = _mockCardCreator.CreateMockCardWithSetNumber(catchNr).Object;

		_mockRoundState.SetupGet(rs => rs.ThrowCards).Returns(new Dictionary<string, IBoomerangCard>
			{
				{ _c_PLAYER_NAME, throwCard }
			}
		);

		_mockRoundState.SetupGet(rs => rs.CatchCards).Returns(new Dictionary<string, IBoomerangCard>
			{
				{ _c_PLAYER_NAME, catchCard }
			}
		);

		// Act
		int result = _sut.CalculateScore(
			_mockPlayer.Object.PlayerState, 
			_mockRoundState.Object
		);

		// Assert
		Assert.Equal(absoluteExpected, result);
	}
}

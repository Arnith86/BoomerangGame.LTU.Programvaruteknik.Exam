// Ignore Spelling: RQ

using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Domain.States.RoundStates;
using BoomerangGame.Core.Scoring;
using Moq;

namespace BoomerangGame.Core.Tests.ScoringTests;

public class ScoreEngineTests
{
	private IRoundState _roundState = Mock.Of<IRoundState>();
	private ITieBreaker _ofTieBreaker = Mock.Of<ITieBreaker>();
	
	private Mock<ITieBreaker> _tieBreaker;
	private Mock<IRoundScoreCategory> _roundCategory;
	private Mock<IBlueIconScoreCategory> _blueIconCategory;
	private Mock<IScoreCategory> _basicCategory;

	public ScoreEngineTests()
	{
		_roundState = Mock.Of<IRoundState>();
		_ofTieBreaker = Mock.Of<ITieBreaker>();
		_tieBreaker = new Mock<ITieBreaker>();
		_roundCategory = new Mock<IRoundScoreCategory>();
		_blueIconCategory = new Mock<IBlueIconScoreCategory>();
		_basicCategory = new Mock<IScoreCategory>();
	}


	[Fact]
	public void CalculateRoundScore_UsesCorrectStrategies_RQ10()
	{
		// Arrange
		IBoomerangPlayerState playerState = Mock.Of<IBoomerangPlayerState>();
		_roundCategory
			.Setup(x => x.CalculateScore(playerState, _roundState))
			.Returns(5);

		_blueIconCategory
			.Setup(x => x.CalculateScore(playerState, "A"))
			.Returns(3);

		_basicCategory
			.Setup(x => x.CalculateScore(playerState))
			.Returns(2);

		IScoreEngine sut = new ScoreEngine(
			new[] { _roundCategory.Object, _blueIconCategory.Object, _basicCategory.Object },
			Mock.Of<IRegionProgressTracker>(),
			_ofTieBreaker,
			"dummy"
		);

		// Act
		var result = sut.CalculateRoundScore(playerState, _roundState, "A");

		// Assert
		Assert.Equal(10, result); 
	}


	[Fact]
	public void DecideWinner_ReturnsPlayerWithHighestScore_RQ10()
	{
		var p1 = new Mock<IPlayerState>();
		p1.SetupGet(x => x.Score).Returns(10);

		var p2 = new Mock<IPlayerState>();
		p2.SetupGet(x => x.Score).Returns(20);

		var sut = new ScoreEngine(
			new List<IScoreCategory>(),
			Mock.Of<IRegionProgressTracker>(),
			_ofTieBreaker,
			"tb"
		);

		// Act
		var result = sut.DecideWinner(new[] { p1.Object, p2.Object });

		// Assert
		Assert.Equal(p2.Object, result);
	}


	[Fact]
	public void DecideWinner_ResolvesTieUsingTieBreaker()
	{
		var p1 = new Mock<IPlayerState>();
		p1.SetupGet(x => x.Score).Returns(15);

		var p2 = new Mock<IPlayerState>();
		p2.SetupGet(x => x.Score).Returns(15);

		var p3 = new Mock<IPlayerState>();
		p3.SetupGet(x => x.Score).Returns(10);

		// TieBreaker returns only p2 as the final winner
		_tieBreaker
			.Setup(x => x.DecideTieWinner("tb", It.IsAny<List<IPlayerState>>()))
			.Returns(new List<IPlayerState> { p2.Object });

		var sut = new ScoreEngine(
			new List<IScoreCategory>(),
			Mock.Of<IRegionProgressTracker>(),
			_tieBreaker.Object,
			"tb"
		);

		// Act
		var winner = sut.DecideWinner(new[] { p1.Object, p2.Object, p3.Object });

		// Assert
		Assert.Equal(p2.Object, winner);
	}
}

using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Domain.States.MapStates;
using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Domain.States.RoundStates;
using Moq;

namespace BoomerangGame.Core.Tests.Domain.ScoringStrategiesTests;

public class RegionBonusPointScoreTests
{
	private readonly Mock<IRegionProgressTracker> _mockRegionTracker;
	private readonly Dictionary<string, int> _regionPoints;
	private readonly Mock<IBoomerangPlayerState> _playerState;
	private readonly Mock<IBoomerangPlayerState> _playerState2;
	private readonly Mock<IMapState> _mapStateMock;
	private readonly Mock<IRegionsState> _regionStateMock;

	private readonly RegionBonusPointScore _sut;
	
	public RegionBonusPointScoreTests()
	{
		_regionPoints = new Dictionary<string, int>
		{
			{ "RegionA", 3 },
			{ "RegionB", 5 }
		};
		
		_mockRegionTracker = new Mock<IRegionProgressTracker>();
		_playerState = new Mock<IBoomerangPlayerState>();
		_playerState2 = new Mock<IBoomerangPlayerState>();
		_mapStateMock = new Mock<IMapState>();
		_regionStateMock = new Mock<IRegionsState>();

		_sut = new RegionBonusPointScore(_mockRegionTracker.Object, _regionPoints);
	}

	private void SetupMapStateMock(string region, IRegionsState regionStateMock)
	{
		_mapStateMock.SetupGet(m => m.CurrentMapState)
		.Returns(new Dictionary<string, IRegionsState>
			{
					{ region, regionStateMock}
			}
		);
	}
	
	private void SetupMockRegionTracker(string region, int roundNumber, bool returnBool)
	{
		_mockRegionTracker.Setup(r => r.EligibleForRegionBonus(region, roundNumber))
			.Returns(returnBool);
	}

	private void SetupMockRegionState(bool returnBool)
	{
		_regionStateMock.Setup(r => r.IsComplete()).Returns(returnBool);
	}





	[Fact]
	public void CalculateScore_PlayerStateNull_ThrowsArgumentNullException()
	{   
		// Act & Assert 
		Assert.Throws<ArgumentNullException>(() 
			=> _sut.CalculateScore(null!, Mock.Of<IRoundState>()));
	}

	[Fact]
	public void CalculateScore_PlayerStateMapNull_ThrowsInvalidOperationException()
	{
		// Arrange
		_playerState.SetupGet(p => p.MapState).Returns((IMapState)null!);

		// Act & Assert 
		Assert.Throws<InvalidOperationException>(() =>
			_sut.CalculateScore(_playerState.Object, Mock.Of<IRoundState>())
		);
	}

	[Fact]
	public void CalculateScore_RoundStateNull_ThrowsNotSupportedException()
	{
		// Arrange
		_playerState.SetupGet(p => p.MapState).Returns(Mock.Of<IMapState>());

		// Act & Assert 
		Assert.Throws<NotSupportedException>(() 
			=> _sut.CalculateScore(_playerState.Object));
	}

	[Fact]
	public void CalculateScore_RegionStateNull_ThrowsInvalidOperationException()
	{
		// Arrange
		var mapStateMock = new Mock<IMapState>();
		mapStateMock.SetupGet(m => m.CurrentMapState)
			.Returns((Dictionary<string, IRegionsState>)null!);

		_playerState.SetupGet(p => p.MapState).Returns(mapStateMock.Object);

		// Act & Assert 
		Assert.Throws<InvalidOperationException>(() =>
			_sut.CalculateScore(_playerState.Object, Mock.Of<IRoundState>())
		);
	}
	

	[Fact]
	public void CalculateScore_RegionCompleteAndEligible_AddsPointsAndMarksRegion_RQ10b_i()
	{
		// Arrange
		var roundNumber = 1;

		SetupMockRegionState(true);
		SetupMapStateMock("RegionA", _regionStateMock.Object);
		
		_playerState.SetupGet(p => p.MapState).Returns(_mapStateMock.Object);

		SetupMockRegionTracker("RegionA", roundNumber, true);
		
		// Act
		int points = _sut.CalculateScore(
			_playerState.Object, 
			Mock.Of<IRoundState>(r => r.RoundNumber == roundNumber)
		);

		// Assert
		Assert.Equal(3, points);
		_mockRegionTracker.Verify(
			r => r.MarkRegionAsCompleted("RegionA", roundNumber), 
			Times.Once
		);
	}

	[Fact]
	public void CalculateScore_RegionComplete_MoreThanOneEligible_AddsPoints_RQ10b_ii()
	{
		// Arrange
		var roundNumber = 1;

		SetupMockRegionState(true);
		SetupMapStateMock("RegionA", _regionStateMock.Object);

		_playerState.SetupGet(p => p.MapState).Returns(_mapStateMock.Object);
		_playerState2.SetupGet(p => p.MapState).Returns(_mapStateMock.Object);


		SetupMockRegionTracker("RegionA", roundNumber, true);

		// Act
		int points = _sut.CalculateScore(
			_playerState.Object,
			Mock.Of<IRoundState>(r => r.RoundNumber == roundNumber)
		);

		int points2 = _sut.CalculateScore(
			_playerState2.Object,
			Mock.Of<IRoundState>(r => r.RoundNumber == roundNumber)
		);

		// Assert
		Assert.Equal(3, points);
		Assert.Equal(3, points2);
		_mockRegionTracker.Verify(
			r => r.MarkRegionAsCompleted("RegionA", roundNumber),
			Times.Exactly(2)
		);
	}

	[Fact]
	public void CalculateScore_RegionNotEligible_ReturnsZeroPoints_RQ10b_i()
	{
		// Arrange
		var roundNumber = 1;

		SetupMockRegionState(true);
		SetupMapStateMock("RegionB", _regionStateMock.Object);

		_playerState.SetupGet(p => p.MapState).Returns(_mapStateMock.Object);

		SetupMockRegionTracker("RegionA", roundNumber, true);

		// Act 
		int points = _sut.CalculateScore(
			_playerState.Object, 
			Mock.Of<IRoundState>(r => r.RoundNumber == roundNumber)
		);

		// Assert 
		Assert.Equal(0, points);
		_mockRegionTracker.Verify(r => r.MarkRegionAsCompleted(
			It.IsAny<string>(), 
			It.IsAny<int>()), 
			Times.Never
		);
	}
}

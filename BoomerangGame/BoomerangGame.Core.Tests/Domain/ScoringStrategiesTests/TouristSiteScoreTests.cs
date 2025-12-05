using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Domain.ScoringStrategies.Strategies;
using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Tests.HelperClasses;
using Moq;

namespace BoomerangGame.Core.Tests.Domain.ScoringStrategiesTests;

public class TouristSiteScoreTests
{
	private IScoreCategory _sut;

	private MockBoomerangPlayerStateCreator _mockBoomerangPlayerStateCreator;
	private Mock<IBoomerangPlayerState> _mockBPState;
	private const string _c_PLAYER_NAME = "TestPlayer";

	public TouristSiteScoreTests()
	{
		_sut = new TouristSiteScore();

		_mockBoomerangPlayerStateCreator = new MockBoomerangPlayerStateCreator();
		_mockBPState = _mockBoomerangPlayerStateCreator.CreateMockBoomerangPlayerState(_c_PLAYER_NAME);	
	}

	[Fact]
	public void CalculateScore_PlayerStateNull_ThrowsArgumentNullException_RQ10b()
	{
		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			_sut.CalculateScore(null!)
		);
	}

	[Fact]
	public void CalculateScore_VisitedSitesNull_ThrowsInvalidOperationException_RQ10b()
	{
		// Arrange
		_mockBPState.SetupGet(bPS => bPS.VisitedSites).Returns((HashSet<string>)null!);

		// Act & Assert
		Assert.Throws<InvalidOperationException>(() =>
			_sut.CalculateScore(_mockBPState.Object)
		);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(4)]
	[InlineData(7)]
	[InlineData(100)]
	public void CalculateScore_ValidVisitedSites_ReturnsCorrectScore_RQ10b(int nrVisitedSites)
	{
		// Arrange
		_mockBPState.SetupGet(bPS => bPS.VisitedSites).Returns(
			_mockBoomerangPlayerStateCreator.SetVisitedSites(nrVisitedSites)
		);

		// Act 
		Assert.Equal(
			nrVisitedSites,
			_sut.CalculateScore(_mockBPState.Object)
		);
	}
}

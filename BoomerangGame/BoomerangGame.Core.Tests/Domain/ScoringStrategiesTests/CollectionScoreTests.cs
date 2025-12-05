using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Tests.HelperClasses;
using Moq;

namespace BoomerangGame.Core.Tests.Domain.ScoringStrategiesTests;

public class CollectionScoreTests
{
	private IScoreCategory _sut;

	private MockBoomerangPlayerStateCreator _mockBoomerangPlayerStateCreator;
	private MockCardCreator _mockCardCreator;
	private Mock<IBoomerangPlayerState> _mockBPState;
	private const string _c_PLAYER_NAME = "TestPlayer";

	public CollectionScoreTests()
	{
		_sut = new CollectionScore();

		_mockBoomerangPlayerStateCreator = new MockBoomerangPlayerStateCreator();
		_mockBPState = _mockBoomerangPlayerStateCreator.CreateMockBoomerangPlayerState(_c_PLAYER_NAME);

		_mockCardCreator = new MockCardCreator();
	}

	[Fact]
	public void CalculateScore_PlayerStateNull_ThrowsArgumentNullException_RQ10c()
	{
		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			_sut.CalculateScore(null!)
		);
	}

	[Fact]
	public void CalculateScore_DraftedCardsNull_ThrowsInvalidOperationException_RQ10c()
	{
		// Arrange
		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns((List<IBoomerangCard<string>>?)null!);

		// Act & Assert
		Assert.Throws<InvalidOperationException>(() =>
			_sut.CalculateScore(_mockBPState.Object)
		);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_OnlyLeaves_ReturnsCorrectScore_RQ10c()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards = 
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"collection", "Leaves",
			"collection", "Leaves",
			"collection", "Leaves",
			"collection", "Leaves",
			"collection", "Leaves",
			"collection", "Leaves",
			"collection", "Leaves"
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(	mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(14, result);
	}
	
	[Fact]
	public void CalculateScore_ValidDraftedCards_OnlyShells_ReturnsCorrectScore_RQ10c()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards = 
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"collection", "Shells",
			"collection", "Shells",
			"collection", "Shells",
			"collection", "Shells",
			"collection", "Shells",
			"collection", "Shells",
			"collection", "Shells"
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(	mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(14, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_OnlyWildflowers_ReturnsCorrectScore_RQ10c()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards = 
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"collection", "Wildflowers",
			"collection", "Wildflowers",
			"collection", "Wildflowers",
			"collection", "Wildflowers",
			"collection", "Wildflowers",
			"collection", "Wildflowers",
			"collection", "Wildflowers"
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(	mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(21, result);
	}
	
	[Fact]
	public void CalculateScore_ValidDraftedCards_OnlySouvenirs_ReturnsCorrectScore_RQ10c()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards = 
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"collection", "Souvenirs",
			"collection", "Souvenirs",
			"collection", "Souvenirs",
			"collection", "Souvenirs",
			"collection", "Souvenirs",
			"collection", "Souvenirs",
			"collection", "Souvenirs"
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(	mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(35, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_8Mix_ReturnsCorrectScore_RQ10c()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"collection", "Leaves",
			"collection", "Shells",
			"collection", "Leaves",
			"collection", "Leaves",
			"collection", "Leaves",
			"collection", "Leaves",
			"collection", "Leaves"
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(8, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_Over8Mix_ReturnsCorrectScore_RQ10c()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"collection", "Leaves",
			"collection", "Shells",
			"collection", "Leaves",
			"collection", "Wildflowers",
			"collection", "Leaves",
			"collection", "Souvenirs",
			"collection", "Souvenirs"
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(18, result);
	}
}

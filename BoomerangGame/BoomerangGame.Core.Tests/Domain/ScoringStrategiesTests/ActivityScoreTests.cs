using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Tests.HelperClasses;
using Moq;

namespace BoomerangGame.Core.Tests.Domain.ScoringStrategiesTests;

public class ActivityScoreTests
{
	private IScoreCategory _sut;

	private MockBoomerangPlayerStateCreator _mockBoomerangPlayerStateCreator;
	private MockCardCreator _mockCardCreator;
	private Mock<IBoomerangPlayerState> _mockBPState;
	private const string _c_PLAYER_NAME = "TestPlayer";

	
	public ActivityScoreTests()
	{
		_mockBoomerangPlayerStateCreator = new MockBoomerangPlayerStateCreator();
		_mockBPState = _mockBoomerangPlayerStateCreator.CreateMockBoomerangPlayerState(_c_PLAYER_NAME);

		_mockCardCreator = new MockCardCreator();
	}

	[Fact]
	public void CalculateScore_PlayerStateNull_ThrowsArgumentNullException()
	{
		// Arrange 
		string category = "SomeCategory";
		_sut = new ActivityScore(category);

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			_sut.CalculateScore(null!)
		);
	}

	[Fact]
	public void CalculateScore_DraftedCardsNull_ThrowsInvalidOperationException()
	{
		// Arrange
		string category = "SomeCategory";
		_sut = new ActivityScore(category);
		
		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns((List<IBoomerangCard<string>>?)null!);
		

		// Act & Assert
		Assert.Throws<InvalidOperationException>(() =>
			_sut.CalculateScore(_mockBPState.Object)
		);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_OneInstance_ReturnsCorrectScore()
	{
		// Arrange
		string category = "SomeCategory";
		
		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeDifferentCategory"
		);
		
		_sut = new ActivityScore(category);
		
		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(0, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_TwoInstance_ReturnsCorrectScore()
	{
		// Arrange
		string category = "SomeCategory";

		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeCategory"
		);

		_sut = new ActivityScore(category);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(2, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_ThreeInstance_ReturnsCorrectScore()
	{
		// Arrange
		string category = "SomeCategory";

		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeCategory"
		);

		_sut = new ActivityScore(category);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(4, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_FourInstance_ReturnsCorrectScore()
	{
		// Arrange
		string category = "SomeCategory";

		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeCategory"
		);

		_sut = new ActivityScore(category);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(7, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_FiveInstance_ReturnsCorrectScore()
	{
		// Arrange
		string category = "SomeCategory";

		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeCategory"
		);

		_sut = new ActivityScore(category);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(10, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_SixInstance_ReturnsCorrectScore()
	{
		// Arrange
		string category = "SomeCategory";

		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeDifferentCategory",
			"blueIcon", "SomeCategory"
		);

		_sut = new ActivityScore(category);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(15, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_SevenInstance_ReturnsCorrectScore()
	{
		// Arrange
		string category = "SomeCategory";

		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeCategory",
			"blueIcon", "SomeCategory"
		);

		_sut = new ActivityScore(category);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(15, result);
	}
}

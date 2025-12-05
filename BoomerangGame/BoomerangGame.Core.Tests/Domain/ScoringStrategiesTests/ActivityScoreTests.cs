using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Domain.ScoringStrategies.Strategies;
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
	private string _category = "SomeCategory";
	private const string _c_PLAYER_NAME = "TestPlayer";
	private const string _c_STRATIGY_NAME = "ActivityScore";

	public ActivityScoreTests()
	{
		_mockBoomerangPlayerStateCreator = new MockBoomerangPlayerStateCreator();
		_mockBPState = _mockBoomerangPlayerStateCreator.CreateMockBoomerangPlayerState(_c_PLAYER_NAME);

		_mockCardCreator = new MockCardCreator();
	}


	[Fact]
	public void Constructor_NameNull_ThrowsArgumentNullException()
	{
		Assert.Throws<ArgumentNullException>(() =>
			new ThrowCatchAbsoluteScore(null!)
		);
	}

	[Fact]
	public void Name_ValidName_ShouldReturnName()
	{
		var sut = new ActivityScore(_category, _c_STRATIGY_NAME);
		Assert.Equal(_c_STRATIGY_NAME, sut.Name);
	}



	[Fact]
	public void CalculateScore_PlayerStateNull_ThrowsArgumentNullException_RQ10e()
	{
		// Arrange 
		_sut = new ActivityScore(_category, _c_STRATIGY_NAME);

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			_sut.CalculateScore(null!)
		);
	}

	[Fact]
	public void CalculateScore_DraftedCardsNull_ThrowsInvalidOperationException_RQ10e()
	{
		// Arrange
		_sut = new ActivityScore(_category, _c_STRATIGY_NAME);
		
		_mockBPState.SetupGet(bPS => bPS.DraftedCards)
			.Returns((List<IBoomerangCard<string>>?)null!);
		

		// Act & Assert
		Assert.Throws<InvalidOperationException>(() =>
			_sut.CalculateScore(_mockBPState.Object)
		);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_OneInstance_ReturnsCorrectScore_RQ10e()
	{
		// Arrange
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
		
		_sut = new ActivityScore(_category, _c_STRATIGY_NAME);
		
		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(0, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_TwoInstance_ReturnsCorrectScore_RQ10e()
	{
		// Arrange
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

		_sut = new ActivityScore(_category, _c_STRATIGY_NAME);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(2, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_ThreeInstance_ReturnsCorrectScore_RQ10e()
	{
		// Arrange
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

		_sut = new ActivityScore(_category, _c_STRATIGY_NAME);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(4, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_FourInstance_ReturnsCorrectScore_RQ10e()
	{
		// Arrange
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

		_sut = new ActivityScore(_category, _c_STRATIGY_NAME);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(7, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_FiveInstance_ReturnsCorrectScore_RQ10e()
	{
		// Arrange
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

		_sut = new ActivityScore(_category, _c_STRATIGY_NAME);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(10, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_SixInstance_ReturnsCorrectScore_RQ10e()
	{
		// Arrange
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

		_sut = new ActivityScore(_category, _c_STRATIGY_NAME);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(15, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_SevenInstance_ReturnsCorrectScore_RQ10e()
	{
		// Arrange
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

		_sut = new ActivityScore(_category, _c_STRATIGY_NAME);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(15, result);
	}
}

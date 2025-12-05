using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Domain.ScoringStrategies.Strategies;
using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Tests.HelperClasses;
using Moq;

namespace BoomerangGame.Core.Tests.Domain.ScoringStrategiesTests;

public class AnimalScoreTests
{
	private IScoreCategory _sut;

	private MockBoomerangPlayerStateCreator _mockBoomerangPlayerStateCreator;
	private MockCardCreator _mockCardCreator;
	private Mock<IBoomerangPlayerState> _mockBPState;
	private const string _c_PLAYER_NAME = "TestPlayer";
	private const string _c_STRATIGY_NAME = "AnimalScore";
	private Dictionary<string, int> _animals = new Dictionary<string, int>
	{
		{ "Kangaroos", 3 },
		{ "Emus", 4 },
		{ "Wombats", 5 },
		{ "Koalas", 7 },
		{ "Platypuses", 9 },
		{ "Mouse", 1 },
		{ "Fish", 2 }
	};

	public AnimalScoreTests()
	{
		_sut = new AnimalScore(_animals, _c_STRATIGY_NAME);

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
		Assert.Equal(_c_STRATIGY_NAME, _sut.Name);
	}


	[Fact]
	public void CalculateScore_PlayerStateNull_ThrowsArgumentNullException_RQ10d()
	{
		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			_sut.CalculateScore(null!)
		);
	}

	[Fact]
	public void CalculateScore_DraftedCardsNull_ThrowsInvalidOperationException_RQ10d()
	{
		// Arrange
		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns((List<IBoomerangCard<string>>?)null!);

		// Act & Assert
		Assert.Throws<InvalidOperationException>(() =>
			_sut.CalculateScore(_mockBPState.Object)
		);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_OnlyKangaroos_ReturnsCorrectScore_RQ10d()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"animal", "Kangaroos",
			"animal", "Kangaroos",
			"animal", "Kangaroos",
			"animal", "Kangaroos",
			"animal", "Kangaroos",
			"animal", "Kangaroos",
			"animal", "Kangaroos"
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(9, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_OnlyEmus_ReturnsCorrectScore_RQ10d()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"animal", "Emus",
			"animal", "Emus",
			"animal", "Emus",
			"animal", "Emus",
			"animal", "Emus",
			"animal", "Emus",
			"animal", "Emus"
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(12, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_OnlyWombats_ReturnsCorrectScore_RQ10d()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"animal", "Wombats",
			"animal", "Wombats",
			"animal", "Wombats",
			"animal", "Wombats",
			"animal", "Wombats",
			"animal", "Wombats",
			"animal", "Wombats"
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(15, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_OnlyKoalas_ReturnsCorrectScore_RQ10d()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"animal", "Koalas",
			"animal", "Koalas",
			"animal", "Koalas",
			"animal", "Koalas",
			"animal", "Koalas",
			"animal", "Koalas",
			"animal", "Koalas"
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(21, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_OnlyPlatypuses_ReturnsCorrectScore_RQ10d()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"animal", "Platypuses",
			"animal", "Platypuses",
			"animal", "Platypuses",
			"animal", "Platypuses",
			"animal", "Platypuses",
			"animal", "Platypuses",
			"animal", "Platypuses"
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(27, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_MixedPairs_ReturnsCorrectScore_RQ10d()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"animal", "Platypuses",
			"animal", "Emus",
			"animal", "Kangaroos",
			"animal", "Platypuses",
			"animal", "Emus",
			"animal", "Kangaroos",
			"animal", "Koalas "
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(16, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_TwoPairs_ReturnsCorrectScore_RQ10d()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"animal", "Platypuses",
			"animal", "Emus",
			"animal", "Kangaroos",
			"animal", "Platypuses",
			"animal", "Wombats",
			"animal", "Kangaroos",
			"animal", "Koalas "
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(12, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_OnePairs_ReturnsCorrectScore_RQ10d()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"animal", "Platypuses",
			"animal", "Emus",
			"animal", "Fish",
			"animal", "Platypuses",
			"animal", "Wombats",
			"animal", "Kangaroos",
			"animal", "Koalas "
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(9, result);
	}

	[Fact]
	public void CalculateScore_ValidDraftedCards_NoPairs_ReturnsCorrectScore_RQ10d()
	{
		// Arrange
		List<IBoomerangCard<string>> mockDraftedCards =
			_mockCardCreator.CreateFullSetOfMockCardWithCollectionSymbol(
			"animal", "Mouse",
			"animal", "Emus",
			"animal", "Fish",
			"animal", "Platypuses",
			"animal", "Wombats",
			"animal", "Kangaroos",
			"animal", "Koalas "
		);

		_mockBPState.SetupGet(bPS => bPS.DraftedCards).Returns(mockDraftedCards);

		// Act
		int result = _sut.CalculateScore(_mockBPState.Object);

		// Assert
		Assert.Equal(0, result);
	}
}

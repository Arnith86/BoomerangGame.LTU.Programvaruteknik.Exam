using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.Cards.Symbols;
using BoomerangGame.Core.Domain.States.MapStates;
using BoomerangGame.Core.Domain.States.PlayerState;
using Moq;

namespace BoomerangGame.Core.Tests.Domain.StatesTests;

public class PlayerStateTests
{
	private readonly PlayerState _sut;
	private readonly Mock<IMapState> _mockMapState;
	private readonly Mock<IBoomerangCard<string>> _cardA;
	private readonly Mock<IBoomerangCard<string>> _cardB;

	public PlayerStateTests()
	{
		_mockMapState = new Mock<IMapState>();
		_sut = new PlayerState(_mockMapState!.Object);


		_cardA = new Mock<IBoomerangCard<string>>();
		_cardA.SetupGet(c => c.Site).Returns("A");
		_cardA.SetupGet(c => c.Region).Returns("Western Australia");

		_cardB = new Mock<IBoomerangCard<string>>();
		_cardB.SetupGet(c => c.Site).Returns("B");
		_cardB.SetupGet(c => c.Region).Returns("Western Australia");
	}

	[Fact]
	public void AddToDraftedCards_ShouldAddCardToDraftedCards()
	{
		// Act
		_sut.AddToDraftedCards(_cardA.Object);

		// Assert
		Assert.Contains(_cardA.Object, _sut.DraftedCards);
		Assert.Contains("A", _sut.VisitedSites);
		_mockMapState.Verify(m => m.UpdateMapState("Western Australia", "A"), Times.Once);
	}

	[Fact]
	public void AddToDraftedCards_FirstCard_ThrowCard_ShouldHaveState_Hidden_RQ5()
	{
		// Arrange
		Mock<ISymbolSet<string>> symbolSet = new Mock<ISymbolSet<string>>();

		// Act
		_sut.AddToDraftedCards(new BoomerangCard<string>(
			name: "The Bungle Bungles",
			region: "Western Australia",
			site: "A",
			number: 1,
			symbols: symbolSet.Object
		));
		
		// Assert 
		Assert.True(_sut.DraftedCards[0].IsHidden);
	}


	// Only first card toggled
	[Fact]
	public void AddToDraftedCards_SecondCard_ShouldNotToggleHidden()
	{
		_sut.AddToDraftedCards(_cardA.Object);
		_sut.AddToDraftedCards(_cardB.Object);

		_cardB.Verify(c => c.ToggleIsHidden(), Times.Never); 
	}

	[Fact]
	public void AddToDraftedCards_NullCard_ShouldThrow()
	{
		Assert.Throws<ArgumentNullException>(() => _sut.AddToDraftedCards(null!));
	}

	[Fact]
	public void AddToScore_PositivePoints_ShouldIncreaseScore()
	{
		_sut.AddToScore(5);
		_sut.AddToScore(10);

		Assert.Equal(15, _sut.Score);
	}

	[Fact]
	public void AddToScore_NegativePoints_ShouldThrow()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => _sut.AddToScore(-1));
	}

	[Fact]
	public void UpdateBlueIconHistory_ValidString_ShouldAddToHistory()
	{
		_sut.UpdateBlueIconHistory("Kangaroo");

		Assert.Contains("Kangaroo", _sut.BlueIconHistory);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData("   ")]
	public void UpdateBlueIconHistory_InvalidString_ShouldThrow(string input)
	{
		Assert.Throws<ArgumentException>(() => _sut.UpdateBlueIconHistory(input));
	}

	[Fact]
	public void DraftedCards_VisitedSites_BlueIconHistory_InitialState()
	{
		Assert.Empty(_sut.DraftedCards);
		Assert.Empty(_sut.VisitedSites);
		Assert.Empty(_sut.BlueIconHistory);
	}

	[Fact]
	public void Constructor_ShouldThrowArgumentNullException_WhenMapStateIsNull()
	{
		// Arrange
		IMapState? nullMapState = null;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
		{
			var sut = new PlayerState(nullMapState!);
		});
	}

	[Fact]
	public void ResetDraftHand_DraftedCards_ShouldBeEmptyAfterReset_RQ11()
	{
		// Arrange
		Mock<ISymbolSet<string>> symbolSet = new Mock<ISymbolSet<string>>();
		
		_sut.AddToDraftedCards(new BoomerangCard<string>(
			name: "The Bungle Bungles",
			region: "Western Australia",
			site: "A",
			number: 1,
			symbols: symbolSet.Object
		));

		// Act
		_sut.ResetDraftHand();

		// Assert 
		Assert.True(_sut.DraftedCards.Count == 0);
	}
}

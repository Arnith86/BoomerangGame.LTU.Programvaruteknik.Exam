using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.Cards.Symbols;

namespace BoomerangGame.Core.Tests.Domain.CardTests;

public class BoomerangCardTests
{
	private const string ValidName = "The Bungle Bungles";
	private const string ValidRegion = "Western Australia";
	private const string ValidSite = "A";
	private const int ValidCardNumber = 1;

	private readonly SymbolSet _validSymbolSet;
	private readonly BoomerangCard _sampleCard;

	public BoomerangCardTests()
	{
		_validSymbolSet = SymbolSetFactory.FromSymbols(
			new Symbol("Collection", "Leaves"),
			new Symbol("blueIcon", "Indigenous Culture")
		);

		_sampleCard = new BoomerangCard(
			name: ValidName,
			region: ValidRegion,
			site: ValidSite,
			number: ValidCardNumber,
			symbolSet: _validSymbolSet
		);
	}

	[Fact]
	public void Constructor_Should_Set_All_Properties()
	{
		Assert.Equal(ValidName, _sampleCard.Name);
		Assert.Equal(ValidSite, _sampleCard.Site);
		Assert.Equal(ValidRegion, _sampleCard.Region);
		Assert.Equal(ValidCardNumber, _sampleCard.Number);
		Assert.NotNull(_sampleCard.Symbols);
	}

	[Fact]
	public void GetSymbolByCategory_Should_Return_Correct_Symbol()
	{
		var first = _sampleCard.Symbols.First;
		var second = _sampleCard.Symbols.Second;
		var third = _sampleCard.Symbols.Third;

		Assert.NotNull(first);
		Assert.Equal("Leaves", first!.Value);

		Assert.NotNull(second);
		Assert.Equal("Indigenous Culture", second!.Value);

		Assert.Null(third);
	}

	[Fact]
	public void ToggleIsHidden_Should_Toggle_IsHidden_Property()
	{
		// Initial state should be false
		Assert.False(_sampleCard.IsHidden);
		// Toggle once, should be true
		_sampleCard.ToggleIsHidden();
		Assert.True(_sampleCard.IsHidden);
		// Toggle again, should be false
		_sampleCard.ToggleIsHidden();
		Assert.False(_sampleCard.IsHidden);
	}
}


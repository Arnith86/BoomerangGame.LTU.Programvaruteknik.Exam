using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.Cards.Symbols;

namespace BoomerangGame.Core.Tests.Domain.CardTests;

public class BoomerangCardTests
{
	private const string _c_ValidName = "The Bungle Bungles";
	private const string _c_ValidRegion = "Western Australia";
	private const string _c_ValidSite = "A";
	private const int _c_ValidCardNumberValue = 1;

	private SymbolSet CreateValidSymbolSet() => SymbolSetFactory.FromSymbols(
		new Symbol("Collection", "Leaves"), 
		new Symbol("Activity", "Indigenous Culture")
	);

	private BoomerangCard CreateSampleCard()
	{	
		return new BoomerangCard(
			name: _c_ValidName,
			region: _c_ValidRegion,
			site: _c_ValidSite,
			number:  _c_ValidCardNumberValue,
			symbolSet: CreateValidSymbolSet()
		);
	}

	[Fact]
	public void Constructor_Should_Set_All_Properties()
	{
		// Arrange & Act
		var card = CreateSampleCard();

		// Assert
		card.Name.Should().Be(_c_ValidName);
		card.Site.Should().Be(_c_ValidSite);
		card.Region.Should().Be(_c_ValidRegion);
		card.Number.Should().Be(_c_ValidCardNumberValue);
		card.Symbols.Should().NotBeNull();
	}

	[Fact]
	public void GetSymbolByCategory_Should_Return_Correct_Symbol()
	{
		// Arrange
		var card = CreateSampleCard();

		// Act
		var first = card.Symbols.First;
		var second = card.Symbols.Second;
		var third = card.Symbols.Third;

		// Assert
		first.Should().NotBeNull();
		first!.Value.Should().Be("Leaves");

		second.Should().NotBeNull();
		second!.Value.Should().Be("Indigenous Culture");

		third.Should().BeNull();
	}
}

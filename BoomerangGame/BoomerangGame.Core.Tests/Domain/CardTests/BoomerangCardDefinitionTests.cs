using BoomerangGame.Core.Config.Factories;
using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.Cards.Symbols;

namespace BoomerangGame.Core.Tests.Domain.CardTests;


public class BoomerangCardDefinitionTests
{
	private const string _c_ValidName = "The Bungle Bungles";
	private const string _c_ValidRegion = "Western Australia";
	private const string _c_ValidSite = "A";
	private const int _c_ValidCardNumberValue = 1;

	private SymbolSet<string> CreateValidSymbolSet() => SymbolSetFactory<string>.FromSymbols(
		new Symbol<string>("Collection", "Leaves"),
		new Symbol<string>("Animal", ""),
		new Symbol<string>("blueIcon", "Indigenous Culture")
	);

	[Fact]
	public void Constructor_ShouldCreateValidCardDefinition()
	{
		var symbols = CreateValidSymbolSet();

		var card = new BoomerangCardDefinition<string>(
			_c_ValidName, _c_ValidRegion, _c_ValidSite, _c_ValidCardNumberValue, symbols);

		Assert.Equal(_c_ValidName, card.Name);
		Assert.Equal(_c_ValidSite, card.Site);
		Assert.Equal(_c_ValidRegion, card.Region);
		Assert.Equal(_c_ValidCardNumberValue, card.Number);
		Assert.Equal(symbols, card.Symbols);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData("   ")]
	public void Constructor_ShouldThrow_WhenRegionInvalid(string? region)
	{
		var symbols = CreateValidSymbolSet();

		Assert.Throws<ArgumentException>(() =>
			new BoomerangCardDefinition<string>(_c_ValidName, region!, _c_ValidSite, _c_ValidCardNumberValue, symbols));
	}

	[Fact]
	public void Constructor_ShouldTrimRegion()
	{
		var symbols = CreateValidSymbolSet();

		var card = new BoomerangCardDefinition<string>(
			_c_ValidName, 
			"  Western Australia   ", 
			_c_ValidSite, 
			_c_ValidCardNumberValue, 
			symbols
		);

		Assert.Equal(_c_ValidRegion, card.Region);
	}
	
	[Fact]
	public void Constructor_ShouldTrimName()
	{
		var symbols = CreateValidSymbolSet();

		var card = new BoomerangCardDefinition<string>(
			"  The Bungle Bungles   ", 
			_c_ValidRegion, 
			_c_ValidSite, 
			_c_ValidCardNumberValue, 
			symbols
		);

		Assert.Equal(_c_ValidName, card.Name);
	}

	[Fact]
	public void Constructor_ShouldThrow_WhenNumNegative()
	{
		var symbols = CreateValidSymbolSet();

		Assert.Throws<ArgumentOutOfRangeException>(() =>
			new BoomerangCardDefinition<string>(
				_c_ValidName, 
				_c_ValidRegion, 
				_c_ValidSite, 
				-1, 
				symbols
			)
		);
	}

	[Fact]
	public void Constructor_ShouldThrow_WhenSymbolsNull()
	{
		Assert.Throws<ArgumentNullException>(() =>
			new BoomerangCardDefinition<string>(
				_c_ValidName, 
				_c_ValidRegion, 
				_c_ValidSite, 
				_c_ValidCardNumberValue, 
				null!
			)
		);
	}
}


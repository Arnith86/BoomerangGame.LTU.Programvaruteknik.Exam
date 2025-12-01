using BoomerangGame.Core.Domain.Cards.Symbols;

namespace BoomerangGame.Core.Tests.Domain.CardTests;

public class SymbolSetFactoryTests
{
	[Fact]
	public void FromDictionary_ShouldIgnoreNullValuesAndRespectOrder()
	{
		// Arrange
		var source = new Dictionary<string, string?>
		{
			{ "collection", "Shells" },
			{ "animal", null },
			{ "blueIcon", "Swimming" }
		};

		// Act
		var symbolSet = SymbolSetFactory.FromDictionary(source);

		// Assert
		symbolSet.Symbols.Should().ContainInOrder(
			new Symbol("collection", "Shells"),
			new Symbol("blueIcon", "Swimming"));
	}

	[Fact]
	public void FromSymbols_ShouldLimitToThreeEntries()
	{
		// Arrange
		var symbols = new[]
		{
			new Symbol("collection", "Shells"),
			new Symbol("animal", "Kangaroo"),
			new Symbol("bonus", "Extra") // should be ignored
		};

		// Act
		var symbolSet = SymbolSetFactory.FromSymbols(symbols);

		// Assert
		symbolSet.Symbols.Should().HaveCount(2);
		symbolSet.Symbols.Should().NotContain(symbols[2]);
	}
}


using BoomerangGame.Core.Config.Factories.Symbols;
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
		var symbolSet = SymbolSetFactory<string>.FromDictionary(source);

		// Assert
		symbolSet.Symbols.Should().ContainInOrder(
			new Symbol<string>("collection", "Shells"),
			new Symbol<string>("blueIcon", "Swimming"));
	}

	[Fact]
	public void FromSymbols_ShouldLimitToThreeEntries()
	{
		// Arrange
		var symbols = new[]
		{
			new Symbol<string>("collection", "Shells"),
			new Symbol<string>("animal", "Kangaroo"),
			new Symbol<string>("bonus", "Extra") // should be ignored
		};

		// Act
		var symbolSet = SymbolSetFactory<string>.FromSymbols(symbols);

		// Assert
		symbolSet.Symbols.Should().HaveCount(2);
		symbolSet.Symbols.Should().NotContain(symbols[2]);
	}
}


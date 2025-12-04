using BoomerangGame.Core.Domain.Cards.Symbols;

namespace BoomerangGame.Core.Tests.Domain.CardTests;

public class SymbolSetTests
{
	[Fact]
	public void Constructor_ShouldStoreAllSymbols()
	{
		// Arrange
		var expectedFirst = new Symbol<string>("collection", "Shells");
		var expectedSecond = new Symbol<string>("animal", "Kangaroo");
		var expectedThird = new Symbol<string>("blueIcon", "Swimming");

		// Act
		var symbolSet = new SymbolSet<string>(expectedFirst, expectedSecond, expectedThird);

		// Assert
		symbolSet.First.Should().Be(expectedFirst);
		symbolSet.Second.Should().Be(expectedSecond);
		symbolSet.Third.Should().Be(expectedThird);
		symbolSet.Symbols.Should().ContainInOrder(expectedFirst, expectedSecond, expectedThird);
	}

	[Fact]
	public void ContainsCategory_WhenSymbolExists_ShouldReturnTrue()
	{
		// Arrange
		var symbolSet = new SymbolSet<string>(
			new Symbol<string>("collection", "Shells"),
			new Symbol<string>("animal", "Kangaroo"),
			null);

		// Act & Assert
		symbolSet.ContainsCategory("animal").Should().BeTrue();
	}

	[Fact]
	public void ContainsCategory_WhenSymbolIsMissing_ShouldReturnFalse()
	{
		// Arrange
		var symbolSet = new SymbolSet<string>(
			new Symbol<string>("collection", "Shells"),
			new Symbol<string>("animal", "Kangaroo"),
			null);

		// Act & Assert
		symbolSet.ContainsCategory("blueIcon").Should().BeFalse();
	}
}

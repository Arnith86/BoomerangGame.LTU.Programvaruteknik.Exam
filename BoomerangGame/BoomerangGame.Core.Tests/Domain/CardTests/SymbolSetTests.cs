using BoomerangGame.Core.Domain.Cards;

namespace BoomerangGame.Core.Tests.Domain.CardTests;

public class SymbolSetTests
{
	[Fact]
	public void Constructor_ShouldStoreAllSymbols()
	{
		// Arrange
		var expectedFirst = new Symbol("collection", "Shells");
		var expectedSecond = new Symbol("animal", "Kangaroo");
		var expectedThird = new Symbol("activity", "Swimming");

		// Act
		var symbolSet = new SymbolSet(expectedFirst, expectedSecond, expectedThird);

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
		var symbolSet = new SymbolSet(
			new Symbol("collection", "Shells"),
			new Symbol("animal", "Kangaroo"),
			null);

		// Act & Assert
		symbolSet.ContainsCategory("animal").Should().BeTrue();
	}

	[Fact]
	public void ContainsCategory_WhenSymbolIsMissing_ShouldReturnFalse()
	{
		// Arrange
		var symbolSet = new SymbolSet(
			new Symbol("collection", "Shells"),
			new Symbol("animal", "Kangaroo"),
			null);

		// Act & Assert
		symbolSet.ContainsCategory("activity").Should().BeFalse();
	}
}

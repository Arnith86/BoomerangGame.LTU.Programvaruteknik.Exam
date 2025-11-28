namespace BoomerangGame.Core.Domain.Cards.Symbols;

/// <summary>
/// Represents a single symbol printed on a card together with its category.
/// </summary>
/// <param name="Category">The category identifier (e.g. collection, animal, activity).</param>
/// <param name="Value">The concrete value within that category (e.g. shells, kangaroo).</param>
public record Symbol(string Category, string Value)
{
	public string Category { get; init; } = Category ?? throw new ArgumentNullException(nameof(Category));
	public string Value { get; init; } = Value ?? throw new ArgumentNullException(nameof(Value));
}


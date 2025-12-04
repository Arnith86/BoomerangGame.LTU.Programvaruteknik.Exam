namespace BoomerangGame.Core.Domain.Cards.Symbols;

/// <summary>
/// Represents up to three symbols printed on a Boomerang card.
/// </summary>
/// <param name="First">The first symbol, if any.</param>
/// <param name="Second">The second symbol, if any.</param>
/// <param name="Third">The third symbol, if any.</param>
public record SymbolSet<TValue>(
	Symbol<TValue>? First, 
	Symbol<TValue>? Second, 
	Symbol<TValue>? Third) : ISymbolSet<TValue>
{
	private static readonly Symbol<TValue>[] Empty = [];

	/// <summary>
	/// All non-null symbols in the order they were added.
	/// </summary>
	public IReadOnlyList<Symbol<TValue>> Symbols =>
		new[] { First, Second, Third }.Where(s => s is not null)
			.Cast<Symbol<TValue>>().ToArray();

	/// <summary>
	/// Returns true if the set contains at least one symbol with the supplied category.
	/// </summary>
	public bool ContainsCategory(string category) =>
		!string.IsNullOrWhiteSpace(category) &&
		Symbols.Any(symbol => symbol.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
}

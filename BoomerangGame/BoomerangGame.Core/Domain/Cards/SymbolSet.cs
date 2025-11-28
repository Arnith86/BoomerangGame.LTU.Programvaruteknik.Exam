namespace BoomerangGame.Core.Domain.Cards;

/// <summary>
/// Represents up to three symbols printed on a Boomerang card.
/// </summary>
/// <param name="First">The first symbol, if any.</param>
/// <param name="Second">The second symbol, if any.</param>
/// <param name="Third">The third symbol, if any.</param>
public record SymbolSet(Symbol? First, Symbol? Second, Symbol? Third)
{
	private static readonly Symbol[] Empty = [];

	/// <summary>
	/// All non-null symbols in the order they were added.
	/// </summary>
	public IReadOnlyList<Symbol> Symbols =>
		new[] { First, Second, Third }.Where(s => s is not null).Cast<Symbol>().ToArray();

	/// <summary>
	/// Returns true if the set contains at least one symbol with the supplied category.
	/// </summary>
	public bool ContainsCategory(string category) =>
		!string.IsNullOrWhiteSpace(category) &&
		Symbols.Any(symbol => symbol.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

	/// <summary>
	/// Returns the first symbol that belongs to the supplied category, if any.
	/// </summary>
	public Symbol? GetSymbolByCategory(string category) =>
		string.IsNullOrWhiteSpace(category)
			? null
			: Symbols.FirstOrDefault(symbol =>
				symbol.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

	/// <summary>
	/// True when no symbols are stored.
	/// </summary>
	public bool IsEmpty => Symbols.Count == 0;
}

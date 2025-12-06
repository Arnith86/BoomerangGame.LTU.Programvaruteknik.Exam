namespace BoomerangGame.Core.Domain.Cards.Symbols;

/// <summary>
/// Represents up to three Symbols printed on a Boomerang card.
/// </summary>
/// <param name="First">The first symbol, if any.</param>
/// <param name="Second">The second symbol, if any.</param>
/// <param name="Third">The third symbol, if any.</param>
public record SymbolSet<TValue>(
	Symbol<TValue>? First, 
	Symbol<TValue>? Second, 
	Symbol<TValue>? Third) 
	: ISymbolSet<TValue>
{
	private static readonly Symbol<TValue>[] Empty = [];

	/// <inheritdoc/>
	public IReadOnlyList<Symbol<TValue>> Symbols =>
		new[] { First, Second, Third }.Where(s => s is not null)
			.Cast<Symbol<TValue>>().ToArray();

	/// <inheritdoc/>
	public bool ContainsCategory(string category) =>
		!string.IsNullOrWhiteSpace(category) &&
		Symbols.Any(symbol => symbol.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

	/// <inheritdoc/>
	public Symbol<TValue>? GetByCategory(string category)
	{
		if (string.IsNullOrWhiteSpace(category))
			return null;

		return Symbols.FirstOrDefault(
			s => s.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
	}
}

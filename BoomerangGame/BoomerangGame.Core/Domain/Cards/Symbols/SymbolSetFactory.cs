namespace BoomerangGame.Core.Domain.Cards.Symbols;

/// <summary>
/// Constructs <see cref="SymbolSet"/> instances from a variety of source shapes.
/// Keeps the public API small and centralizes validation rules.
/// </summary>
public static class SymbolSetFactory
{
	/// <summary>
	/// Creates a symbol set from category/value pairs (ignoring null or whitespace values).
	/// </summary>
	public static SymbolSet FromDictionary(IEnumerable<KeyValuePair<string, string?>> categoryValues)
	{
		if (categoryValues is null)
		{
			throw new ArgumentNullException(nameof(categoryValues));
		}

		var symbols = categoryValues
			.Where(entry => !string.IsNullOrWhiteSpace(entry.Key) && !string.IsNullOrWhiteSpace(entry.Value))
			.Select(entry => new Symbol(entry.Key.Trim(), entry.Value!.Trim()))
			.ToList();

		return FromSymbols(symbols);
	}

	/// <summary>
	/// Creates a symbol set from an enumerable of <see cref="Symbol" /> instances.
	/// </summary>
	public static SymbolSet FromSymbols(IEnumerable<Symbol?> symbols)
	{
		if (symbols is null)
		{
			throw new ArgumentNullException(nameof(symbols));
		}

		var symbolArray = symbols
			.Where(symbol => symbol is not null)
			.Cast<Symbol>()
			.Take(3)
			.ToArray();

		return new SymbolSet(
			symbolArray.ElementAtOrDefault(0),
			symbolArray.ElementAtOrDefault(1),
			symbolArray.ElementAtOrDefault(2));
	}

	/// <summary>
	/// Convenience factory for callers that have between zero and three symbols already constructed.
	/// </summary>
	public static SymbolSet FromSymbols(params Symbol?[] symbols) => FromSymbols((IEnumerable<Symbol?>)symbols);
}


using BoomerangGame.Core.Domain.Cards.Symbols;

namespace BoomerangGame.Core.Config.Factories;

/// <summary>
/// Constructs <see cref="SymbolSet"/> instances from a variety of source shapes.
/// Keeps the public API small and centralizes validation rules.
/// </summary>
public static class SymbolSetFactory<TValue>
{
	/// <summary>
	/// Creates a symbol set from category/value pairs (ignoring null or whitespace values).
	/// </summary>
	public static SymbolSet<TValue>FromDictionary(
		IEnumerable<KeyValuePair<string, TValue?>> categoryValues
	)
	{
		if (categoryValues is null)
		{
			throw new ArgumentNullException(nameof(categoryValues));
		}

		var symbols = categoryValues
			.Where(entry =>
				!string.IsNullOrWhiteSpace(entry.Key) &&
				entry.Value is not null)
			.Select(entry =>
				new Symbol<TValue>(entry.Key.Trim(), entry.Value!))
			.ToList();

		return FromSymbols(symbols);
	}

	/// <summary>
	/// Creates a symbol set from an enumerable of <see cref="Symbol" /> instances.
	/// </summary>
	public static SymbolSet<TValue> FromSymbols(IEnumerable<Symbol<TValue>> symbols)
	{
		if (symbols is null)
		{
			throw new ArgumentNullException(nameof(symbols));
		}

		var symbolArray = symbols
			.Where(symbol => symbol is not null)
			.Cast<Symbol<TValue>>()
			.Take(2)
			.ToArray();

		return new SymbolSet<TValue>(
			symbolArray.ElementAtOrDefault(0),
			symbolArray.ElementAtOrDefault(1),
			symbolArray.ElementAtOrDefault(2));
	}

	/// <summary>
	/// Convenience factory for callers that have between zero and three Symbols already constructed.
	/// </summary>
	public static SymbolSet<TValue> FromSymbols(params Symbol<TValue>[] symbols) 
		=> FromSymbols((IEnumerable<Symbol<TValue>>)symbols);
}


namespace BoomerangGame.Core.Domain.Cards.Symbols;

/// <summary>Represents a collection of up to three symbols printed on a Boomerang card.</summary>
/// <typeparam name="TValue">The type of the value associated with each symbol (e.g., string, int).</typeparam>
/// <remarks>The third symbol is not used on the final card, only the definition.</remarks>
public interface ISymbolSet<TValue>
{
	Symbol<TValue>? First { get; init; }
	Symbol<TValue>? Second { get; init; }
	Symbol<TValue>? Third { get; init; }
	
	/// <summary>
	/// Gets all non-null symbols contained in the set, in the order they appear <br/>
	/// (First → Second → Third).
	/// </summary>
	IReadOnlyList<Symbol<TValue>> Symbols { get; }


	/// <summary>
	/// Determines whether the symbol set contains at least one symbol whose category <br/>
	/// matches the specified category string.
	/// </summary>
	/// <param name="category"> The category name to check for.</param>
	/// <returns><c>true</c> if a symbol with the given category exists; otherwise, <c>false</c></returns>
	bool ContainsCategory(string category);

	/// <summary>Retrieves the first symbol whose category matches the supplied category.</summary>
	/// <param name="category">The category name to look up. </param>
	/// <returns>The matching <see cref="Symbol{TValue}"/> if found; otherwise, <c>null</c>.</returns>
	Symbol<TValue>? GetByCategory(string category);
	
	string ToString();
}
using BoomerangGame.Core.Domain.Cards;

namespace BoomerangGame.Core.Domain.ScoringStrategies.Utilities;

/// <summary>
/// Provides helper functionality for counting how many times specific symbol values <br/>
/// appear across a collection of Boomerang cards.
/// </summary>
public static class SymbolInstanceCounter
{
	/// <summary>
	/// Counts the number of occurrences of symbol values for a given category across <br/>
	/// all cards in the provided collection.
	/// </summary>
	/// <param name="cards">The list of <see cref="IBoomerangCard{TValue}"/> instances to analyze.</param>
	/// <param name="type">
	/// The symbol category to search for. Only symbols whose category matches this value will be counted.
	/// </param>
	/// <returns>
	/// A dictionary where each key represents a symbol value (e.g. <c>"Leaves"</c>), and the <br/>
	/// corresponding value represents the number of times that symbol appears across the card set.
	/// </returns>
	/// <exception cref="ArgumentNullException">
	/// Thrown when <paramref name="cards"/> is <c>null</c>.
	/// </exception>
	public static Dictionary<string, int> CountInstances(
		List<IBoomerangCard<string>> cards, 
		string type
	)
	{
		if (cards is null)
			throw new ArgumentNullException(nameof(cards));

		var result = new Dictionary<string, int>();

		foreach (var card in cards)
		{
			if (card.Symbols is null) continue;

			var symbol = card.Symbols.GetByCategory(type);

			string key = symbol!.Value;

			if (result.ContainsKey(key))
				result[key]++;
			else
				result[key] = 1;
		}

		return result;
	}
}

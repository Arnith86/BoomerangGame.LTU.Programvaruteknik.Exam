using BoomerangGame.Core.Domain.Cards.Symbols;
using System;

namespace BoomerangGame.Core.Domain.Cards;

/// <summary>
/// Represents a single card in the game with a site, region, number, and a set of symbols.
/// </summary>
public sealed class BoomerangCardDefinition
{
	public string Name { get; }
	public string Region { get; }
	public string Site { get; }
	public int Number { get; }
	public SymbolSet Symbols { get; }

	/// <summary>
	/// Constructs a new CardDefinition instance.
	/// </summary>
	/// <param name="name">The name of tourist site (non-null, trimmed).</param>
	/// <param name="Site">The letter identifier of the card (e.g., "A", "B").</param>
	/// <param name="region">The region code (non-null, trimmed).</param>
	/// <param name="number">The numeric value of the card (non-negative).</param>
	/// <param name="symbols">The set of symbols associated with the card (non-null).</param>
	public BoomerangCardDefinition(string name, string region, string site, int number, SymbolSet symbols)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Name cannot be null or empty.", nameof(name));
		
		if (string.IsNullOrWhiteSpace(region))
			throw new ArgumentException("Region cannot be null or empty.", nameof(region));

		if (number < 0)
			throw new ArgumentOutOfRangeException(nameof(number), "Number cannot be negative.");

		Name = name.Trim();
		Region = region.Trim();
		Site = site ?? throw new ArgumentNullException("Site cannot be null or empty.", nameof(site)); 
		Number = number;
		Symbols = symbols ?? throw new ArgumentNullException("Region cannot be null or empty.", nameof(region));
	}
}
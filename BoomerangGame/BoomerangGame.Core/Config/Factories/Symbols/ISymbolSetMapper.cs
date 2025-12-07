using BoomerangGame.Core.Domain.Cards.Symbols;

namespace BoomerangGame.Core.Config.Factories.Symbols;

/// <summary>
/// Defines a strategy for converting a loosely typed symbol payload into a
/// strongly typed <see cref="SymbolSet{TValue}"/> instance.
/// </summary>
public interface ISymbolSetMapper
{
	/// <summary>
	/// Converts the raw symbol DTO into a <see cref="SymbolSet{TValue}"/>
	/// containing the symbols used by a Boomerang card.
	/// </summary>
	/// <returns>
	/// A fully constructed <see cref="SymbolSet{TValue}"/> representing the
	/// normalized symbol data for the card.
	/// </returns>
	/// <exception cref="ArgumentException">
	/// Thrown when the DTO shape does not match the expected format for the
	/// mapper's edition or cannot be converted to the required structure.
	/// </exception>
	SymbolSet<string> MapSymbols(object symbolDto);
}

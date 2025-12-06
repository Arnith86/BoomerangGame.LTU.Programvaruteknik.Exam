
using BoomerangGame.Core.Domain.Cards.Symbols;

namespace BoomerangGame.Core.Config.Factories;

/// <summary>
/// Provides factory methods for selecting an <see cref="ISymbolSetMapper"/> implementation <br/>
/// based on the active Boomerang game edition.
/// </summary>
/// <remarks>
/// New editions can be supported by implementing <see cref="ISymbolSetMapper"/> and adding a <br/>
/// new case to the <see cref="GetMapper(string)"/> method.
/// </remarks>
public static class SymbolSetMapperFactory
{
	/// <summary>
	/// Retrieves the <see cref="ISymbolSetMapper"/> used to interpret symbol JSON data <br/>
	/// for the specified edition of the Boomerang game.
	/// </summary>
	/// <param name="editionName">The name of the game edition.</param>
	/// <returns>
	/// An instance of <see cref="ISymbolSetMapper"/> capable of converting <br/>
	/// the edition-specific JSON symbol data into a normalized <see cref="SymbolSet{TValue}"/>.
	/// </returns>
	/// <exception cref="ArgumentException">Thrown when no mapper is registered for the supplied <paramref name="editionName"/>.</exception>
	public static ISymbolSetMapper GetMapper(string editionName)
	{
		return editionName switch
		{
			"Boomerang Australia" => new AustraliaSymbolSetMapper(),
			_ => throw new ArgumentException($"No mapper found for edition {editionName}")
		};
	}
}

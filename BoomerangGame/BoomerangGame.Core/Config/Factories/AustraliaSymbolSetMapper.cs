// Ignore Spelling: Dto

using BoomerangGame.Core.Domain.Cards.Symbols;
using System.Text.Json;

namespace BoomerangGame.Core.Config.Factories;
/// <summary>
/// Maps raw JSON-deserialized symbol data into a <see cref="SymbolSet{TValue}"/> for the <br/>
/// Australia edition of the Boomerang game.
/// </summary>
/// <remarks>
/// To ensure consistent handling regardless of how the data was parsed, the
/// implementation re-serializes the DTO back to JSON and deserializes it into a
/// strongly typed <see cref="Dictionary{TKey, TValue}"/>. This normalizes the
/// structure and guarantees predictable input for <see cref="SymbolSetFactory{TValue}.FromDictionary"/>.
/// </remarks>
public class AustraliaSymbolSetMapper : ISymbolSetMapper
{
	/// <summary>
	/// Converts a raw symbol DTO into a normalized <see cref="SymbolSet{TValue}"/> containing the <br/>
	/// symbol data for an Australian Boomerang card.
	/// </summary>
	/// <param name="symbolDto">
	/// The raw symbol data as deserialized from JSON. The runtime type may vary <br/>
	/// depending on the JSON source (e.g. <see cref="JsonElement"/>).
	/// </param>
	/// <returns>A <see cref="SymbolSet{TValue}"/> containing the validated symbolinformation for the card.</returns>
	/// <exception cref="ArgumentException">Thrown when the DTO cannot be converted into a valid dictionary structure.</exception>
	public SymbolSet<string> MapSymbols(object symbolDto)
	{
		// Normalize the input by re-serializing it to known JSON
		var json = JsonSerializer.Serialize(symbolDto);
		// Deserialize the JSON into the canonical dictionary format
		var dict = JsonSerializer.Deserialize<Dictionary<string, string?>>(json);

		if (dict is null) 
			throw new ArgumentException("Invalid symbols data");

		return SymbolSetFactory<string>.FromDictionary(dict);
	}
}

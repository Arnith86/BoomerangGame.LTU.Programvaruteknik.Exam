using BoomerangGame.Core.Domain.Cards.Symbols;

namespace BoomerangGame.Core.Domain.Cards
{
	/// <summary>
	/// Represents the basic structure of a Card.
	/// </summary>
	public interface ICard
	{
		/// <summary>
		/// The numeric value of the card.
		/// </summary>
		int Number { get; }

		/// <summary>
		/// The symbols of the card.
		/// </summary>
		SymbolSet Symbols { get; }
	}
}

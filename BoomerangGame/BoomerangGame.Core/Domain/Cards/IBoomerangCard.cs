using BoomerangGame.Core.Domain.Cards.Symbols;

namespace BoomerangGame.Core.Domain.Cards
{
	/// <summary>
	/// Represents a Boomerang game card with specific properties like name, letter, and region.
	/// </summary>
	public interface IBoomerangCard<TValue> : ICard
	{
		/// <summary>
		/// The name of the card.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The letter identifier of the card.
		/// </summary>
		string Site { get; }

		/// <summary>
		/// The region associated with the card.
		/// </summary>
		string Region { get; }

		/// <summary>
		/// The symbols of the card.
		/// </summary>
		ISymbolSet<TValue> Symbols { get; }
	}
}
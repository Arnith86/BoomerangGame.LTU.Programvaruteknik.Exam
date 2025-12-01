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
		/// Indicates whether the card is currently hidden for other players.
		/// </summary>
		bool IsHidden { get; }

		/// <summary>
		/// Toggles the hidden state of the card.
		/// </summary>
		void ToggleIsHidden();
	}
}

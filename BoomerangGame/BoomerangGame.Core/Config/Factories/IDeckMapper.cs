namespace BoomerangGame.Core.Config.Factories;

/// <summary>
/// Provides functionality to map a list of input card data to a deck of output cards.
/// This interface focuses on converting data from one card representation to another.
/// </summary>
public interface IDeckMapper
{
	/// <summary>
	/// Maps a list of input cards to a deck of output cards using the specified converter function.
	/// </summary>
	/// <typeparam name="TIn">The type of the input card data.</typeparam>
	/// <typeparam name="TOut">The type of the output card.</typeparam>
	/// <param name="inputCards">The input cards to convert.</param>
	/// <param name="converter">A function that converts an input card into an output card.</param>
	/// <returns>An <see cref="IEnumerable{TOut}"/> containing the converted cards.</returns>
	/// <exception cref="ArgumentNullException">
	/// Thrown if <paramref name="inputCards"/> or <paramref name="converter"/> is null.
	/// </exception>

	IEnumerable<TOut> MapDeck<TIn, TOut>(List<TIn> inputCards, Func<TIn, TOut> converter);
}

using BoomerangGame.Core.Application;

namespace BoomerangGame.Core.Domain.TurnOrders;

/// <summary>
/// Defines a strategy for determining turn order in the Boomerang game.
/// </summary>
public interface ITurnOrder
{
	/// <summary>
	/// Determines the next player based on the current turn order strategy.
	/// </summary>
	/// <param name="players">The ordered list of all players in the game.</param>
	/// <param name="currentPlayer">The player whose turn has just completed.</param>
	/// <returns>The next player who should take a turn.</returns>
	IPlayer GetNextPlayer(List<IPlayer> players, IPlayer currentPlayer);

	/// <summary>
	/// Gets the direction in which turns should be passed <br/>
	/// (e.g., clockwise or counterclockwise).
	/// </summary>
	/// <returns>A <see cref="PassDirection"/> value representing the turn direction.</returns>
	PassDirection GetPassDirection();
}

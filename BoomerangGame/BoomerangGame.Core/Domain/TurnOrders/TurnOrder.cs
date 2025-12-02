using BoomerangGame.Core.Application;

namespace BoomerangGame.Core.Domain.TurnOrders;

/// <summary>
/// Represents a turn-order handler that determines which player acts next <br />
/// based on a configured <see cref="PassDirection"/>.
/// </summary>
public class TurnOrder : ITurnOrder
{
	private readonly PassDirection _passDirection;

	/// <summary>
	/// Initializes a new instance of the <see cref="TurnOrder"/> class.
	/// </summary>
	/// <param name="passDirection">
	/// The direction in which player turns should advance.
	/// </param>
	public TurnOrder(PassDirection passDirection)
	{
		_passDirection = passDirection;
	}

	/// <inheritdoc/>
	public IPlayer GetNextPlayer(List<IPlayer> players, IPlayer currentPlayer)
	{
		int index = players.IndexOf(currentPlayer);
		int nextIndex = (index + (int)_passDirection);

		if ( nextIndex >= players.Count)
		{
			nextIndex = 0;
		} 
		else if ( nextIndex < 0)
		{
			nextIndex = players.Count - 1;
		}

		return players[nextIndex];
	}

	/// <inheritdoc/>
	public PassDirection GetPassDirection() => _passDirection;
}

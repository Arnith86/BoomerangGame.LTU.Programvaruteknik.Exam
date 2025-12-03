using BoomerangGame.Core.Application;
using BoomerangGame.Core.Domain.States.RoundStates;

namespace BoomerangGame.Core.Domain.ScoringStrategies;

/// <summary>
/// Represents a scoring category where a player's score is determined by the absolute <br/>
/// difference between their thrown boomerang card and the card they catch.
/// </summary>
public class ThrowCatchAbsoluteScore : IScoringCategory
{
	/// <summary>
	/// Calculates the player's score using the absolute difference between the value of their <br/>
	/// thrown card and the card they caught.
	/// </summary>
	/// <param name="player">The player for whom the score is being calculated.</param>
	/// <param name="roundState">The state of the current round, containing card assignments.</param>
	/// <returns>
	/// The absolute difference between the throw card's value and the catch card's value.
	/// </returns>
	/// <exception cref="ArgumentNullException">
	/// Thrown if <paramref name="player"/> or <paramref name="roundState"/> is null.
	/// </exception>
	/// <exception cref="InvalidOperationException">
	/// Thrown if the player does not have both a throw card and a catch card recorded.
	/// </exception>
	public int CalculateScore(IPlayer player, IRoundState roundState)
	{
		ScoringStrategyPreConditionNullCheck.Check(player, roundState);
		EnsuresPlayerHasCards(player, roundState);

		int throwCard = roundState.ThrowCards[player].Number;
		int catchCard = roundState.CatchCards[player].Number;

		return Math.Abs(throwCard - catchCard);
	}

	private static void EnsuresPlayerHasCards(IPlayer player, IRoundState roundState)
	{
		if (!roundState.ThrowCards.ContainsKey(player) ||
					!roundState.CatchCards.ContainsKey(player))
			throw new InvalidOperationException("Player does not have both throw and catch cards recorded.");
	}
}

using BoomerangGame.Core.Domain.ScoringStrategies;
using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Domain.States.RoundStates;

namespace BoomerangGame.Core.Scoring;

/// <summary>
/// Provides functionality for calculating round scores and determining <br/>
/// the winner among players based on configured scoring strategies.
/// </summary>
public class ScoreEngine : IScoreEngine
{
	private readonly IEnumerable<IScoreCategory> _scoreStrategies;
	private readonly ITieBreaker _tieBreaker;
	private string _tieBreakerIdentifier;

	/// <summary>
	/// Initializes a new instance of <see cref="ScoreEngine"/> using
	/// the provided scoring strategies, region tracker, and tie-breaker logic.
	/// </summary>
	/// <param name="scoreStrategies">A collection of scoring strategies to apply when calculating scores.</param>
	/// <param name="tieBreaker">Tie-breaking logic used when multiple players share the same score.</param>
	/// <param name="tieBreakerIdentifier">Identifier determining which tie-breaker strategy to apply.</param>
	/// <exception cref="ArgumentNullException">
	/// Thrown if <paramref name="scoreStrategies"/> or <paramref name="tieBreaker"/> is null.
	/// </exception>
	public ScoreEngine(
		IEnumerable<IScoreCategory> scoreStrategies,
		ITieBreaker tieBreaker,
		string tieBreakerIdentifier
	)
	{
		_scoreStrategies = scoreStrategies
			?? throw new ArgumentNullException(nameof(scoreStrategies));
		_tieBreaker = tieBreaker
			?? throw new ArgumentNullException(nameof(tieBreaker));
		_tieBreakerIdentifier = tieBreakerIdentifier;
	}


	/// <inheritdoc/>
	public int CalculateRoundScore(
		IBoomerangPlayerState playerState,
		IRoundState roundState,
		string chosenBlueIcon
	)
	{
		int score = 0;

		foreach (var strategy in _scoreStrategies)
		{
			if (strategy is IRoundScoreCategory roundScoreStrategy)
				score += roundScoreStrategy.CalculateScore(playerState, roundState);
			else if (strategy is IBlueIconScoreCategory blueIconScoreCategory)
				score += blueIconScoreCategory.CalculateScore(playerState, chosenBlueIcon);
			else
				score += strategy.CalculateScore(playerState);
		}

		return score;
	}

	/// <inheritdoc/>
	public IPlayerState DecideWinner(IEnumerable<IPlayerState> playerStates)
	{
		List<IPlayerState> highestScoringPlayer = FindHighestScoringPlayer(playerStates);

		if (highestScoringPlayer.Count > 1)
		{
			highestScoringPlayer
				= _tieBreaker.DecideTieWinner(_tieBreakerIdentifier, highestScoringPlayer);
		}

		return highestScoringPlayer[0];
	}


	private List<IPlayerState> FindHighestScoringPlayer(IEnumerable<IPlayerState> playerStates)
	{
		List<IPlayerState> highestScoringPlayer = new List<IPlayerState>();
		int highestScore = int.MinValue;

		foreach (IPlayerState playerState in playerStates)
		{
			if (playerState.Score > highestScore)
			{
				highestScore = playerState.Score;
				highestScoringPlayer = SetNewHighestScoringPlayer(highestScoringPlayer, playerState);
			}
			else if (playerState.Score == highestScore)
			{
				highestScoringPlayer.Add(playerState);
			}
		}

		return highestScoringPlayer;
	}


	private List<IPlayerState> SetNewHighestScoringPlayer(
		List<IPlayerState> highestScoringPlayer,
		IPlayerState playerState)
	{
		highestScoringPlayer.Clear();
		highestScoringPlayer.Add(playerState);

		return highestScoringPlayer;
	}
}

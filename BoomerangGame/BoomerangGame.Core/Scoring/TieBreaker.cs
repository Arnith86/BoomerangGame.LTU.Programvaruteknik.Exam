using BoomerangGame.Core.Domain.ScoringStrategies.Utilities;
using BoomerangGame.Core.Domain.States.PlayerState;

namespace BoomerangGame.Core.Scoring;

public class TieBreaker : ITieBreaker
{

	public Dictionary<string, List<IScoreEntry>> ScoreByCategory { get; private set; }

	public TieBreaker()
	{
		ScoreByCategory = new Dictionary<string, List<IScoreEntry>>();
	}

	/// <inheritdoc/>
	public void RegisterScoreByCategory(string playerName, string strategy, int points)
	{
		if (!ScoreByCategory.TryGetValue(strategy, out var entries))
		{
			entries = new List<IScoreEntry>();
			ScoreByCategory[strategy] = entries;
		}

		// Find the player's entry, or create it if it doesn't exist
		var playerEntry = entries.FirstOrDefault(e => e.Name == playerName);

		if (playerEntry is null)
			entries.Add(new ScoreEntry(playerName, points));
		else
			playerEntry.AddToPoints(points);
	}

	/// <inheritdoc/>
	public List<IPlayerState> DecideTieWinner(string strategy, List<IPlayerState> playerStates)
	{
		if (!ScoreByCategory.ContainsKey(strategy) || playerStates.Count == 0)
			throw new InvalidOperationException("No scores available for the given strategy or no players provided.");

		List<IPlayerState> highestScoringPlayer = new List<IPlayerState>();
		int highestScore = int.MinValue;

		foreach (IPlayerState playerState in playerStates)
		{
			IScoreEntry? scoreEntry = ScoreByCategory[strategy]
				.FirstOrDefault(p => p.Name == playerState.Name);

			if (scoreEntry == null)	continue; 

			if (scoreEntry.Points > highestScore)
			{
				highestScoringPlayer.Clear();
				highestScore = scoreEntry.Points;
				highestScoringPlayer.Add(
					playerStates.FirstOrDefault(ps => ps.Name == scoreEntry.Name)!
				);
			}
		}

		return highestScoringPlayer;
	}
}

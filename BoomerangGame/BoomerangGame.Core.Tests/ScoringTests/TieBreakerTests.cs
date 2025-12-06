using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Scoring;
using BoomerangGame.Core.Tests.HelperClasses;

namespace BoomerangGame.Core.Tests.ScoringTests;


public class TieBreakerTests
{
	private readonly ITieBreaker _tieBreaker;
	private readonly MockPlayerCreator _mockPlayerCreator;

	public TieBreakerTests()
	{
		_tieBreaker = new TieBreaker();
		_mockPlayerCreator = new MockPlayerCreator();
	}

	[Fact]
	public void RegisterScoreByCategory_ShouldAddNewEntry_WhenStrategyDoesNotExist_RQ12a()
	{
		_tieBreaker.RegisterScoreByCategory("Alice", "Strategy1", 5);

		// Assert
		Assert.True(_tieBreaker.ScoreByCategory.ContainsKey("Strategy1"));
		var entries = _tieBreaker.ScoreByCategory["Strategy1"];

		Assert.Single(entries);
		Assert.Equal("Alice", entries[0].Name);
		Assert.Equal(5, entries[0].Points);
	}

	[Fact]
	public void RegisterScoreByCategory_ShouldAddPoints_WhenPlayerAlreadyExists()
	{
		// Arrange & Act 
		_tieBreaker.RegisterScoreByCategory("Alice", "Strategy1", 5);
		_tieBreaker.RegisterScoreByCategory("Alice", "Strategy1", 3);

		// Assert
		var entries = _tieBreaker.ScoreByCategory["Strategy1"];

		Assert.Equal(8, entries[0].Points); 
	}

	[Fact]
	public void DecideWinner_ShouldReturnPlayerWithHighestPoints()
	{
		// Arrange
		_tieBreaker.RegisterScoreByCategory("Alice", "Strategy1", 5);
		_tieBreaker.RegisterScoreByCategory("Bob", "Strategy1", 8);
		_tieBreaker.RegisterScoreByCategory("Charlie", "Strategy1", 3);


		var players = new List<IPlayerState>
		{
			_mockPlayerCreator.CreateMockIPlayerStateNameScore("Alice", 5),
			_mockPlayerCreator.CreateMockIPlayerStateNameScore("Bob", 8),
			_mockPlayerCreator.CreateMockIPlayerStateNameScore("Charlie", 3)
		};

		// Act
		var winners = _tieBreaker.DecideTieWinner("Strategy1", players);

		// Assert
		Assert.Equal("Bob", winners[0].Name);
	}

	[Fact]
	public void DecideWinner_ShouldThrow_WhenStrategyDoesNotExist()
	{
		// Arrange
		var tieBreaker = new TieBreaker();

		var players = new List<IPlayerState>
		{
			_mockPlayerCreator.CreateMockIPlayerStateNameScore("Alice", 5),
			_mockPlayerCreator.CreateMockIPlayerStateNameScore("Bob", 8),
			_mockPlayerCreator.CreateMockIPlayerStateNameScore("Charlie", 3)
		};

		// Act & Assert
		Assert.Throws<InvalidOperationException>(() =>
			tieBreaker.DecideTieWinner("NonExistentStrategy", players));
	}
}

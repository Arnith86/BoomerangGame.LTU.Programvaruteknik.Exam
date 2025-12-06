using BoomerangGame.Core.Scoring;

namespace BoomerangGame.Core.Tests.ScoringTests;

public class RegionProgressTrackerTests
{

	private IRegionProgressTracker _sut;

	public RegionProgressTrackerTests()
	{
		_sut = new RegionProgressTracker();
	}

	[Fact]
	public void EligibleForRegionBonus_RegionNotPresent_ReturnsTrue()
	{
		// Arrange
		string region = "NonExistent";
		int currentRound = 1;

		// Act
		bool result = _sut.EligibleForRegionBonus(region, currentRound);

		// Assert
		Assert.True(result);
	}
	
	[Fact]
	public void EligibleForRegionBonus_TwoRegionsNotPresent_SameRound_ReturnsTrue()
	{
		// Arrange
		string region = "NonExistent";
		int currentRound = 1;
		bool result = _sut.EligibleForRegionBonus(region, currentRound);

		string region2 = "NonExistent2";
		int currentRound2 = 1;

		// Act
		bool result2 = _sut.EligibleForRegionBonus(region2, currentRound2);

		// Assert
		Assert.True(result2);
	}

	[Fact]
	public void MarkRegionAsCompleted_DoesNotOverwriteExistingEntry()
	{
		// Arrange
		string region = "RegionD";
		int firstRound = 2;
		int secondRound = 5;

		// Act
		_sut.MarkRegionAsCompleted(region, firstRound);
		
		// Assert
		Assert.False(_sut.MarkRegionAsCompleted(region, secondRound));
	}

	[Fact]
	public void MarkRegionAsCompleted_Then_EligibleForRegionBonus_SameRound_ReturnsTrue()
	{
		// Arrange
		string region = "RegionA";
		int round = 5;

		// Act
		_sut.MarkRegionAsCompleted(region, round);
		bool eligible = _sut.EligibleForRegionBonus(region, round);

		// Assert
		Assert.True(eligible);
	}

	[Fact]
	public void MarkRegionAsCompleted_Then_EligibleForRegionBonus_DifferentRound_ReturnsFalse()
	{
		// Arrange
		string region = "RegionB";
		int completedRound = 3;
		int currentRound = 4;

		// Act
		_sut.MarkRegionAsCompleted(region, completedRound);
		bool eligible = _sut.EligibleForRegionBonus(region, currentRound);

		// Assert
		Assert.False(eligible);
	}

	[Fact]
	public void Reset_ClearsCompletedRegions_ThenRegionIsConsideredNotPresent()
	{
		// Arrange
		string region = "RegionC";
		int round = 2;

		// Act
		_sut.MarkRegionAsCompleted(region, round);
		_sut.Reset();
		bool eligibleAfterReset = _sut.EligibleForRegionBonus(region, round);

		// Assert
		// After reset the region should be treated as not present => eligible
		Assert.True(eligibleAfterReset);
	}

	
}

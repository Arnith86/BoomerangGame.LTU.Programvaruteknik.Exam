using BoomerangGame.Core.Domain.States;
using BoomerangGame.Core.Domain.States.MapStates;

namespace BoomerangGame.Core.Tests.Domain.StatesTests;

public class RegionStateTests
{
	private readonly List<string> WesternAustraliaSites = new() { "A", "B", "C", "D" };
	
	private IRegionsState _regionsState;
	
	public RegionStateTests()
	{
		_regionsState = new RegionState(WesternAustraliaSites);
	}

	

	[Fact]
	public void IsComplete_ShouldBeFalse_WhenNoSitesVisited()
	{
		Assert.False(_regionsState.IsComplete());
	}

	[Fact]
	public void UpdateRegionsState_3Missing_ShouldMarkProgress()
	{
		_regionsState.UpdateRegionsState("A");

		Assert.False(_regionsState.IsComplete()); 
	}

	[Fact]
	public void UpdateRegionsState_ShouldIgnoreDuplicates()
	{
		_regionsState.UpdateRegionsState("A");
		_regionsState.UpdateRegionsState("A");
		_regionsState.UpdateRegionsState("A");

		Assert.False(_regionsState.IsComplete());
	}

	[Fact]
	public void IsComplete_ShouldBeTrue_WhenAllSitesVisited()
	{
		_regionsState.UpdateRegionsState("A");
		_regionsState.UpdateRegionsState("B");
		_regionsState.UpdateRegionsState("C");
		_regionsState.UpdateRegionsState("D");

		Assert.True(_regionsState.IsComplete());
	}

	[Fact]
	public void UpdateRegionsState_UnknownSite_ShouldNotAffectCompletion()
	{
		_regionsState.UpdateRegionsState("X"); // Not part of this region
		_regionsState.UpdateRegionsState("A");
		_regionsState.UpdateRegionsState("B");
		_regionsState.UpdateRegionsState("C");

		// Only 3 out of 4 required
		Assert.False(_regionsState.IsComplete());
	}

	[Fact]
	public void RegionState_ShouldWorkWithAccentedLetters()
	{
		var tasmaniaSites = new List<string> { "Z", "Å", "Ä", "Ö" };
		var state = new RegionState(tasmaniaSites);

		state.UpdateRegionsState("Z");
		state.UpdateRegionsState("Å");
		state.UpdateRegionsState("Ä");
		state.UpdateRegionsState("Ö");

		Assert.True(state.IsComplete());
	}

}

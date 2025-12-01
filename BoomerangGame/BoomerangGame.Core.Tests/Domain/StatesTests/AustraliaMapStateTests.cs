using BoomerangGame.Core.Domain.States;

namespace BoomerangGame.Core.Tests.Domain.StatesTests;


public class AustraliaMapStateTests
{
	private readonly Dictionary<string, List<string>> _testMap;
	private readonly AustraliaMapState _sut; 

	public AustraliaMapStateTests()
	{
		_testMap = new Dictionary<string, List<string>>
		{
			{ "North", new List<string> { "SiteA", "SiteB" } },
			{ "South", new List<string> { "SiteC" } }
		};

		_sut = new AustraliaMapState(_testMap);
	}

	[Fact]
	public void Constructor_ShouldInitializeAllRegions()
	{
		// Assert
		Assert.Equal(2, _sut.CurrentMapState.Count);
		Assert.True(_sut.CurrentMapState.ContainsKey("North"));
		Assert.True(_sut.CurrentMapState.ContainsKey("South"));
	}

	[Fact]
	public void Constructor_ShouldInitializeRegionState()
	{
		// Assert
		Assert.NotNull(_sut.CurrentMapState["North"]);
		Assert.IsType<RegionState>(_sut.CurrentMapState["North"]);
	}

	[Fact]
	public void UpdateMapState_ShouldNotMarkAsComplete_SitesNotAllVisited()
	{
		// Act
		_sut.UpdateMapState("North", "SiteA");

		var north = _sut.CurrentMapState["North"];

		// Assert
		Assert.False(north.IsComplete()); // SiteB still unvisited
	}

	[Fact]
	public void UpdateMapState_ShouldCompleteRegion_WhenAllSitesVisited()
	{

		// Act
		_sut.UpdateMapState("South", "SiteC");

		var south = _sut.CurrentMapState["South"];

		// Assert
		Assert.True(south.IsComplete());
	}

	[Fact]
	public void UpdateMapState_ShouldThrow_WhenRegionDoesNotExist()
	{
		// Assert
		Assert.Throws<ArgumentException>(() =>
			_sut.UpdateMapState("InvalidRegion", "SiteX"));
	}

	[Fact]
	public void Constructor_ShouldThrow_WhenMapIsNullOrEmpty()
	{
		// Assert
		Assert.Throws<ArgumentException>(() => new AustraliaMapState(null!));
		Assert.Throws<ArgumentException>(() => new AustraliaMapState(new Dictionary<string, List<string>>()));
	}
}


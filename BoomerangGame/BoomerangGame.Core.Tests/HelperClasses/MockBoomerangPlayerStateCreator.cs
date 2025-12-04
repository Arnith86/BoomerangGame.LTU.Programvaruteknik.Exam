using BoomerangGame.Core.Domain.States.PlayerState;
using Moq;

namespace BoomerangGame.Core.Tests.HelperClasses;

public class MockBoomerangPlayerStateCreator
{
	
	public HashSet<string> SetVisitedSites(int nrOfSites)
	{
		HashSet<string> visitedSites = new HashSet<string>(); 
		
		for (int i = 0; i < nrOfSites ; i++)
		{
			visitedSites.Add($"site{i}");
		}

		return visitedSites;
	}

	public Mock<IBoomerangPlayerState>CreateMockBoomerangPlayerState(
		string playerName, 
		int nrVisitedSites = 2
	)
	{
		Mock<IBoomerangPlayerState> mock = new Mock<IBoomerangPlayerState>();
		mock.SetupGet(bPS => bPS.Name).Returns(playerName);
		mock.SetupGet(bPS => bPS.VisitedSites).Returns(SetVisitedSites(nrVisitedSites));

		return mock;
	}
}

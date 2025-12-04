using BoomerangGame.Core.Domain.States.PlayerState;
using Moq;

namespace BoomerangGame.Core.Tests.HelperClasses;

public class MockBoomerangPlayerStateCreator
{
	
	public Mock<IBoomerangPlayerState>CreateMockBoomerangPlayerState(string playerName)
	{
		Mock<IBoomerangPlayerState> mock = new Mock<IBoomerangPlayerState>();
		mock.SetupGet(bPS => bPS.Name).Returns(playerName);

		return mock;
	}
}

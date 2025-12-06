using BoomerangGame.Core.Application;
using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Port;
using Moq;

namespace BoomerangGame.Core.Tests.HelperClasses;

/// <summary>
/// Provides helper methods for creating mocked <see cref="IPlayer"/> <br/>
/// instances for use in unit tests.
/// </summary>
public class MockPlayerCreator
{
	public Mock<IPlayer> CreateSimpleMockPlayer()
	{
		var mockState = new Mock<IBoomerangPlayerState>();
		var mockChannel = new Mock<IPlayerChannel>();

		var mockPlayer = new Mock<IPlayer>();
		mockPlayer.Setup(p => p.PlayerState).Returns(mockState.Object);
		mockPlayer.Setup(p => p.PlayerChannel).Returns(mockChannel.Object);

		return mockPlayer;
	}

	public IPlayerState CreateMockIPlayerStateNameScore(string name, int score = 0)
	{
		var mock = new Mock<IPlayerState>();
		mock.Setup(p => p.Name).Returns(name);
		mock.Setup(p => p.Score).Returns(score);
		
		return mock.Object;
	}
}

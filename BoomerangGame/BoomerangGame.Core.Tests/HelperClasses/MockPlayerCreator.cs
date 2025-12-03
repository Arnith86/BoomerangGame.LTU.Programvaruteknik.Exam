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
		var mockState = new Mock<IPlayerState>();
		var mockChannel = new Mock<IPlayerChannel>();

		var mockPlayer = new Mock<IPlayer>();
		mockPlayer.Setup(p => p.PlayerState).Returns(mockState.Object);
		mockPlayer.Setup(p => p.PlayerChannel).Returns(mockChannel.Object);

		return mockPlayer;
	}
}

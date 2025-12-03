using BoomerangGame.Core.Application;
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
		return new Mock<IPlayer>();
	}
}

using BoomerangGame.Core.Domain.Cards;
using Moq;

namespace BoomerangGame.Core.Tests.HelperClasses;

/// <summary>
/// Provides helper methods for creating mocked <see cref="IBoomerangCard"/> <br/>
/// instances for use in unit tests.
/// </summary>
public class MockCardCreator
{
	/// <summary>
	/// Creates a mock <see cref="IBoomerangCard"/> whose <see cref="IBoomerangCard.Number"/> <br/>
	/// property returns the specified value.
	/// </summary>
	/// <param name="number">
	/// The integer value to be returned by the mocked card's <see cref="IBoomerangCard.Number"/> property.
	/// </param>
	/// <returns>
	/// A <see cref="Mock{T}"/> configured to represent an <see cref="IBoomerangCard"/> with the given number.
	/// </returns>
	public Mock<IBoomerangCard> CreateMockCardWithSetNumber(int number)
	{
		var mock = new Mock<IBoomerangCard>();
		mock.Setup(c => c.Number).Returns(number);
		return mock;
	}
}

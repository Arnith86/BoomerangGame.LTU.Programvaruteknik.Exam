using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.Cards.Symbols;
using Moq;
using System.Globalization;

namespace BoomerangGame.Core.Tests.HelperClasses;

/// <summary>
/// Provides helper methods for creating mocked <see cref="IBoomerangCard"/> <br/>
/// instances for use in unit tests.
/// </summary>
public class MockCardCreator
{
	private MockSymbolSetCreator<string> _mockSymbolSetCreator;
	
	public MockCardCreator()
	{
		_mockSymbolSetCreator = new MockSymbolSetCreator<string>();
	}

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
	public Mock<IBoomerangCard<string>> CreateMockCardWithSetNumber(int number)
	{
		var mock = new Mock<IBoomerangCard<string>>();
		mock.Setup(c => c.Number).Returns(number);
		return mock;
	}

	public Mock<IBoomerangCard<string>> CreateSingleMockCardWithSetCollection(string type, string value)
	{
		Mock<ISymbolSet<string>> mockSymbolSet = 
			_mockSymbolSetCreator.CreateMockSymbolSetWithFirst(type, value);
		
		Mock<IBoomerangCard<string>> mockCard = new Mock<IBoomerangCard<string>>();
		mockCard.SetupGet(c => c.Symbols).Returns(mockSymbolSet.Object);

		return mockCard;
	}

	public List<Mock<IBoomerangCard<string>>> CreateSetOfMockCardWithSingleSetOfSymbols(
		string type1, string value1,
		string type2, string value2,
		string type3, string value3,
		string type4, string value4,
		string type5, string value5,
		string type6, string value6,
		string type7, string value7)
	{
		List<Mock<IBoomerangCard<string>>> cards = new List<Mock<IBoomerangCard<string>>>
		{
			CreateSingleMockCardWithSetCollection(type1, value1),
			CreateSingleMockCardWithSetCollection(type2, value2),
			CreateSingleMockCardWithSetCollection(type3, value3),
			CreateSingleMockCardWithSetCollection(type4, value4),
			CreateSingleMockCardWithSetCollection(type5, value5),
			CreateSingleMockCardWithSetCollection(type6, value6),
			CreateSingleMockCardWithSetCollection(type7, value7)
		};

		return cards;
	}

}

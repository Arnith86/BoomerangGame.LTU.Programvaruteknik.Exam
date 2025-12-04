using BoomerangGame.Core.Domain.Cards.Symbols;
using Moq;

namespace BoomerangGame.Core.Tests.HelperClasses;

public class MockSymbolSetCreator<TValue>
{
	public Mock<ISymbolSet<TValue>> CreateMockSymbolSetWithFirst(string type, TValue value)
	{
		var mockSymbolSet = new Mock<ISymbolSet<TValue>>() { CallBase = true };
		mockSymbolSet.SetupGet(ss => ss.First).Returns(new Symbol<TValue>(type, value));

		return mockSymbolSet;
	}
}

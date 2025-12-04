using BoomerangGame.Core.Application;
using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.States.RoundStates;
using Moq;

namespace BoomerangGame.Core.Tests.HelperClasses;

public class MockRoundStateCreator
{
	public Mock<IRoundState> CreateMockRoundState(
	IPlayer player,
	IBoomerangCard? throwCard = null,
	IBoomerangCard? catchCard = null)
	{
		var mock = new Mock<IRoundState>();

		var throwDict = new Dictionary<string, IBoomerangCard>();
		var catchDict = new Dictionary<string, IBoomerangCard>();

		if (throwCard != null)
			throwDict[player.PlayerState.Name] = throwCard;

		if (catchCard != null)
			catchDict[player.PlayerState.Name] = catchCard;

		mock.Setup(r => r.ThrowCards).Returns(throwDict);
		mock.Setup(r => r.CatchCards).Returns(catchDict);

		return mock;
	}
}

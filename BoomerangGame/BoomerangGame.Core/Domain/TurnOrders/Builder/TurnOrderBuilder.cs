namespace BoomerangGame.Core.Domain.TurnOrders.Builder;

//TODO: JSON should be directly mappable to the enum, and alternating should be available
public class TurnOrderBuilder : ITurnOrderBuilder
{
	public ITurnOrder CreateTurnOrder(string direction)
	{
		PassDirection passDirection = PassDirection.COUNTERCLOCKWISE;

		if (direction != "left")
			passDirection = PassDirection.CLOCKWISE;

		return new TurnOrder(passDirection);
	}
}

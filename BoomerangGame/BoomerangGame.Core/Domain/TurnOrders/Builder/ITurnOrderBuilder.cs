namespace BoomerangGame.Core.Domain.TurnOrders.Builder
{
	public interface ITurnOrderBuilder
	{
		ITurnOrder CreateTurnOrder(string direction);
	}
}
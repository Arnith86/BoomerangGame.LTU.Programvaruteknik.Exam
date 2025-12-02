namespace BoomerangGame.Core.Domain.TurnOrders;

/// <summary>
/// Represents the order in which players pass cards or take turns
/// during gameplay.
/// </summary>
public enum PassDirection
{
	CLOCKWISE = 1,
	COUNTERCLOCKWISE = -1
}

namespace BoomerangGame.Core.Domain.States.RoundStates;

/// <summary>
/// Represents a draft pick containing an entity and an item, possibly of different types.
/// </summary>
/// <typeparam name="T">The type of the main entity in the draft pick.</typeparam>
/// <typeparam name="I">The type of the item associated with the draft pick.</typeparam>
public interface IDraftPick<T,I>
{
	/// <summary>
	/// Gets the main entity associated with this draft pick.
	/// </summary
	T Entity { get; init; }

	/// <summary>
	/// Gets the item associated with this draft pick.
	/// </summary>
	I Item { get; init; }
}
namespace BoomerangGame.Core.Domain.States.RoundStates;

public record DraftPick<T, I>(T Entity, I Item) : IDraftPick<T, I>
{
	public T Entity { get; init; } = Entity ?? throw new ArgumentNullException(nameof(Entity));
	public I Item { get; init; } = Item ?? throw new ArgumentNullException(nameof(Item));
}
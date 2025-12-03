namespace BoomerangGame.Core.Domain.States.RoundStates;

public static class DraftPickCreator
{
	public static IDraftPick<T, I> Create<T, I>(T entity, I item)
	{
		return new DraftPick<T, I>(entity, item);
	}
}

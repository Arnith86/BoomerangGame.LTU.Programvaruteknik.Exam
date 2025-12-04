
namespace BoomerangGame.Core.Domain.Cards.Symbols
{
	public interface ISymbolSet<TValue>
	{
		Symbol<TValue>? First { get; init; }
		Symbol<TValue>? Second { get; init; }
		IReadOnlyList<Symbol<TValue>> Symbols { get; }
		Symbol<TValue>? Third { get; init; }

		bool ContainsCategory(string category);
		//void Deconstruct(out Symbol? First, out Symbol? Second, out Symbol? Third);
		string ToString();
	}
}
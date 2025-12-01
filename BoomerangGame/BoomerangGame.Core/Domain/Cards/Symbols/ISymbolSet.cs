
namespace BoomerangGame.Core.Domain.Cards.Symbols
{
	public interface ISymbolSet
	{
		Symbol? First { get; init; }
		Symbol? Second { get; init; }
		IReadOnlyList<Symbol> Symbols { get; }
		Symbol? Third { get; init; }

		bool ContainsCategory(string category);
		//void Deconstruct(out Symbol? First, out Symbol? Second, out Symbol? Third);
		string ToString();
	}
}
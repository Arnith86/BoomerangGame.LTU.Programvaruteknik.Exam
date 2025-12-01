
namespace BoomerangGame.Core.Domain.Cards.Symbols
{
	public interface ISymbolSet
	{
		Symbol? First { get; init; }
		bool IsEmpty { get; }
		Symbol? Second { get; init; }
		IReadOnlyList<Symbol> Symbols { get; }
		Symbol? Third { get; init; }

		bool ContainsCategory(string category);
		void Deconstruct(out Symbol? First, out Symbol? Second, out Symbol? Third);
		bool Equals(object? obj);
		bool Equals(SymbolSet? other);
		int GetHashCode();
		Symbol? GetSymbolByCategory(string category);
		string ToString();
	}
}
using BoomerangGame.Core.Domain.Cards.Symbols;

namespace BoomerangGame.Core.Domain.Cards;

public class BoomerangCard : IBoomerangCard
{
	private readonly string _name;
	private readonly string _region;
	private readonly string _letter;
	private readonly int _number;
	private readonly SymbolSet _symbolSet;

	public BoomerangCard(string name, string region, string letter, int number, SymbolSet symbolSet)
	{
		_name = name;
		_region = region;
		_letter = letter;
		_number = number;
		_symbolSet = symbolSet;
	}

	public string Name => _name;

	public string Letter => _letter;

	public string Region => _region;

	public int Number => _number;

	public SymbolSet Symbols => _symbolSet;
}

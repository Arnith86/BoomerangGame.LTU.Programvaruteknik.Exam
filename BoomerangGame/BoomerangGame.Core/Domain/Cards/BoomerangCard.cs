using BoomerangGame.Core.Domain.Cards.Symbols;

namespace BoomerangGame.Core.Domain.Cards;

public class BoomerangCard<TValue> : IBoomerangCard<TValue>
{
	private readonly string _name;
	private readonly string _region;
	private readonly string _site;
	private readonly int _number;
	private readonly ISymbolSet<TValue> _symbols;
	private bool _isHidden;

	public BoomerangCard(
		string name, 
		string region, 
		string site, 
		int number, 
		ISymbolSet<TValue> symbols)
	{
		_name = name;
		_region = region;
		_site = site;
		_number = number;
		_symbols = symbols;
		_isHidden = false;
	}

	public string Name => _name;

	public string Site => _site;

	public string Region => _region;

	public int Number => _number;

	public ISymbolSet<TValue> Symbols => _symbols;

	public bool IsHidden => _isHidden;

	public void ToggleIsHidden() => _isHidden = !_isHidden;
}

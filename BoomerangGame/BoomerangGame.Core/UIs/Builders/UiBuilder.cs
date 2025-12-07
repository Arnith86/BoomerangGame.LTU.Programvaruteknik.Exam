using BoomerangGame.Core.UIs.CWritePrint;

namespace BoomerangGame.Core.UIs.Builders;

public class UiBuilder : IUiBuilder
{
	private readonly IConsoleWritePrint _cwp;

	public UiBuilder(IConsoleWritePrint cwp)
	{
		_cwp = cwp;
	}

	public IUI GetUi() => new ConsoleUI(_cwp);

}

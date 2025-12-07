using BoomerangGame.Core.UIs.CWritePrint;

namespace BoomerangGame.Core.UIs;

public class ConsoleUI : IUI
{
	private readonly IConsoleWritePrint _cwp;

	public ConsoleUI(IConsoleWritePrint cwp)
	{
		_cwp = cwp;
	}
	public void DisplayMessage(string message) => _cwp.WriteLine(message);


	public string GetInput() 
	{
		string input = string.Empty;
		bool validInput = false; 

		do {
			
			input = _cwp.ReadLine();
			if (!String.IsNullOrWhiteSpace(input))
				validInput = true;
			else
				_cwp.Write("Only a single character is allowed, try again!");

		} while (!validInput);

		return input;
	}
}

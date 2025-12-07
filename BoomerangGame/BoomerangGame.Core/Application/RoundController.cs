using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.States.RoundStates;
using BoomerangGame.Core.Domain.TurnOrders;
using BoomerangGame.Core.Scoring;

namespace BoomerangGame.Core.Application;

public class RoundController : IRoundController
{
	private IScoreEngine _scoreEngine;
	private readonly ITurnOrder _turnOrder;

	public RoundController(
		IScoreEngine scoreEngine,
		ITurnOrder turnOrder
	)
	{
		_scoreEngine = scoreEngine;
		_turnOrder = turnOrder;
	}

	public void RunRound(List<IPlayer> players, List<IBoomerangCard<string>> deck)
	{
		throw new NotImplementedException();
	}

	public void EvaluateRound(List<IPlayer> players)
	{
		throw new NotImplementedException();
	}

	public IRoundState CreateRoundState()
	{
		throw new NotImplementedException();
	}
}

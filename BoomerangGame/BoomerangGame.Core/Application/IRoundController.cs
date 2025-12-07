using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.States.RoundStates;

namespace BoomerangGame.Core.Application
{
	public interface IRoundController
	{
		IRoundState CreateRoundState();
		void EvaluateRound(List<IPlayer> players);
		void RunRound(List<IPlayer> players, List<IBoomerangCard<string>> deck);
	}
}
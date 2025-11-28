using BoomerangGame.Core.Domain.Cards;

namespace BoomerangGame.Core.Domain.RuleSets;

public interface IRuleSet
{
	IReadOnlyList<ICard> Deck { get; }
	//IReadOnlyList<IScoringStrategy> ScoringStrategies { get; }

	//ITieBreakerStrategy TieBreaker { get; }
	//ITurnOrderStrategy TurnOrder { get; }

	IReadOnlyList<string> Regions { get; }
}

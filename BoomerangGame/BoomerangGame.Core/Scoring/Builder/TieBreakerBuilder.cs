namespace BoomerangGame.Core.Scoring.Builder;

public class TieBreakerBuilder : ITieBreakerBuilder
{
	public ITieBreaker CreateTieBreaker() => new TieBreaker();
}

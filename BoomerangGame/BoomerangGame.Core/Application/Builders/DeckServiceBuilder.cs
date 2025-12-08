namespace BoomerangGame.Core.Application.Builders;

public class DeckServiceBuilder : IDeckServiceBuilder
{
	public IDeckServices CreateDeckServices() => new DeckServices();
}

namespace BoomerangGame.Core.Config.Factories.ScoreCategories;

public static class AbstractScoreCategoryFactoryProducer
{
	public static IScoreCategoryFactory GetFactory(string edition)
	{
		return edition switch
		{
			"Australia" => new AustraliaScoreCategoryFactory(),
			_ => throw new NotImplementedException($"Factory for {edition} not implemented")
		};

	}
}
namespace BoomerangGame.Core.Tests.HelperClasses;

public static class GetEditionJSON
{
	public static string GetValidEditionConfigJSON()
	{
		var path = Path.Combine(AppContext.BaseDirectory, "ValidTestConfigurationFile.json");
		
		return Path.Combine(AppContext.BaseDirectory, "ValidTestConfigurationFile.json");
	}
}

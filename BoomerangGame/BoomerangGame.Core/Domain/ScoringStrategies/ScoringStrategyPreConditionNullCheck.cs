namespace BoomerangGame.Core.Domain.ScoringStrategies;

/// <summary>
/// Provides static helper methods to validate that objects used in <br/r>
/// scoring strategies are not null. 
/// Throws an <see cref="ArgumentNullException"/> if a null value is detected.
/// </summary>
public static class ScoringStrategyPreConditionNullCheck
{
	public static void Check<T>(T state)
	{
		if (state is null)
			ThrowStateArgumentNullException(nameof(state));
	}

	public static void Check<T1, T2>(T1 state1, T2 state2)
	{
		if (state1 is null)
			ThrowStateArgumentNullException(nameof(state1));
		if (state2 is null)
			ThrowStateArgumentNullException(nameof(state2));
	}

	private static void ThrowStateArgumentNullException(string paramName)
	{
		throw new ArgumentNullException(paramName, "Must have a value");
	}
}

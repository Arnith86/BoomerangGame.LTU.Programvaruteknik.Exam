namespace BoomerangGame.Core.UIs.Builders;

/// <summary>
/// Defines a factory for creating <see cref="IUI"/> implementations used by the game.
/// </summary>
public interface IUiBuilder
{
	IUI GetUi();
}
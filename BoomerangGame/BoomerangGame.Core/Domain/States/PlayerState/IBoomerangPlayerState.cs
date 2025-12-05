using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.States.MapStates;

namespace BoomerangGame.Core.Domain.States.PlayerState;

/// <summary>
/// Represents the complete state of a player in a Boomerang-style game. <br/>
/// Extends <see cref="IBoomerangPlayerState"/> with card-drafting, map-tracking, <br/>
/// blueIcon history, and visited-site tracking functionality.
/// </summary>
public interface IBoomerangPlayerState : IPlayerState
{
	/// <summary>
	/// Gets the history of all blue icons collected by the player. <br/>
	/// Each entry represents a unique blue icon encountered.
	/// </summary>
	HashSet<string> BlueIconHistory { get; }

	/// <summary>
	/// Gets the map state used to track region and site progression for this player during the game.
	/// </summary>
	IMapState MapState { get; }

	/// <summary>
	/// Gets the list of drafted cards that the player has accumulated. <<br/>
	/// The first drafted card is typically hidden based on game rules.
	/// </summary>
	List<IBoomerangCard<string>> DraftedCards { get; }

	/// <summary>
	/// Gets the set of unique sites the player has visited through drafted cards.
	/// </summary>
	HashSet<string> VisitedSites { get; }

	/// <summary>
	/// Adds a card to the player's drafted card collection.
	/// Also updates visited sites and map progression.
	/// </summary>
	/// <param name="card">The card to add to the player's draft.</param>
	/// <exception cref="ArgumentNullException">
	/// Thrown if <paramref name="card"/> is <c>null</c>.
	/// </exception>
	void AddToDraftedCards(IBoomerangCard<string> card);

	/// <summary>
	/// Records a blue icon collected by the player and stores it in the history set.
	/// </summary>
	/// <param name="blueIcon">The blue icon identifier to record.</param>
	/// <exception cref="ArgumentException">
	/// Thrown if <paramref name="blueIcon"/> is null, empty, or whitespace.
	/// </exception>
	void UpdateBlueIconHistory(string blueIcon);


	/// <summary>
	/// Resets the draft hand state for the player in preparation for the next round of the game.
	/// <para>
	/// Fields representing <em>game-long</em> progress—such as total score, visited sites, <br/>
	/// completed regions, and activity/blue-icon history—are intentionally left untouched.
	/// </para>
	/// </summary>
	void ResetDraftHand();
}
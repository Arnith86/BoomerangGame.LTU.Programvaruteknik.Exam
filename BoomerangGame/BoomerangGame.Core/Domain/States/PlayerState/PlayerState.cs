using BoomerangGame.Core.Domain.Cards;
using BoomerangGame.Core.Domain.States.MapStates;

namespace BoomerangGame.Core.Domain.States.PlayerState;

public class PlayerState : IBoomerangPlayerState
{
	private string _name; 
	private int _score = 0;

	private List<IBoomerangCard<string>> _draftedCards = new List<IBoomerangCard<string>>();
	private HashSet<string> _visitedSites = new HashSet<string>();
	private IMapState _mapState;
	private HashSet<string> _blueIconHistory = new HashSet<string>();

	/// <summary>
	/// Initializes a new instance of the <see cref="PlayerState"/> class <br/>
	/// using the specified map state tracker.
	/// </summary>
	/// <param name="mapState">
	/// The <see cref="IMapState"/> implementation responsible for tracking
	/// region and site progression for the player.
	/// </param>
	/// <exception cref="ArgumentNullException">
	/// Thrown if <paramref name="mapState"/> is <c>null</c>.
	/// </exception>
	public PlayerState(IMapState mapState)
	{
		_mapState = mapState ?? throw new ArgumentNullException(nameof(mapState));
	}

	/// <inheritdoc/>
	public string Name => _name;

	/// <inheritdoc/>
	public int Score => _score;

	/// <inheritdoc/>
	public List<IBoomerangCard<string>> DraftedCards => _draftedCards;
	
	/// <inheritdoc/>
	public HashSet<string> VisitedSites => _visitedSites;
	
	/// <inheritdoc/>
	public HashSet<string> BlueIconHistory => _blueIconHistory;
	
	/// <inheritdoc/>
	public IMapState MapState => _mapState;


	/// <inheritdoc/>
	public void AddToDraftedCards(IBoomerangCard<string> card)
	{
		if (card is null)
			throw new ArgumentNullException(nameof(card), "Card cannot be null.");

		if (_draftedCards.Count == 0) card.ToggleIsHidden();

		MarkSiteVisited(card.Site);
		_draftedCards.Add(card);
		_mapState.UpdateMapState(card.Region, card.Site);
	}

	/// <inheritdoc/>
	public void AddToScore(int points)
	{
		if (points < 0)
			throw new ArgumentOutOfRangeException(nameof(points), "Points to add cannot be negative.");

		_score += points;
	}


	/// <inheritdoc />
	public void UpdateBlueIconHistory(string blueIcon)
	{
		if (string.IsNullOrWhiteSpace(blueIcon))
			throw new ArgumentException("Blue icon cannot be null or empty.", nameof(blueIcon));

		_blueIconHistory.Add(blueIcon);
	}

	
	/// <inheritdoc>/>
	public void ResetDraftHand() => _draftedCards.Clear();
	
	private void MarkSiteVisited(string site) 
		=> _visitedSites.Add(site);
}

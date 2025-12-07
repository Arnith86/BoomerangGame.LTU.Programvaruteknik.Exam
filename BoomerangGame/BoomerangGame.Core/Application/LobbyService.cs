using BoomerangGame.Core.Application;
using BoomerangGame.Core.Domain.States.MapStates;
using BoomerangGame.Core.Domain.States.PlayerState;
using BoomerangGame.Core.Port;

public class LobbyService : ILobbyService
{
	private readonly List<IPlayer> _players = new();
	private readonly int _minPlayers;
	private readonly int _maxPlayers;
	private readonly IMapState _mapState;

	public LobbyService(IMapState mapState, int minPlayers = 2, int maxPlayers = 4 )
	{
		if (minPlayers < 1 || maxPlayers < minPlayers)
			throw new ArgumentOutOfRangeException("Invalid player limits.");

		_minPlayers = minPlayers;
		_maxPlayers = maxPlayers;
		_mapState = mapState;
	}

	public IPlayer AddPlayer(IPlayerChannel channel)
	{
		if (channel is null)
			throw new ArgumentNullException(nameof(channel));

		if (_players.Count >= _maxPlayers)
			throw new InvalidOperationException("Lobby is full.");

		IPlayer player = new Player(new PlayerState(_mapState), channel);
		_players.Add(player);

		return player;
	}

	public bool ValidatePlayerCount()
	{
		return _players.Count >= _minPlayers &&
			   _players.Count <= _maxPlayers;
	}

	public List<IPlayer> GetPlayers() => new List<IPlayer>(_players);

	public Task<bool> IsReady()
	{
		return Task.FromResult(ValidatePlayerCount());
	}
}

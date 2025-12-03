using BoomerangGame.Core.Application;
using BoomerangGame.Core.Domain.TurnOrders;
using Moq;

namespace BoomerangGame.Core.Tests.Domain;

public class TurnOrderTests
{
	private List<IPlayer> CreateMockPlayers(int count)
	{
		var list = new List<IPlayer>();
		for (int i = 0; i < count; i++)
		{
			var mock = new Mock<IPlayer>();
			mock.Setup(p => p.ToString()).Returns($"Player{i}");
			list.Add(mock.Object);
		}
		return list;
	}

	[Fact]
	public void GetNextPlayer_ClockwiseDirection_ReturnsNextPlayer()
	{
		// Arrange
		var players = CreateMockPlayers(3);
		var currentPlayer = players[0];
		var sut = new TurnOrder(PassDirection.CLOCKWISE);

		// Act
		var result = sut.GetNextPlayer(players, currentPlayer);

		// Assert
		Assert.Equal(players[1], result);
	}

	[Fact]
	public void GetNextPlayer_ClockwiseDirection_WrapsAroundToFirst()
	{
		// Arrange
		var players = CreateMockPlayers(3);
		var currentPlayer = players[2];
		var sut = new TurnOrder(PassDirection.CLOCKWISE);

		// Act
		var result = sut.GetNextPlayer(players, currentPlayer);

		// Assert
		Assert.Equal(players[0], result);
	}

	[Fact]
	public void GetNextPlayer_CounterClockwiseDirection_ReturnsPreviousPlayer()
	{
		// Arrange
		var players = CreateMockPlayers(3);
		var currentPlayer = players[1];
		var turnOrder = new TurnOrder(PassDirection.COUNTERCLOCKWISE);

		// Act
		var result = turnOrder.GetNextPlayer(players, currentPlayer);

		// Assert
		Assert.Equal(players[0], result);
	}

	[Fact]
	public void GetNextPlayer_CounterClockwiseDirection_WrapsToLast()
	{
		// Arrange
		var players = CreateMockPlayers(3);
		var currentPlayer = players[0];
		var turnOrder = new TurnOrder(PassDirection.COUNTERCLOCKWISE);

		// Act
		var result = turnOrder.GetNextPlayer(players, currentPlayer);

		// Assert
		Assert.Equal(players[2], result);
	}

	[Theory]
	[InlineData(PassDirection.CLOCKWISE)]
	[InlineData(PassDirection.COUNTERCLOCKWISE)]
	public void GetPassDirection_ReturnsGivenDirection(PassDirection direction)
	{
		// Arrange
		var turnOrder = new TurnOrder(direction);

		// Act
		var result = turnOrder.GetPassDirection();

		// Assert
		Assert.Equal(direction, result);
	}
}


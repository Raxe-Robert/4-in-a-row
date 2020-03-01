using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using WPFUI.Commands;
using WPFUI.Models;

namespace WPFUI.ViewModels
{
	internal class GameViewModel : INotifyPropertyChanged
	{
		private Game _game;
		public Game Game
		{
			get { return _game; }
			private set
			{
				if (_game == value)
					return;

				_game = value;
				OnPropertyChanged();
			}
		}

		public GameRestartCommand RestartCommand { get; private set; }

		public GameViewModel()
		{
			Game = new Game();

			RestartCommand = new GameRestartCommand(this);
		}

		/// <summary>
		/// Perform a move for the currently active player
		/// </summary>
		/// <param name="column">Index between 0 (inclusive) and <see cref="COLUMNS"/>(exlusive)</param>
		public void DoMove(int column)
		{
			// Find an empty row for the given column
			int row = Game.Grid.GetLength(1) - 1;
			for (; row >= 0; row--)
			{
				if (Game.Grid[column, row] == 0)
					break;
			}

			// Row is full
			if (row < 0)
				return;

			ChipViewModel viewModel = new ChipViewModel();
			var chip = viewModel.Chip;
			chip.Row = row;
			chip.Column = column;
			chip.Player = Game.CurrentPlayer;

			Game.Grid[column, row] = Game.CurrentPlayer;
			Game.Chips.Add(viewModel);

			if (CheckWinner())
				Debug.Print($"Player {Game.CurrentPlayer} has won at turn {Game.Turn}!");

			Game.Turn++;
		}

		bool CheckWinner()
		{
			var grid = Game.Grid;
			var columns = grid.GetLength(0);
			var rows = grid.GetLength(1);

			var lastChip = Game.Chips[^1].Chip;

			var xMin = Math.Clamp(lastChip.Column - 3, 0, columns - 1);
			var xMax = Math.Clamp(lastChip.Column + 3, 0, columns - 1);
			var yMin = Math.Clamp(lastChip.Row - 3, 0, rows - 1);
			var yMax = Math.Clamp(lastChip.Row + 3, 0, rows - 1);

			var counter = 0;

			{
				// Horizontal
				for (int x = xMin; x <= xMax; x++)
				{
					var player = grid[x, lastChip.Row];

					counter = (player == lastChip.Player) ? counter + 1 : 0;
					if (counter == 4)
						return true;
				}
			}

			counter = 0;

			{
				// Vertical
				for (int y = yMin; y <= yMax; y++)
				{
					var player = grid[lastChip.Column, y];

					counter = (player == lastChip.Player) ? counter + 1 : 0;
					if (counter == 4)
						return true;
				}
			}

			counter = 0;

			{
				// Diagonal: top left (-1, -1) to bottom right (1, 1)
				var topLeft = Math.Max(xMin - lastChip.Column, yMin - lastChip.Row);
				var bottomRight = Math.Min(xMax - lastChip.Column, yMax - lastChip.Row);

				for (int i = topLeft; i <= bottomRight; i++)
				{
					var x = lastChip.Column + i;
					var y = lastChip.Row + i;

					var player = grid[x, y];

					counter = (player == lastChip.Player) ? counter + 1 : 0;
					if (counter == 4)
						return true;
				}
			}

			counter = 0;

			{
				// Diagonal: bottom left (1, -1) to top right (-1, 1)
				var bottomLeft = Math.Max(xMin - lastChip.Column, lastChip.Row - yMax);
				var topRight = Math.Min(xMax - lastChip.Column, lastChip.Row - yMin);

				for (int i = bottomLeft; i <= topRight; i++)
				{
					var x = lastChip.Column + i;
					var y = lastChip.Row - i;

					var player = grid[x, y];

					counter = (player == lastChip.Player) ? counter + 1 : 0;
					if (counter == 4)
						return true;
				}
			}

			return false;
		}

		public void Restart()
		{
			Game = new Game();
		}

		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}

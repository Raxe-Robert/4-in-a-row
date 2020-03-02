using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using WPFUI.Commands;
using WPFUI.Models;

namespace WPFUI.ViewModels
{
	internal class GameViewModel : BaseViewModel
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

		public ObservableCollection<ChipViewModel> Chips { get; private set; }
		private ChipViewModel _previewChipViewModel;

		public ICommand RestartCommand { get; private set; }

		public GameViewModel()
		{
			Game = new Game();
			Chips = new ObservableCollection<ChipViewModel>();

			RestartCommand = new GameRestartCommand(this);
		}

		public void UpdateMovePreview(int column)
		{
			if (Game.Finished)
				return;

			var row = GetEmptyRowIndex(column);
			if (row == -1)
				return;

			if (_previewChipViewModel == null)
			{
				_previewChipViewModel = new ChipViewModel();
				Chips.Add(_previewChipViewModel);
			}

			var previewChip = _previewChipViewModel.Chip;
			previewChip.Column = column;
			previewChip.Row = row;
			previewChip.Player = Game.CurrentPlayer;
		}

		/// <summary>
		/// Perform a move for the current player
		/// </summary>
		/// <param name="column">Index between 0 (inclusive) and <see cref="Game.COLUMNS"/>(exlusive)</param>
		public void DoMove(int column)
		{
			if (Game.Finished || _previewChipViewModel == null)
				return;

			var row = GetEmptyRowIndex(column);
			if (row == -1)
				return;

			Game.Board[column, row] = Game.CurrentPlayer;

			var chip = _previewChipViewModel.Chip;
			chip.Row = row;
			chip.Column = column;
			chip.Player = Game.CurrentPlayer;

			// Clear the preview chip and force a refresh of our chips collection
			_previewChipViewModel = null;
			Chips.Move(Chips.Count - 1, Chips.Count - 1);

			if (HasCurrentPlayerWon())
			{
				Game.Finished = true;
				return;
			}

			Game.Turn++;
			UpdateMovePreview(column);
		}

		public void Restart()
		{
			Game = new Game();
			Chips.Clear();
		}

		/// <summary>
		/// Checks if the board contains an empty row for the given column
		/// </summary>
		/// <param name="column"></param>
		/// <returns>Returns -1 if no row was found</returns>
		private int GetEmptyRowIndex(int column)
		{
			int row = Game.Board.GetLength(1) - 1;
			for (; row >= 0; row--)
			{
				if (Game.Board[column, row] == 0)
					return row;
			}
			
			return -1;
		}

		/// <summary>
		/// Checks if the last move resulted in a player winning the game
		/// </summary>
		/// <remarks>Can not be used to check the whole board.</remarks>
		private bool HasCurrentPlayerWon()
		{
			var grid = Game.Board;
			var columns = grid.GetLength(0);
			var rows = grid.GetLength(1);

			var lastChip = Chips[^1].Chip;

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
	}
}

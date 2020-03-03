using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WPFUI.Commands;
using WPFUI.Common;
using WPFUI.Models;

namespace WPFUI.ViewModels
{
	internal class GameViewModel : ObservableObject
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

		public enum GameModes { Singleplayer, Multiplayer}
		public GameModes GameMode { get; private set; }

		private bool IsCurrentPlayerAI => GameMode == GameModes.Singleplayer && Game.CurrentPlayer == 2;

		public ObservableCollection<ChipViewModel> Chips { get; private set; }

		private ChipViewModel _preview;
		public ChipViewModel Preview
		{ 
			get { return _preview; }
			set
			{
				if (_preview == value)
					return;

				_preview = value;
				OnPropertyChanged();
			}
		}

		public ICommand RestartCommand { get; private set; }

		public GameViewModel()
		{
			Game = new Game();
			Chips = new ObservableCollection<ChipViewModel>();

			RestartCommand = new GameRestartCommand(this);

			var selectedGameMode = Application.Current.Properties["GameMode"];
			GameMode = selectedGameMode switch
			{
				"Singleplayer" => GameModes.Singleplayer,
				"Multiplayer" => GameModes.Multiplayer,
				_ => GameModes.Singleplayer
			};
		}

		public void UpdateMovePreview(int column)
		{
			if (Game.Finished || IsCurrentPlayerAI)
				return;

			var row = GetEmptyRowIndex(column);
			if (row == -1)
				return;

			if (Preview == null)
			{
				Preview = new ChipViewModel();
				Chips.Add(Preview);
			}

			var previewChip = Preview.Chip;
			previewChip.Column = column;
			previewChip.Row = row;
			previewChip.Player = Game.CurrentPlayer;
		}

		/// <summary>
		/// Perform a move for the current player
		/// </summary>
		/// <param name="column">Index between 0 (inclusive) and <see cref="Game.COLUMNS"/>(exlusive)</param>
		public void DoPlayerMove(int column)
		{
			if (!IsCurrentPlayerAI)
				DoMove(column);
		}

		/// <summary>
		/// Perform a move for the <see cref="Game.CurrentPlayer"/>
		/// </summary>
		/// <param name="column">Index between 0 (inclusive) and <see cref="Game.COLUMNS"/>(exlusive)</param>
		private void DoMove(int column)
		{
			if (Game.Finished)
				return;

			var row = GetEmptyRowIndex(column);
			if (row == -1)
				return;

			Game.Board[column, row] = Game.CurrentPlayer;

			if (Preview == null)
			{
				Preview = new ChipViewModel();
				Chips.Add(Preview);
			}

			var chip = Preview.Chip;
			chip.Row = row;
			chip.Column = column;
			chip.Player = Game.CurrentPlayer;

			if (HasCurrentPlayerWon())
			{
				Game.Finished = true;
				return;
			}

			Preview = null;
			Game.Turn++;

			// Immediately perform the move for the AI
			if (IsCurrentPlayerAI)
			{
				// Determine best move
				var rand = new System.Random();
				var col = rand.Next(0, Game.COLUMNS);

				DoMove(col);
			}
		}

		public void Restart()
		{
			Game = new Game();
			Chips.Clear();
			Preview = null;
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

using System.ComponentModel;
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
			int row = Game.ChipsArr.GetLength(0) - 1;
			for (; row >= 0; row--)
			{
				if (Game.ChipsArr[row, column] == null)
					break;
			}

			// Row is full
			if (row < 0)
				return;

			ChipViewModel viewModel = new ChipViewModel();
			var chip = viewModel.Chip;
			chip.Row = row;
			chip.Column = column;
			chip.Color = Game.CurrentPlayer == 1 ? "Red" : "Yellow";

			Game.ChipsArr[row, column] = viewModel;
			Game.Chips.Add(viewModel);

			CheckWinner();

			Game.Turn++;
			Game.CurrentPlayer = Game.CurrentPlayer == 1 ? 2 : 1;
		}

		bool CheckWinner()
		{
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

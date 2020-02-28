using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace WPFUI
{
	public class Player1ViewModel : PlayerViewModel
	{

	}

	public class Player2ViewModel : PlayerViewModel
	{

	}

	public abstract class PlayerViewModel
	{
		public int Row { get; set; }
		public int Column { get; set; }
	}

	public class Game
	{
		public const int ROWS = 6;
		public const int COLUMNS = 7;

		public ObservableCollection<PlayerViewModel> Moves
		{
			get;
			set;
		}

		public int CurrentPlayer { get; set; }

		public Game()
		{
			CurrentPlayer = 1;

			Moves = new ObservableCollection<PlayerViewModel>();
		}

		/// <summary>
		/// Perform a move for the currently active player
		/// </summary>
		/// <param name="column">Index between 0 (inclusive) and <see cref="COLUMNS"/>(exlusive)</param>
		public void DoMove(int column)
		{
			Debug.Print("MOVE: " + column);

			// We start at the bottom row 
			int row = ROWS - 1;

			for (int i = 0; i < Moves.Count; i++)
			{
				var move = Moves[i];

				if (move.Column == column)
					row--;
			}

			PlayerViewModel viewModel;

			if (CurrentPlayer == 1)
				viewModel = new Player1ViewModel() { Row = row, Column = column };
			else
				viewModel = new Player2ViewModel() { Row = row, Column = column };

			Moves.Add(viewModel);

			CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
		}
	}
}

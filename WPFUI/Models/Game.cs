using WPFUI.Common;

namespace WPFUI.Models
{
	internal class Game : ObservableObject
	{
		public const int ROWS = 6;
		public const int COLUMNS = 7;

		public enum PlayerType { Human, Computer }
		public enum Player
		{
			None = 0,
			Red = 1,
			Yellow = 2,
		}
		public Player[,] Board { get; set; }

		private int _turn;
		public int Turn
		{
			get { return _turn;  }
			set
			{
				if (_turn == value)
					return;

				_turn = value;
				OnPropertyChanged();
				OnPropertyChanged("CurrentPlayer");
			}
		}

		private bool _finished;
		public bool Finished
		{ 
			get { return _finished; }
			set
			{
				if (_finished == value)
					return;

				_finished = value;
				OnPropertyChanged();
			}
		}


		public Player CurrentPlayer => Turn % 2 == 0 ? Player.Yellow : Player.Red;

		public Game()
		{
			Board = new Player[COLUMNS, ROWS];

			Turn = 1;
			Finished = false;
		}
	}
}

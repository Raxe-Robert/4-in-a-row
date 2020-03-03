using WPFUI.Common;

namespace WPFUI.Models
{
	internal class Game : ObservableObject
	{
		public const int ROWS = 6;
		public const int COLUMNS = 7;

		public int[,] Board { get; set; }

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


		public int CurrentPlayer => Turn % 2 == 0 ? 2 : 1;

		public Game()
		{
			Board = new int[COLUMNS, ROWS];

			Turn = 1;
			Finished = false;
		}
	}
}

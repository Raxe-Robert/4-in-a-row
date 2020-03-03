using WPFUI.Common;

namespace WPFUI.Models
{
	internal class Chip : ObservableObject
	{
		private int _row;
		public int Row
		{
			get { return _row; }
			set
			{
				if (_row == value)
					return;

				_row = value;
				OnPropertyChanged();
			}
		}

		private int _column;
		public int Column
		{
			get { return _column; }
			set
			{
				if (_column == value)
					return;

				_column = value;
				OnPropertyChanged();
			}
		}

		private Game.Player _player;
		public Game.Player Player
		{
			get { return _player; }
			set
			{
				if (_player == value)
					return;

				_player = value;
				OnPropertyChanged();
			}
		}
	}
}

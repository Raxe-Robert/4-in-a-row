using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFUI.Models
{
	internal class Game : INotifyPropertyChanged
	{
		public const int ROWS = 6;
		public const int COLUMNS = 7;

		public ObservableCollection<ChipViewModel> Chips { get; set; }
		public int[,] Grid { get; set; }

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
		public int CurrentPlayer => Turn % 2 == 0 ? 2 : 1;

		public Game()
		{
			Chips = new ObservableCollection<ChipViewModel>();
			Grid = new int[COLUMNS, ROWS];

			Turn = 1;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

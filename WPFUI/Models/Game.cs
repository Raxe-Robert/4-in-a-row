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
		public ChipViewModel[,] ChipsArr { get; set; }

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
			}
		}

		private int _currentPlayer;
		public int CurrentPlayer
		{
			get { return _currentPlayer; }
			set
			{
				if (_currentPlayer == value)
					return;

				_currentPlayer = value;
				OnPropertyChanged();
			}
		}

		public Game()
		{
			Chips = new ObservableCollection<ChipViewModel>();
			ChipsArr = new ChipViewModel[ROWS, COLUMNS];

			Turn = 1;
			CurrentPlayer = 1;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

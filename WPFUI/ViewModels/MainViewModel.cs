using System.Windows.Input;
using WPFUI.Commands;
using WPFUI.Common;

namespace WPFUI.ViewModels
{
	internal class MainViewModel : ObservableObject
	{
		private ObservableObject _activeViewModel;
		public ObservableObject ActiveViewModel 
		{ 
			get { return _activeViewModel; }
			set
			{
				_activeViewModel = value;
				OnPropertyChanged();
			}
		}

		public ICommand NavigateViewCommand { get; set; }

		public MainViewModel()
		{
			ActiveViewModel = new MenuViewModel();

			NavigateViewCommand = new NavigateViewCommand(this);
		}
	}
}

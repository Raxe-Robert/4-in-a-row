using System.Windows.Input;
using WPFUI.Commands;

namespace WPFUI.ViewModels
{
	internal class MainViewModel : BaseViewModel
	{
		private BaseViewModel _activeViewModel;
		public BaseViewModel ActiveViewModel 
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

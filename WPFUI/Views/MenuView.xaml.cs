using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFUI.Extensions;
using WPFUI.ViewModels;

namespace WPFUI.Views
{
	/// <summary>
	/// Interaction logic for MenuView.xaml
	/// </summary>
	public partial class MenuView : UserControl
	{
		public MenuView()
		{
			InitializeComponent();
		}

		private void SingleplayerButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Application.Current.Properties["GameMode"] = "Singeplayer";

			NavigateToGame();
		}

		private void MultiplayerButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Properties["GameMode"] = "Multiplayer";

			NavigateToGame();
		}

		private void NavigateToGame()
		{
			var mainWindow = VisualTreeHelperExtensions.FindParent<MainWindow>(this);
			if (mainWindow == null)
				return;

			var mainViewModel = (MainViewModel)mainWindow.DataContext;
			mainViewModel.NavigateViewCommand.Execute("Game");
		}
	}

}

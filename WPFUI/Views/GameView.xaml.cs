using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFUI.Models;
using WPFUI.ViewModels;

namespace WPFUI.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class GameView : UserControl
	{
		public GameView()
		{
			InitializeComponent();

			DataContext = new GameViewModel();
		}

		private void GameGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var element = (FrameworkElement)sender;

			// Determine which column has been clicked
			var clickPos = e.GetPosition(element);
			var gridWidth = element.ActualWidth;
			var xPosClickPercentage = clickPos.X / gridWidth;
			var col = (int)(Game.COLUMNS * xPosClickPercentage);

			var viewModel = (GameViewModel)DataContext;
			viewModel.DoMove(col);
		}
	}
}

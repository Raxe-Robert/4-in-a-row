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
			var column = GetColumn(element, e);

			var viewModel = (GameViewModel)DataContext;
			viewModel.DoMove(column);
		}

		private void GameGrid_MouseMove(object sender, MouseEventArgs e)
		{
			var element = (FrameworkElement)sender;
			var column = GetColumn(element, e);

			var viewModel = (GameViewModel)DataContext;
			viewModel.UpdateMovePreview(column);
		}

		private int GetColumn(FrameworkElement el, MouseEventArgs mousePoint)
		{
			var clickPos = mousePoint.GetPosition(el);
			var gridWidth = el.ActualWidth;
			var xPercentage = clickPos.X / gridWidth;
			
			return (int)(Game.COLUMNS * xPercentage);
		}
	}
}

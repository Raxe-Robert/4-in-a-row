using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public Game Game { get; private set; }

		public MainWindow()
		{
			InitializeComponent();

			Game = new Game();

			DataContext = this;
		}

		private void Column_Click(object sender, MouseButtonEventArgs e)
		{
			var chip = new Ellipse
			{
				Fill = Brushes.Red,
				Width = 20,
				Height = 20
			};

			RegisterName(chip.Name, chip);

			Canvas.SetLeft(chip, 20d);
			Canvas.SetTop(chip, 20d);
		}

		private void GameGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			// Determine which column has been clicked
			var clickPos = e.GetPosition(GameGrid);
			var width = GameGrid.ActualWidth;
			var percentage = clickPos.X / width;
			var col = (int)(Game.COLUMNS * percentage);

			Game.DoMove(col);
		}
	}
}

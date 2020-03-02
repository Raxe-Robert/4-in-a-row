using System.Windows;
using WPFUI.ViewModels;

namespace WPFUI.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			DataContext = new MainViewModel();
		}
	}
}

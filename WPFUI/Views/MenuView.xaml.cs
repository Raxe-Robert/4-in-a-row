using System.Windows.Controls;
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

			DataContext = new MenuViewModel();
		}
	}
}

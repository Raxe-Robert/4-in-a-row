using WPFUI.Common;
using WPFUI.Models;

namespace WPFUI.ViewModels
{
	internal class ChipViewModel : ObservableObject
	{
		public Chip Chip { get; private set; }

		public ChipViewModel()
		{
			Chip = new Chip();
		}
	}
}

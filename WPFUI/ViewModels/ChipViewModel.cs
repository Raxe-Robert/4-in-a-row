using WPFUI.Models;

namespace WPFUI.ViewModels
{
	internal class ChipViewModel : BaseViewModel
	{
		public Chip Chip { get; private set; }

		public ChipViewModel()
		{
			Chip = new Chip();
		}
	}
}

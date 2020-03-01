using WPFUI.Models;

namespace WPFUI
{
	internal class ChipViewModel
	{
		public Chip Chip { get; private set; }

		public ChipViewModel()
		{
			Chip = new Chip();
		}
	}
}

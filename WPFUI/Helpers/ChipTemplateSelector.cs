using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using WPFUI.Models;
using WPFUI.ViewModels;

namespace WPFUI.Helpers
{
	public class ChipTemplateSelector : DataTemplateSelector
	{
		public DataTemplate Player1Template { get; set; }
		public DataTemplate Player2Template { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			var selectedTemplate = Player1Template;

			if (!(item is ChipViewModel viewModel))
				return selectedTemplate;

			switch (viewModel.Chip.Player)
			{
				case Game.Player.Red:
					selectedTemplate = Player1Template;
					break;
				case Game.Player.Yellow:
					selectedTemplate = Player2Template;
					break;
			}

			return selectedTemplate;
		}
	}
}

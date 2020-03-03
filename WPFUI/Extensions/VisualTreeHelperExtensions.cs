using System.Windows;
using System.Windows.Media;

namespace WPFUI.Extensions
{
	public static class VisualTreeHelperExtensions
	{
		public static T FindParent<T>(DependencyObject child) where T : DependencyObject
		{
			var parentObject = VisualTreeHelper.GetParent(child);
			if (parentObject == null)
				return null;

			if (parentObject is T parent)
				return parent;
			else
				return FindParent<T>(parentObject);
		}
	}
}

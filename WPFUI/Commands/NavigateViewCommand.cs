using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WPFUI.ViewModels;
using WPFUI.Views;

namespace WPFUI.Commands
{
	internal class NavigateViewCommand : ICommand
	{
		private readonly MainViewModel _viewModel;

		public NavigateViewCommand(MainViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		event EventHandler ICommand.CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		bool ICommand.CanExecute(object parameter)
		{
			return _viewModel != null;
		}

		void ICommand.Execute(object parameter)
		{
			var path = (string)parameter;

			switch (path)
			{
				case "Menu":
					_viewModel.ActiveViewModel = new MenuViewModel();
					break;
				case "Game":
					_viewModel.ActiveViewModel = new GameViewModel();
					break;
			}
		}
	}
}

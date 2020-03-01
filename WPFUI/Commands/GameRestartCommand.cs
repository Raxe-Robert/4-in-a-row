using System;
using System.Windows.Input;
using WPFUI.ViewModels;

namespace WPFUI.Commands
{
	internal class GameRestartCommand : ICommand
	{
		private GameViewModel _viewModel;

		public GameRestartCommand(GameViewModel viewModel)
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
			return true;
		}

		void ICommand.Execute(object parameter)
		{
			_viewModel.Restart();
		}
	}
}

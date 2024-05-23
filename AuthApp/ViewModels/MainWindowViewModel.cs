using AuthApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AuthApp.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private object currentView;

        [ObservableProperty]
        private object authView;

        public ObservableCollection<CommandViewModelPair> SectionCommands { get; }

        public MainWindowViewModel(AuthViewModel authViewModel, View1ViewModel view1ViewModel, View2ViewModel view2ViewModel)
        {
            AuthView = authViewModel;

            SectionCommands = new ObservableCollection<CommandViewModelPair>();

            AddSectionCommand(nameof(View1), view1ViewModel);
            AddSectionCommand(nameof(View2), view2ViewModel);

            CurrentView = SectionCommands.FirstOrDefault()?.ViewModel;
        }

        public void AddSectionCommand(string sectionName, object viewModel)
        {
            var command = new RelayCommand(() => ShowView(viewModel));
            SectionCommands.Add(new CommandViewModelPair(sectionName, command, viewModel));
        }

        private void ShowView(object viewModel)
        {
            CurrentView = viewModel;
        }
    }

    public class CommandViewModelPair
    {
        public string SectionName { get; }
        public ICommand Command { get; }
        public object ViewModel { get; }

        public CommandViewModelPair(string sectionName, ICommand command, object viewModel)
        {
            SectionName = sectionName;
            Command = command;
            ViewModel = viewModel;
        }
    }
}

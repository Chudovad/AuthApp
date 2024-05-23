using AuthApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AuthApp.ViewModels
{
    public partial class View1ViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isAuthorized;

        [ObservableProperty]
        private string visibility;

        [ObservableProperty]
        private User userInfo;

        [ObservableProperty]
        private string textInfo;

        public View1ViewModel()
        {
            UpdateView();
        }

        public void UpdateView()
        {
            if (IsAuthorized)
            {
                Visibility = "Visible";
                TextInfo = "";
            }
            else
            {
                Visibility = "Hidden";
                TextInfo = StaticData.ViewInfoUnauthorize;
            }
              
        }
    }
}

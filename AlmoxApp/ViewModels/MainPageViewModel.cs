using AlmoxApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AlmoxApp.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {
        }

        [RelayCommand]
        public async Task GoMovSaida()
        {
            await Shell.Current.GoToAsync(nameof(MovSaida));
            //await Navigation.PushAsync(new MovSaida());
        }
    }
}

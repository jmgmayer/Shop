namespace Shop.UIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Shop.UIForms.Views;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand => new RelayCommand(Login);

        public LoginViewModel()
        {
            this.Email = "juan.manuel.gutierrezm@pemex.com";
            this.Password = "rufus170608.";
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a Email", "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error","You must enter a Password","Accept");
                return;
            }
            if (!this.Email.Equals("juan.manuel.gutierrezm@pemex.com") || !this.Password.Equals("rufus170608."))
            {
                await Application.Current.MainPage.DisplayAlert("Error","User or password wrong","Accept");
            }

            //await Application.Current.MainPage.DisplayAlert("Ok", "You have logged in to the application", "Accept");

            MainViewModel.GetInstance().Products = new ProductsViewModel();

            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());
        }
    }
}

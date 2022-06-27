using Firebase.Database;
using PoleChudes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PoleChudes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        
        FirebaseClient client;
        public LoginPage()
        {
            InitializeComponent();
            client = new FirebaseClient("https://poledatabase-default-rtdb.firebaseio.com/");
        }

        //вход в поле говна
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var user = (await client.Child("User").OnceAsync<UserModel>()).Where(u => u.Object.Login == login.Text.ToString() && u.Object.Password == pass.Text.ToString()).FirstOrDefault();

            if (user != null)
            {
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
            else
            {
                await DisplayAlert("Его нет", " не всё ок", "ладно");
            }

        }

       

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushModalAsync(new Reg());
        }
    }
}
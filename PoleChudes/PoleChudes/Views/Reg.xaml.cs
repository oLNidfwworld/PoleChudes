using Firebase.Database;
using Firebase.Database.Query;
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
    public partial class Reg : ContentPage
    {
        FirebaseClient client;
        public Reg()
        {
            InitializeComponent();
            client = new FirebaseClient("https://poledatabase-default-rtdb.firebaseio.com/");
        }



        public async Task<bool> IsUserExists(string login)
        {
            var user = (await client.Child("Users").OnceAsync<UserModel>()).Where(u => u.Object.Login == login).FirstOrDefault();
            return (user != null);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (await IsUserExists(login.Text.ToString() ))
            {
                await Shell.Current.DisplayAlert("Внимание", "Такой пользователь уже есть в бд!", "OK");
            }
            else
            {
               
                    await client.Child("User").PostAsync(new UserModel()
                    {
                        
                        Login = login.Text.ToString(),
                        Password = pass.Text.ToString()
                       
                    });
                await Shell.Current.DisplayAlert("Успешно", "Пользователь зарега", "OK");
            }
        }
    }
}
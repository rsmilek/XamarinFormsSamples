using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IDCardValidator.Pages;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace IDCardValidator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Nejprve musíme aplikaci říct, jakou stránku má spustit jako první. Nastavením vlastnosti MainPage.
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

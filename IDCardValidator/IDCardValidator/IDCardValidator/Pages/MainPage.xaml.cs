using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDCardValidator.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Definovat různé chování na jednotlivých platformách lze nejen v XAML, ale i v kódu například pro úpravu vlastnosti Title. 
            // Ta se totiž na Windows Phone zobrazuje trochu jinak než na ostatní platformách.
            //  if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows) // Just updated to Xamarin.Forms 2.3.4 and above
            if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.WPF)
            {
                this.Title = "  " + this.Title.ToLower();
            }

        }

        private /* async */ void SendButton_Click(object sender, EventArgs e)
        {
            //var manager = new IDValidityManager();
            //var result = await manager.CheckIDValidity(this.IDNumber.Text);
            //switch (result)
            //{
            //    case IDValidityManager.Validity.Unknown:
            //        await DisplayAlert("Platnost občanky", "Platnost dokladu se nepodařilo ověřit!", "OK");
            //        break;
            //    case IDValidityManager.Validity.Valid:
            //        await DisplayAlert("Platnost občanky", "Zadaný doklad není evidován v databázi neplatných dokladů", "OK");
            //        break;
            //    case IDValidityManager.Validity.Invalid:
            //        await DisplayAlert("Platnost občanky", "Zadaný doklad je evidován v databázi neplatných dokladů", "OK");
            //        break;
            //}
        }
    }
}

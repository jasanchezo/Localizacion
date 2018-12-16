using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Plugin.Geolocator;

namespace Localizacion
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //INICIALIZAR EL PLUGIN
            InitializePlugin();
        }

        private void InitializePlugin()
        {
            if (!CrossGeolocator.IsSupported) {
                await DisplayAlert("Error", "No puede cargar el plugin", "OK");
                return;
            }
            if (!CrossGeolocator.Current.IsGeolocationAvailable || !CrossGeolocator.Current.IsGeolocationEnabled) {
                await DisplayAlert("Alerta", "Revisa tu GPS", "OK");
                return;
            }
            await CrossGeoLocator.Current.StartListeningAsync(new TimeSpan(0, 0, 2), 1);
            CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
        }

        void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            if (!CrossGeolocator.Current.IsListening){
                return;
            }
            var position = CrossGeolocator.Current.GetPositionAsync();
            lblAlt.Text = position.Result.Altitude.ToString();
            lblLat.Text = position.Result.Latitude.ToString();
            lblLon.Text = position.Result.Longitude.ToString();
        }




    }
}

using ArdeleanDariaLab7.Models;
using Plugin.LocalNotification;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.ApplicationModel;

namespace ArdeleanDariaLab7;

public partial class ShopPage : ContentPage
{
    public ShopPage()
    {
        InitializeComponent();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }

    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        var address = shop.Adress;

        // Geocodăm adresa magazinului
        var locations = await Geocoding.GetLocationsAsync(address);
        var shopLocation = locations?.FirstOrDefault();

        if (shopLocation == null)
        {
            await DisplayAlert("Eroare", "Nu s-a putut găsi locația magazinului.", "OK");
            return;
        }

        // Locația utilizatorului
        var myLocation = await Geolocation.GetLocationAsync();

        // Dacă testezi pe Windows în loc de telefon, poți folosi:
        // var myLocation = new Location(46.7731796289, 23.6213886738);

        // Calculăm distanța
        var distance = myLocation.CalculateDistance(shopLocation, DistanceUnits.Kilometers);
        await DisplayAlert("Distanță", $"{distance:F2} km", "OK");

        // Dacă e mai mic de 5 km, trimitem notificare
        if (distance < 5)
        {
            var request = new NotificationRequest
            {
                Title = "Ai de făcut cumpărături în apropiere!",
                Description = address,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                }
            };

            LocalNotificationCenter.Current.Show(request);
        }

        // Deschidem harta
        var options = new MapLaunchOptions
        {
            Name = "Magazinul meu preferat"
        };

        await Map.OpenAsync(shopLocation, options);
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;

        bool answer = await DisplayAlert(
            "Delete Shop",
            "Are you sure you want to delete this shop?",
            "Yes", "No");

        if (answer)
        {
            await App.Database.DeleteShopAsync(shop);
            await Navigation.PopAsync();
        }
    }


}

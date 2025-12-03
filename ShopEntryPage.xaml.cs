using ArdeleanDariaLab7.Models;
namespace ArdeleanDariaLab7;

public partial class ShopEntryPage : ContentPage
{
	public ShopEntryPage()
	{
		InitializeComponent();
	}
    // Metoda apelată de fiecare dată când pagina devine vizibilă
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Încarcă lista de magazine din baza de date și o afișează în ListView
        listView.ItemsSource = await App.Database.GetShopsAsync();
    }

    // Eveniment declanșat la apăsarea butonului "Add Shop" din ToolbarItem
    async void OnShopAddedClicked(object sender, EventArgs e)
    {
        // Navighează la ShopPage, transmițând un obiect Shop nou
        await Navigation.PushAsync(new ShopPage
        {
            BindingContext = new Shop() // Creează un magazin nou
        });
    }

    // Eveniment declanșat la selectarea unui element din ListView
    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            // Navighează la ShopPage, transmițând obiectul Shop selectat pentru editare
            await Navigation.PushAsync(new ShopPage
            {
                BindingContext = e.SelectedItem as Shop
            });

            // Deselectează elementul în ListView
            ((ListView)sender).SelectedItem = null;
        }
    }
}
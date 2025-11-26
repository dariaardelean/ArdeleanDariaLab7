using System;
using ArdeleanDariaLab7.Models;

namespace ArdeleanDariaLab7;


public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;

        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;

        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
        {
            BindingContext = new Product()
        });
    }

    //lab 9 cerinta 15
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var shopl = (ShopList)BindingContext;

        listViewProducts.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }

    //lab 9 cerinta
    async void OnDeleteItemClicked(object sender, EventArgs e)
    {
        var product = listViewProducts.SelectedItem as Product;

        if (product != null)
        {
            var sl = (ShopList)BindingContext;

            // Ștergem produsul din ListProduct folosind ambele ID-uri
            await App.Database.DeleteListProductAsync(product.ID, sl.ID);

            // Reîncarcăm lista de produse
            listViewProducts.ItemsSource = await App.Database.GetListProductsAsync(sl.ID);
        }
    }

}
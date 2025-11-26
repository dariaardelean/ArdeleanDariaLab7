using System;
using ArdeleanDariaLab7.Models;
namespace ArdeleanDariaLab7;

public partial class ProductPage : ContentPage
{
    ShopList sl;
	public ProductPage(ShopList slist)
	{
		InitializeComponent();
        sl = slist;
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var product = (Product)BindingContext;
        await App.Database.SaveProductAsync(product);

        // Reîncarcă lista cu produse
        listView.ItemsSource = await App.Database.GetProductsAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var product = listView.SelectedItem as Product;

        if (product != null)
        {
            await App.Database.DeleteProductAsync(product);

            // Reîncarcă lista cu produse
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Încarcă produsele când deschizi pagina
        listView.ItemsSource = await App.Database.GetProductsAsync();
    }

    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        Product p;

        if (listView.SelectedItem != null)
        {
            p = listView.SelectedItem as Product;

            var lp = new ListProduct()
            {
                ShopListID = sl.ID,
                ProductID = p.ID
            };

            await App.Database.SaveListProductAsync(lp);

            // Legăm lista de produse pentru ORM SQLiteNetExtensions (nu e obligatoriu)
            p.ListProducts = new List<ListProduct> { lp };

            await Navigation.PopAsync();
        }
    }


}
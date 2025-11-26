using System;
using ArdeleanDariaLab7.Data;
using System.IO;

namespace ArdeleanDariaLab7;

public partial class App : Application
{
	static ShoppingListDatabase database;

	public static ShoppingListDatabase Database
	{
		get
		{
			if (database == null)
			{
				database = new
ShoppingListDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
LocalApplicationData), "ShoppingList.db3"));
            }
			return database;
		}
	}
	public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new AppShell());


    }
}

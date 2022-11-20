using Foodwaste.Repositories;
using System.Text;

namespace Foodwaste;

public partial class MainPage : ContentPage
{
    //int count = 0;
    readonly LocalJson<Expiry.Product[]> jsonDB = new LocalJson<Expiry.Product[]>(String.Format("{0}/products.json", FileSystem.AppDataDirectory));

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        var products = Expiry.ExpiryCheck.TempCreateProducts();
        jsonDB.Write((Expiry.Product[])products.ToArray(typeof(Expiry.Product)));
    }

    private async void OnRead(object sender, EventArgs e)
    {
        var products = jsonDB.Read();

        StringBuilder jsonProducts = new StringBuilder();
        foreach (var product in products) { jsonProducts.AppendLine(product.ToString()); }

        await DisplayAlert("", jsonProducts.ToString(), "OK");
    }
}


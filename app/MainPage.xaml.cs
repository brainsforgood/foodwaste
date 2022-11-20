using Foodwaste.ViewModel;

namespace Foodwaste;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new ProductsViewModel();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new Scan());
    }
}


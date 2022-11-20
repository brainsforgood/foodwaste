using Foundation;

namespace Foodwaste;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => FoodwasteProgram.CreateMauiApp();
}

using ZXing.Net.Maui.Controls;
using ZXing.Net.Maui;

namespace Foodwaste;

public partial class Scan : ContentPage
{
    public Scan()
    {
		InitializeComponent();
        camView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.OneDimensional,
            AutoRotate = true,
            Multiple = true,
        };
    }

    private void camView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
		foreach (var barcode in e.Results)
		{
			Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");
		}
    }
}
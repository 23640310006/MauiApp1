namespace MauiApp1;

public partial class RenkSecimiSayfası : ContentPage
{

    private Frame _bottomFrame; // Alt kısımdaki frame'i tutacağız
    public RenkSecimiSayfası()
	{
		InitializeComponent();
        // Alt kısımdaki frame'i bulup değişkene atıyoruz
        _bottomFrame = (Frame)this.FindByName("BottomFrame");
    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        int red = (int)RedSlider.Value;
        int green = (int)GreenSlider.Value;
        int blue = (int)BlueSlider.Value;

        RedLabel.Text = $"Kırmızı Ton: {red:D2}";
        GreenLabel.Text = $"Yeşil Ton: {green:D2}";
        BlueLabel.Text = $"Mavi Ton: {blue:D2}";

        string hexColor = $"#{red:X2}{green:X2}{blue:X2}";
        HexLabel.Text = hexColor;

        // Alt frame'in rengini güncelliyoruz
        _bottomFrame.BackgroundColor = Color.FromArgb(hexColor);
    }

    private async void OnCopyClicked(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(HexLabel.Text);
        await DisplayAlert("Bilgi", "Renk kodu kopyalandı!", "Tamam");
    }

}
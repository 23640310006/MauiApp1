namespace MauiApp1;

public partial class KrediHesaplamaSayfasi : ContentPage
{
	public KrediHesaplamaSayfasi()
	{
		InitializeComponent();
	}

    private void OnTermSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider deðeri deðiþtiðinde Vade Entry'sini güncelle
        TermEntry.Text = Math.Round(e.NewValue).ToString();
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        if (!double.TryParse(InterestEntry.Text, out double oran) ||
            !int.TryParse(TermEntry.Text, out int vade) ||
            !decimal.TryParse(AmountEntry.Text, out decimal tutar))
        {
            DisplayAlert("Hata", "Lütfen geçerli sayýsal deðerler giriniz", "Tamam");
            return;
        }

        oran /= 100; // Yüzdeyi ondalýða çevir (örnek: 1.5 => 0.015)
        double brutFaiz = 0;
        double kkdf = 0;
        double bsmv = 0;

        // Kredi türüne göre vergi oranlarýný belirle
        switch (KrediPicker.SelectedItem as string)
        {
            case "Ýhtiyaç Kredisi":
                kkdf = 0.15;
                bsmv = 0.10;
                break;
            case "Konut Kredisi":
                kkdf = 0;
                bsmv = 0;
                break;
            case "Taþýt Kredisi":
                kkdf = 0.15;
                bsmv = 0.05;
                break;
            case "Ticari Kredi":
                kkdf = 0;
                bsmv = 0.05;
                break;
            default:
                DisplayAlert("Hata", "Lütfen kredi türü seçiniz", "Tamam");
                return;
        }

        // Brüt faiz hesapla
        brutFaiz = oran + (oran * bsmv) + (oran * kkdf);

        // Aylýk taksit hesapla
        double taksit = (Math.Pow(1 + brutFaiz, vade) * brutFaiz) / (Math.Pow(1 + brutFaiz, vade) - 1) * (double)tutar;

        // Toplam ödeme ve faiz
        double toplam = taksit * vade;
        double toplamFaiz = toplam - (double)tutar;

        // Sonuçlarý göster
        MonthlyPaymentLabel.Text = $"{taksit:N2} TL";
        TotalPaymentLabel.Text = $"{toplam:N2} TL";
        TotalInterestLabel.Text = $"{toplamFaiz:N2} TL";
    }


}
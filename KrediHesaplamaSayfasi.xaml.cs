namespace MauiApp1;

public partial class KrediHesaplamaSayfasi : ContentPage
{
	public KrediHesaplamaSayfasi()
	{
		InitializeComponent();
	}

    private void OnTermSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider de�eri de�i�ti�inde Vade Entry'sini g�ncelle
        TermEntry.Text = Math.Round(e.NewValue).ToString();
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        if (!double.TryParse(InterestEntry.Text, out double oran) ||
            !int.TryParse(TermEntry.Text, out int vade) ||
            !decimal.TryParse(AmountEntry.Text, out decimal tutar))
        {
            DisplayAlert("Hata", "L�tfen ge�erli say�sal de�erler giriniz", "Tamam");
            return;
        }

        oran /= 100; // Y�zdeyi ondal��a �evir (�rnek: 1.5 => 0.015)
        double brutFaiz = 0;
        double kkdf = 0;
        double bsmv = 0;

        // Kredi t�r�ne g�re vergi oranlar�n� belirle
        switch (KrediPicker.SelectedItem as string)
        {
            case "�htiya� Kredisi":
                kkdf = 0.15;
                bsmv = 0.10;
                break;
            case "Konut Kredisi":
                kkdf = 0;
                bsmv = 0;
                break;
            case "Ta��t Kredisi":
                kkdf = 0.15;
                bsmv = 0.05;
                break;
            case "Ticari Kredi":
                kkdf = 0;
                bsmv = 0.05;
                break;
            default:
                DisplayAlert("Hata", "L�tfen kredi t�r� se�iniz", "Tamam");
                return;
        }

        // Br�t faiz hesapla
        brutFaiz = oran + (oran * bsmv) + (oran * kkdf);

        // Ayl�k taksit hesapla
        double taksit = (Math.Pow(1 + brutFaiz, vade) * brutFaiz) / (Math.Pow(1 + brutFaiz, vade) - 1) * (double)tutar;

        // Toplam �deme ve faiz
        double toplam = taksit * vade;
        double toplamFaiz = toplam - (double)tutar;

        // Sonu�lar� g�ster
        MonthlyPaymentLabel.Text = $"{taksit:N2} TL";
        TotalPaymentLabel.Text = $"{toplam:N2} TL";
        TotalInterestLabel.Text = $"{toplamFaiz:N2} TL";
    }


}
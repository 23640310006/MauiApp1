namespace MauiApp1;

public partial class VucutKitleEndeksiSayfası : ContentPage
{
	public VucutKitleEndeksiSayfası()
	{
		InitializeComponent();

        // Başlangıç değerlerini ayarla
        WeightEntry.Text = WeightSlider.Value.ToString("F1");
        HeightEntry.Text = HeightSlider.Value.ToString("F2");
    }

    // Kilo Slider Değiştiğinde
    private void OnWeightSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(WeightEntry.Text) &&
            double.TryParse(WeightEntry.Text, out double currentValue) &&
            Math.Abs(currentValue - e.NewValue) < 0.1)
            return;

        WeightEntry.Text = e.NewValue.ToString("F1");
        CalculateBMI();
    }

    // Kilo Entry Değiştiğinde
    private void OnWeightEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue)) return;

        if (double.TryParse(e.NewTextValue, out double value))
        {
            value = Math.Clamp(value, WeightSlider.Minimum, WeightSlider.Maximum);
            if (Math.Abs(WeightSlider.Value - value) > 0.1)
            {
                WeightSlider.Value = value;
            }
        }
        CalculateBMI();
    }

    // Boy Slider Değiştiğinde
    private void OnHeightSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(HeightEntry.Text) &&
            double.TryParse(HeightEntry.Text, out double currentValue) &&
            Math.Abs(currentValue - e.NewValue) < 0.01)
            return;

        HeightEntry.Text = e.NewValue.ToString("F2");
        CalculateBMI();
    }

    // Boy Entry Değiştiğinde
    private void OnHeightEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue)) return;

        if (double.TryParse(e.NewTextValue, out double value))
        {
            value = Math.Clamp(value, HeightSlider.Minimum, HeightSlider.Maximum);
            if (Math.Abs(HeightSlider.Value - value) > 0.01)
            {
                HeightSlider.Value = value;
            }
        }
        CalculateBMI();
    }

    private void CalculateBMI()
    {
        if (double.TryParse(WeightEntry.Text, out double weight) &&
            double.TryParse(HeightEntry.Text, out double height) &&
            height > 0)
        {
            double bmi = weight / (height * height);
            ResultLabel.Text = bmi.ToString("F1");

            StatusLabel.Text = bmi switch
            {
                < 18.5 => "Zayıf",
                < 25 => "Normal",
                < 30 => "Fazla Kilolu",
                _ => "Obez"
            };
        }
    }


}
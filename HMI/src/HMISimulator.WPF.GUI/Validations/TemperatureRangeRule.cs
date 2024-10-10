using System.Globalization;
using System.Windows.Controls;

namespace HMISimulator.WPF.GUI.Validations;

public sealed class TemperatureRangeRule : ValidationRule
{
    public double Min { get; set; }
    public double Max { get; set; }
    
    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        var temperature = 0.0;

        try
        {
            if (((string?) value)?.Length > 0)
            {
                temperature = double.Parse((string) value);
            }
        }
        catch (Exception e)
        {
            return new ValidationResult(false, $"Illegal characters or {e.Message}");
        }
        
        if (temperature < Min || temperature > Max)
        {
            return new ValidationResult(false, $"Please enter a temperature in the range: {Min}-{Max}.");
        }
        
        return ValidationResult.ValidResult;
    }
}
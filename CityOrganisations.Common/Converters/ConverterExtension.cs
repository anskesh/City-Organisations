using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CityOrganisations.Converters
{
    public abstract class ConverterExtension: MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
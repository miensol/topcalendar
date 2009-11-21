using System;
using System.Globalization;
using System.Windows.Data;

namespace TopCalendar.Utility.UI.Converters
{
	[ValueConversion(typeof(DateTime), typeof(string))]
	public class DateTimeToShortTimeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var date = (DateTime) value;
			return date.ToShortTimeString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
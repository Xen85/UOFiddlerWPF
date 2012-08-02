using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenUO.Ultima;

namespace OpenUO_WPF_Fiddler.Converters
{
    [ValueConversion(typeof(Object), typeof(BitmapImage))]
    class TextureConverter : MarkupExtension, IValueConverter
    {
        private static TextureConverter _converter;
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null; 

            if (!(value is int))
                return null;

            if ((int)value < 0)
                return null;

            return MainWindow.FactoryTex.GetTexmap<ImageSource>((int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Overrides of MarkupExtension

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null)
            {
                _converter = new TextureConverter();
                return _converter;
            }
            else
            {
                return _converter;
            }
        }

        #endregion
    }
}

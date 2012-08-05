using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using TilesInfo.Components;

namespace XenToolsGui.Converters
{
    [ValueConversion(typeof(object), typeof(IList<TileStyle>))]
    class ListConverter : MarkupExtension, IValueConverter
    {
        private static ListConverter _converter;
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var category = value as TileCategory;
            if (category == null)
                return null;
            return category.Styles;
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
                _converter = new ListConverter();
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

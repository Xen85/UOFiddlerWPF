using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using TilesInfo.Components;
using TilesInfo.Components.Tiles;

namespace XenToolsGui.Converters
{
    [ValueConversion(typeof(object), typeof(IList<TilesInfo.Components.Tile>))]
    class TileListConverter : MarkupExtension, IValueConverter
    {
        private static TileListConverter _converter;
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var style = value as TileStyle;
            if (style == null)
                return null;
            var tile  = style.Tiles.FirstOrDefault();
            if (tile == null) return tile;
            switch (tile.GetType().Name)
            {
                case "TileRoof":
                    {
                        return style.Tiles.Select(tileRoof => tileRoof as TileRoof).ToList();
                    }
                case "TileMisc":
                    {
                        return style.Tiles.Select(VARIABLE => VARIABLE as TileMisc).ToList();
                    }
                case "TileFloor":
                    {
                        return style.Tiles.Select(VARIABLE => VARIABLE as TileFloor).ToList();
                    }
                default:
                    {
                        return style.Tiles;
                    }

            }
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
                _converter = new TileListConverter();
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

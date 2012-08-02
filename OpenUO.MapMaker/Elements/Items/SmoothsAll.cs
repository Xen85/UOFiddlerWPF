using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OpenUO.MapMaker.Elements.BaseTypes.Base;

namespace OpenUO.MapMaker.Elements.Items
{
    [Serializable]
    [XmlInclude(typeof(ItemID))]
    public class SmoothsAll
    {
        public List<ItemsSmooth.ItemsSmooth> List { get; set; }
 
        public SmoothsAll()
        {
            List = new List<ItemsSmooth.ItemsSmooth>();
        }


        #region Search Methods

        public ItemsSmooth.ItemsSmooth FindFromByColor(Color color)
        {
            return List.FirstOrDefault(smooth => smooth.ColorFrom == color);
        }

        public ItemsSmooth.ItemsSmooth FindToByColor(Color color)
        {
            return List.FirstOrDefault(smooth => smooth.ColorTo == color);
        }

        public IEnumerable<Color> AllColorsFrom()
        {
            return List.Select(Smooth => Smooth.ColorFrom);
        }
        
        public IEnumerable<Color> AllColorsTo()
        {
            return List.Select(smooth => smooth.ColorTo);
        }
        #endregion
    }
}

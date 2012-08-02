using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
using OpenUO.MapMaker.Elements.BaseTypes.Base;
using OpenUO.MapMaker.Elements.Items.ItemCoast;

namespace OpenUO.MapMaker.Elements.Items
{
    [Serializable]
    [XmlInclude(typeof(ItemID))]
    public class CoastsAll
    {
        public List<ItemsCoasts> List { get; set; }

        [NonSerialized] private Dictionary<Color, ItemsCoasts> _coastses;
        public CoastsAll()
        {
            List = new List<ItemsCoasts>();
        }

        #region search methods

        public ItemsCoasts FindGroundByColor(Color color )
        {
            return List.FirstOrDefault(itemsCoastse => color == itemsCoastse.Ground.Color);
        }


        public ItemsCoasts FindCoastByColor(Color color)
        {
            return List.FirstOrDefault(itemsCoastse => color == itemsCoastse.Ground.Color);
        }
        
        public IEnumerable<Color> AllColorsGround()
        {
            return List.Select(items => items.Ground.Color);
        }
        
        public IEnumerable<Color> AllColorsCoast()
        {
            return List.Select(items => items.Coast.Color);
        }

        public ItemsCoasts FindByColor(Color color)
        {
            if(_coastses== null)
            {
                _coastses = new Dictionary<Color, ItemsCoasts>();
                foreach (var itemsCoastse in List)
                {
                    _coastses.Add(itemsCoastse.Ground.Color, itemsCoastse);
                }
            }
            ItemsCoasts c;
            _coastses.TryGetValue(color, out c);
            return c;
        }
        #endregion



    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;

namespace OpenUO.MapMaker.Elements.Items
{
    [Serializable]
    public class ItemsAll
    {
        public List<Items> List { get; set; }
        [NonSerialized] private Dictionary<Color, Items> _items; 
        public ItemsAll()
        {
            List = new List<Items>();
            _items = null;
        }


        #region Search Methods

        

        public Items SearchByColor(Color color)
        {
            if(_items == null)
            {
                _items = new Dictionary<Color, Items>();
                foreach (var itemse in List)
                {
                    _items.Add(itemse.Color,itemse);
                }
            }
            Items i;
            _items.TryGetValue(color, out i);
            return i;
        }

        #endregion
    }
}

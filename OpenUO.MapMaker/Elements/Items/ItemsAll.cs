using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;

namespace OpenUO.MapMaker.Elements.Items
{
    [Serializable]
    public class ItemsAll : IContainerSet
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
            Items i;
            _items.TryGetValue(color, out i);
            return i;
        }

        #endregion

        public void InitializeSeaches()
        {
            _items = new Dictionary<Color, Items>();

            foreach (var itemse in List)
            {
                try
                {
                    _items.Add(itemse.Color, itemse);
                }
                catch(Exception)
                {
                    
                }
            }
        }
    }
}

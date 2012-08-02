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
    public class SmoothsAll : IContainerSet
    {
        public List<ItemsSmooth.ItemsSmooth> List { get; set; }

        [NonSerialized] private Dictionary<Color, ItemsSmooth.ItemsSmooth> _dictionarySmooth;
        [NonSerialized] private Dictionary<Color, bool> _dictionaryColorTo; 

        public SmoothsAll()
        {
            List = new List<ItemsSmooth.ItemsSmooth>();
        }


        #region Search Methods

        public ItemsSmooth.ItemsSmooth FindFromByColor(Color color)
        {
            ItemsSmooth.ItemsSmooth smooth;

            _dictionarySmooth.TryGetValue(color, out smooth);

            return smooth;
        }

        
        public bool ContainsColorTo(Color color)
        {
            bool ret;
            _dictionaryColorTo.TryGetValue(color, out ret);
            return ret;
        }

        #endregion
    
        public void  InitializeSeaches()
        {
            _dictionarySmooth = new Dictionary<Color, ItemsSmooth.ItemsSmooth>();
            _dictionaryColorTo = new Dictionary<Color, bool>();

            foreach (ItemsSmooth.ItemsSmooth itemsSmooth in List)
            {
                try
                {
                    _dictionarySmooth.Add(itemsSmooth.ColorFrom, itemsSmooth);
                }
                catch (Exception)
                {
                }

                try
                {
                    _dictionaryColorTo.Add(itemsSmooth.ColorTo,true);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}

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
    public class CoastsAll : IContainerSet
    {
        public List<ItemsCoasts> List { get; set; }

        [NonSerialized] private Dictionary<Color, ItemsCoasts> _coastses;
        [NonSerialized] private Dictionary<Color, bool> _dictionaryColorCoast;
        
        
        public CoastsAll()
        {
            List = new List<ItemsCoasts>();
        }

        #region search methods

        public ItemsCoasts FindGroundByColor(Color color )
        {
            ItemsCoasts coast = null;

            _coastses.TryGetValue(color, out coast);

            return coast;
        }

        public bool FindCoastByColor(Color color)
        {
            bool ret;
            _dictionaryColorCoast.TryGetValue(color, out ret);

            return ret;
        }
        
        public ItemsCoasts FindByColor(Color color)
        {
            ItemsCoasts c;
            _coastses.TryGetValue(color, out c);
            return c;
        }

        #endregion
        
        public void InitializeSeaches()
        {
            _coastses = new Dictionary<Color, ItemsCoasts>();
            _dictionaryColorCoast = new Dictionary<Color, bool>();

            foreach (var itemsCoastse in List)
            {
                try
                {
                    _coastses.Add(itemsCoastse.Ground.Color, itemsCoastse);

                }
                catch (Exception)
                {
                }

                try
                {
                    _dictionaryColorCoast.Add(itemsCoastse.Coast.Color, true);
                }
                catch (Exception)
                {
                }
            }
        }

      
    }
}

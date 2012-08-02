using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;

namespace OpenUO.MapMaker.Elements.Items.ItemText
{
    [Serializable]
    public class Item
    {
        public ItemID Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Hue { get; set; }

        public Item()
        {
            Id = new ItemID();
            X = 0;
            Y = 0;
            Z = 0;
            Hue = 0;
        }
    }
}

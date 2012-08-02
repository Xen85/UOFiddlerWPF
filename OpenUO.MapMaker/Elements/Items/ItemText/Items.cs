using System;
using System.Collections.Generic;
using System.Drawing;
using OpenUO.MapMaker.Elements.Items.ItemText;

namespace OpenUO.MapMaker.Elements.Items
{
    [Serializable]
    public class Items
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public List<ItemGroup> List { get; set; }
 
        public Items()
        {
            Color = Color.Black;
            List = new List<ItemGroup>();
            Name = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;

namespace OpenUO.MapMaker.Elements.Items.ItemText
{
    [Serializable]
    public class ItemGroup
    {
        public int Percent { get; set; }
        public string Name { get; set; }
        public List<Item> List { get; set; }

        public ItemGroup()
        {
            Percent = 0;
            List = new List<Item>();
            Name = "";
        }
    }
}

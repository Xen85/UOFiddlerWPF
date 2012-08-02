using System;

namespace OpenUO.MapMaker.Elements.Items.ItemCoast
{
    [Serializable]
    public class ItemsCoasts
    {
        public String Name { get; set; }
        public ItemsCoast Coast { get; set; }
        public ItemsCoast Ground { get; set; }
        
        public ItemsCoasts()
        {
            Coast = new ItemsCoast();
            Ground = new ItemsCoast();
            Name = "";
        }
    }
}

using System;
using System.Drawing;
using OpenUO.MapMaker.Elements.BaseTypes.ComplexTypes;

namespace OpenUO.MapMaker.Elements.Items.ItemsSmooth
{
    [Serializable]
    public class ItemsSmooth : Transition
    {
        public Color ColorFrom { get; set; }
        public Color ColorTo { get; set; }

        public ItemsSmooth()
            :base()
        {
            ColorFrom = Color.Black;
            ColorTo = Color.Black;
        }
    }
}

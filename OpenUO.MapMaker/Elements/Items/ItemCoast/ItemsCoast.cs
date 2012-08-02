using System;
using System.Drawing;
using OpenUO.MapMaker.Elements.BaseTypes.Base;
using OpenUO.MapMaker.Elements.BaseTypes.ComplexTypes;

namespace OpenUO.MapMaker.Elements.Items.ItemCoast
{
    [Serializable]
    public class ItemsCoast : Transition
    {
        public Color Color { get; set; }
        public TextureID Texture { get; set; }

        public ItemsCoast()
            :base()
        {
            Color = Color.Black;
            Texture = new TextureID();
        }
    }
}

using System;
using System.Drawing;
using OpenUO.MapMaker.Elements.BaseTypes.ComplexTypes;

namespace OpenUO.MapMaker.Elements.Textures.TextureSmooth
{
    [Serializable]
    public class TextureSmooth: Transition
    {
        public Color ColorFrom { get; set; }
        public Color ColorTo { get; set; }
        public string Name { get; set; }
        public TextureSmooth()
            :base()
        {
            ColorFrom = Color.Black;
            ColorTo = Color.Black;
            Name = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using OpenUO.MapMaker.Elements.BaseTypes.Base;


namespace OpenUO.MapMaker.Elements.Textures.TextureCliff
{
    [Serializable]
    public class TextureCliff
    {
        public CliffDirections Directions { get; set; }
        public List<Color> Colors { get; set; }

        public List<TextureID> List { get; set; }

        public TextureCliff()
        {
            Directions = CliffDirections.EastEnd;
            Colors = new List<Color>();
            List = new List<TextureID>();
        }
    }
}

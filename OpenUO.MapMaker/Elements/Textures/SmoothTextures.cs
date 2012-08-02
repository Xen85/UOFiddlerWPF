using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;

namespace OpenUO.MapMaker.Elements.Textures
{
    [Serializable]
    public class SmoothTextures
    {
        public List<TextureSmooth.TextureSmooth> List { get; set; }
 
        public SmoothTextures()
        {
            List = new List<TextureSmooth.TextureSmooth>();
        }


        #region Search Methods

        public IEnumerable<TextureSmooth.TextureSmooth> FindFromByColor(Color color)
        {
            return List.Where(text => text.ColorFrom == color);
        }

        public IEnumerable<TextureSmooth.TextureSmooth> FindToByColor(Color color)
        {
            return List.Where(text => text.ColorTo == color);
        }

        public IEnumerable<Color> AllColorsFrom()
        {
            return List.Select(txt => txt.ColorFrom);
        }

        public IEnumerable<Color> AllColorsTo()
        {
            return List.Select(txt => txt.ColorTo);
        }
        #endregion
    }
}

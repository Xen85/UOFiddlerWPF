using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OpenUO.MapMaker.Elements.Textures
{
    [Serializable]
    public class Cliffs
    {
        public Color Color { get; set; }

        public List<TextureCliff.TextureCliff> List { get; set; }
 
        public Cliffs()
        {
            Color = Color.White;
            List = new List<TextureCliff.TextureCliff>();
        }

        #region Search Methods
        
        public IEnumerable<TextureCliff.TextureCliff> FindFromByColor(Color color)
        {
            return List.Where(textureCliff => textureCliff.Colors[0] == color);
        }

        public IEnumerable<TextureCliff.TextureCliff> FindToByColor(Color color)
        {
            return List.Where(textureCliff => textureCliff.Colors[0] != color && textureCliff.Colors.Contains(color));
        }

        public IEnumerable<TextureCliff.TextureCliff> FindByColor(Color color)
        {
            return List.Where(textureCliff => textureCliff.Colors.Contains(color));
        }

        public IEnumerable<Color> AllColors()
        {
            return List.SelectMany(textureCliff => textureCliff.Colors);
        }

        #endregion

    }
}

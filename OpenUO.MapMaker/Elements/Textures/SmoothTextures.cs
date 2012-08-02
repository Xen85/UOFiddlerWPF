using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;

namespace OpenUO.MapMaker.Elements.Textures
{
    [Serializable]
    public class SmoothTextures : IContainerSet
    {
        public List<TextureSmooth.TextureSmooth> List { get; set; }
        [NonSerialized] private Dictionary<Color, bool> _dictionaryColorTo;
        [NonSerialized] private Dictionary<Color, bool> _dictionaryColorFrom;
 
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

        public bool ColorFromContains(Color color)
        {
            bool answer;

            _dictionaryColorFrom.TryGetValue(color, out answer);
            return answer;
        }

        public bool Contains(Color color)
        {
            bool answer;

            _dictionaryColorFrom.TryGetValue(color, out answer);
            if (answer)
                return true;

            _dictionaryColorTo.TryGetValue(color, out answer);
            return answer;

        }
        #endregion

        public void InitializeSeaches()
        {
            _dictionaryColorTo  = new Dictionary<Color, bool>();
            _dictionaryColorFrom = new Dictionary<Color, bool>();

            foreach (var textureSmooth in List)
            {
                try
                {
                    _dictionaryColorTo.Add(textureSmooth.ColorTo, true);
                }
                catch (Exception)
                {
                }
                try
                {
                    _dictionaryColorFrom.Add(textureSmooth.ColorFrom,true);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}

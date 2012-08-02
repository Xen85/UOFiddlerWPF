using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;

namespace OpenUO.MapMaker.Elements.Textures
{
    [Serializable]
    public class TextureArea : IContainerSet
    {
        public List<Textures> List { get; set; }
        
        [NonSerialized] private Dictionary<int, Textures> _fast; 
        public TextureArea()
        {
            List = new List<Textures>();
        }

        #region Search Methods

        public Textures FindByIndex(Id id )
        {
            Textures text;
            _fast.TryGetValue(id.Value,out text);
            return text;
        }
        
        #endregion

        public void InitializeSeaches()
        {
            _fast = new Dictionary<int, Textures>();
            foreach (Textures texturese in List)
            {
                _fast.Add(texturese.Index.Value, texturese);
            }
    }
    }
}

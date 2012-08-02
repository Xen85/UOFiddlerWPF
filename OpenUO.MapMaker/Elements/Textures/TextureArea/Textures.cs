using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;

namespace OpenUO.MapMaker.Elements.Textures
{
    [Serializable]
    public class Textures
    {
        public IndexId Index { get; set; }
        public List<TextureID> List { get; set; }
        public String Name { get; set; }
        
        public Textures()
        {
            Index = new IndexId();
            List = new List<TextureID>();
            Name = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;
using OpenUO.MapMaker.Elements.Textures;

namespace OpenUO.MapMaker.TextFileReading.Factories2.Textures
{
    public class FactoryTextureArea : Factory
    {
        public TextureArea Textures { get; set; }

        public FactoryTextureArea(string location) : base(location)
        {
            Textures = new TextureArea();
        }

        public override void Read()
        {

            var texture = new Elements.Textures.Textures();
            int counter = -1;
            foreach (string s in Strings.Where(s => !s.StartsWith("// ")).Where(s => !string.IsNullOrEmpty(s)))
            {
                if(s.Contains("//"))
                {
                    texture.Name = s.Replace("//","");
                    continue;
                }
                if(texture.Index.Value==0)
                {
                    texture.Index.Value = int.Parse(s);
                    continue;
                }
                if(counter == -1)
                {
                    counter = int.Parse(s);
                    continue;
                }
                if(counter > 0)
                {
                    texture.List.Add(new TextureID(){Value = Convert.ToInt32(s,16)});
                    counter--;
                }
                if(counter == 0)
                {
                    counter--;
                    Textures.List.Add(texture);
                    texture = new Elements.Textures.Textures();
                    continue;
                }

            }
        }
    }
}

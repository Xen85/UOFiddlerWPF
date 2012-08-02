using System;
using System.Drawing;
using System.Linq;
using OpenUO.MapMaker.Elements.Textures;
using OpenUO.MapMaker.Elements.Textures.TextureSmooth;

namespace OpenUO.MapMaker.TextFileReading.Factories2.Textures
{
    public class FactoryTextureSmooth : FactoryTransition
    {
        public SmoothTextures Smooth { get; set; } 
        
        public FactoryTextureSmooth(string filelocation) : base(filelocation)
        {
            Smooth = new SmoothTextures();
        }


        public override void Read()
        {
            var smooth = new TextureSmooth();
            var counter4 = 0;
            foreach (string s in Strings.Where(s => !string.IsNullOrEmpty(s) && !s.StartsWith("//")))
            {
               
                if (s.Contains("="))
                {
                    if(smooth.ColorFrom != Color.Black)
                    {
                        Smooth.List.Add(smooth);
                    }
                    smooth.Name = s;
                    smooth = new TextureSmooth();
                    continue;
                }
                var chars = new char[]{'/'};
                var name = s.Split(chars,StringSplitOptions.RemoveEmptyEntries);
                var str = name[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);

                if(str.Length==1 && str[0].StartsWith("0x"))
                {
                    smooth.ColorFrom = ReadColorFromInt(str[0]);
                    continue;
                }
                if (str.Length == 2)
                {
                    smooth.ColorFrom = ReadColorFromInt(str[0]);
                    smooth.ColorTo = ReadColorFromInt(str[1]);
                    continue;
                }

                if(str.Length == 4)
                {
                    TransitionCheck(smooth,str.ToList(),counter4);
                    counter4++;
                    counter4 = counter4%3;
                    continue;
                }
            }
        }
    }
}

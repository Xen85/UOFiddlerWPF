using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;
using OpenUO.MapMaker.Elements.Textures;
using OpenUO.MapMaker.Elements.Textures.TextureCliff;

namespace OpenUO.MapMaker.TextFileReading.Factories2.Textures
{
    public class FactoryCliff : Factory
    {

        public Cliffs Cliffs { get; set; }

        public FactoryCliff(string location) : base(location)
        {
            Cliffs = new Cliffs();
        }


        public override void Read()
        {
            int counter = -1;
            
            foreach (var s in Strings)
            {
                if (s.StartsWith("//"))
                {
                    counter++;
                    continue;
                }
                if (string.IsNullOrWhiteSpace(s)) continue;

                var cliff = new TextureCliff();
                var name = s.Split('/');
                var strings = name[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);

                if (strings.Length == 1)
                {
                    Cliffs.Color = ReadColorFromInt(strings[0]);
                    continue;
                }

                if(strings.Length > 1)
                {
                    foreach (string s1 in strings)
                    {
                        if(s1.Length == 8)
                            cliff.Colors.Add(ReadColorFromInt(s1));
                        else
                        {
                            cliff.List.Add(new TextureID(){Value = Convert.ToInt32(s1,16)});
                        }
                        cliff.Directions = (CliffDirections) counter;

                    }
                }

                Cliffs.List.Add(cliff);
            }


        }
    }
}

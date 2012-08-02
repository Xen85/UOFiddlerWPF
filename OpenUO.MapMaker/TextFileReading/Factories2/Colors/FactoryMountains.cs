using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.ColorArea;
using OpenUO.MapMaker.Elements.ColorArea.Mountains;

namespace OpenUO.MapMaker.TextFileReading.Factories2.Colors
{
    public class FactoryMountains:Factory
    {
        public MountainsAreas Mountains { get; set; }

        public FactoryMountains(string location) : base(location)
        {
            Mountains = new MountainsAreas();
        }

        public override void Read()
        {
            int counter = 0;
            var mountains = new ColorMountains();
            foreach (string s in Strings)
            {
                if(s.StartsWith("//"))
                {
                    mountains.Name = s.Replace("//","");
                    continue;
                }
                if (string.IsNullOrEmpty(s))
                {
                    if(mountains.Color != Color.Black)
                        Mountains.List.Add(mountains);
                    mountains = new ColorMountains();
                    continue;
                }
                char[] chars = {','};
                var str = s.Split(chars,StringSplitOptions.RemoveEmptyEntries);
                if(str.Length == 1)
                {
                    if (mountains.Color == Color.Black)
                        mountains.Color = ReadColorFromInt(s);
                    else
                    {
                        mountains.ColorMountain = ReadColorFromInt(s);
                    }
                    continue;
                }

                if(str.Length == 2)
                {
                    if(mountains.IndexMountainGroup.Value == 0)
                    {
                        mountains.IndexMountainGroup.Value = int.Parse(str[0]);
                        mountains.ModeAutomatic = int.Parse(str[1]) == 1;
                        continue;
                    }
                    if(s.StartsWith("0x"))
                    {
                        mountains.ColorMountain = ReadColorFromInt(str[0]);
                        mountains.IndexGroupTop.Value = int.Parse(str[1]);
                        continue;
                    }
                    var circle = new MountainsCircle();
                    circle.From = int.Parse(str[0]);
                    circle.To = int.Parse(str[1]);
                    mountains.List.Add(circle);
                    continue;
                }
                
            }
            Mountains.List.Add(mountains);
        }
    }
}

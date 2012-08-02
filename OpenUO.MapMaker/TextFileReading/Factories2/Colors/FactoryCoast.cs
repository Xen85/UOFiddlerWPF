using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.ColorArea;
using OpenUO.MapMaker.Elements.ColorArea.Area;

namespace OpenUO.MapMaker.TextFileReading.Factories2.Colors
{
    public class FactoryCoast:Factory
    {
        public  OpenUO.MapMaker.Elements.ColorArea.ColorAreas Area { get; set; }

        public FactoryCoast(string location) : base(location)
        {
           Area = new ColorAreas();
        }

        public override void Read()
        {
            foreach (var area in from s in Strings
                                 where !s.StartsWith("//") && !string.IsNullOrEmpty(s)
                                 select s.Split('/') into name
                                 let strings = name.First().Split(separator, StringSplitOptions.RemoveEmptyEntries)
                                 select new AreaCoast()
                                 {
                                     Color = ReadColorFromInt(Convert.ToInt32(strings[0], 16)),
                                     Name = name.Last(),
                                     Index = { Value = int.Parse(strings[1]) },
                                     Low = int.Parse(strings[2]),
                                     Hight = int.Parse(strings[3])
                                 })
            {
                Area.List.Add(area);
            }
        }
    }
}

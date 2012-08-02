using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.ColorArea;
using OpenUO.MapMaker.Elements.ColorArea.Area;

namespace OpenUO.MapMaker.TextFileReading.Factories2.Colors
{
    public class FactoryColorArea : Factory
    {
        public ColorAreas Areas { get; set; }
        #region ctor
        public FactoryColorArea(string location) : base(location)
        {
            Areas = new ColorAreas();
        }
        #endregion

        public override void Read()
        {
            //foreach (var area in from s in Strings where !s.StartsWith("//") select s.Split('/') into name let strings = name.First().Split(separator, StringSplitOptions.RemoveEmptyEntries) select new Area
            //                                                                                                                                                                                             {
            //                                                                                                                                                                                                 Color = ReadColorFromInt(Convert.ToInt32(strings[0], 16)),
            //                                                                                                                                                                                                 Name = name.Last(),
            //                                                                                                                                                                                                 Index = {Value = int.Parse(strings[1])},
            //                                                                                                                                                                                                 Low = int.Parse(strings[2]),
            //                                                                                                                                                                                                 Hight = int.Parse(strings[3])
            //                                                                                                                                                                                             })
            //{
            //    Areas.List.Add(area);
            //}

            foreach (string s in Strings)
            {
                if (s.StartsWith("//") || string.IsNullOrEmpty(s)) continue;
                var strings = s.Split('/');
                var read = strings[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                var area = new Area();
                area.Color = ReadColorFromInt(read[0]);
                area.Name = strings.Last();
                area.Index.Value = int.Parse(read[1]);
                area.Low = int.Parse(read[2]);
                area.Hight = int.Parse(read[3]);
                Areas.List.Add(area);
            }
        }
    }
}

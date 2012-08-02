using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;
using OpenUO.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using OpenUO.MapMaker.Elements.Items;
using OpenUO.MapMaker.Elements.Items.ItemCoast;

namespace OpenUO.MapMaker.TextFileReading.Factories2.Items
{
    public class FactoryItemCoasts : FactoryTransition
    {
        public CoastsAll CoastsAll { get; set; }


        public FactoryItemCoasts(string location) : base(location)
        {
            CoastsAll = new CoastsAll();
        }

        protected override void SetTransition(int counter, Elements.BaseTypes.ComplexTypes.Transition transition, int j, string s)
        {
            var coast = transition as ItemsCoast;
            if(coast == null)
            {
                int a = 5;
            }
            int value = Convert.ToInt32(s, 16);
            coast.AddElement((LineType)counter,j,new ItemID(){Value =value});
        }



        public override void Read()
        {
            var coastpart = new ItemsCoast();
            var CoastTotal = new ItemsCoasts();
            var counterLengt4 = 0;

            foreach (string s in Strings.Where(s => !string.IsNullOrEmpty(s)).Where(s => !s.StartsWith("//")&& s!="1"))
            {

                if(s.Contains("="))
                {
                    CoastTotal.Name = s;
                    continue;
                }
                var str = s.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                
                if(str.Length==1)
                {
                    if(s.StartsWith("0x") && coastpart.Color == Color.Black)
                    {
                        coastpart.Color = ReadColorFromInt(s);
                        continue;
                    }
                    if(s.StartsWith("0x"))
                    {
                        coastpart.Texture.Value = Convert.ToInt32(s, 16);
                        continue;
                    }
                }

                if(str.Length==4)
                {
                    TransitionCheck(coastpart, str.ToList(), counterLengt4);
                    counterLengt4++;
                    counterLengt4 = counterLengt4%3;
                }

                if(counterLengt4 == 0)
                {
                    if(CoastTotal.Coast.Color == Color.Black)
                    {
                        CoastTotal.Coast = coastpart;
                    }
                    else
                    {
                        CoastTotal.Ground = coastpart;
                        CoastsAll.List.Add(CoastTotal);
                        CoastTotal = new ItemsCoasts();
                    }
                    coastpart = new ItemsCoast();
                    continue;
                }

            }

        }
    }
}

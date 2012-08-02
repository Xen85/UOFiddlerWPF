using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;
using OpenUO.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using OpenUO.MapMaker.Elements.Items;
using OpenUO.MapMaker.Elements.Items.ItemsSmooth;

namespace OpenUO.MapMaker.TextFileReading.Factories2.Items
{
    public class FactorySmoothItems : FactoryTransition
    {
        public SmoothsAll SmoothsAll { get; set; }

        public FactorySmoothItems(string filelocation) : base(filelocation)
        {
            SmoothsAll = new SmoothsAll();
        }

        protected override void SetTransition(int counter, Elements.BaseTypes.ComplexTypes.Transition transition, int j, string s)
        {
            transition.AddElement((LineType)counter,j,new ItemID(){Value = Convert.ToInt32(s,16)});
        }

        public override void Read()
        {
            Color from = Color.Black;
            Color To = Color.Black;
            var items = new ItemsSmooth();
            var counter4 = 0;
            foreach (var strings in from s in Strings where !string.IsNullOrEmpty(s) where s.StartsWith("0x") select s.Split(separator))
            {
                if(strings.Length<4)
                {
                    items.ColorFrom = ReadColorFromInt(strings[0]);
                    if (strings.Length > 1)
                        items.ColorTo = ReadColorFromInt(strings[1]);
                    continue;
                }
                if(strings.Length == 4)
                {
                    TransitionCheck(items,strings.ToList(),counter4);
                    counter4++;
                    counter4 = counter4%3;
                }

                if(counter4 == 0)
                {
                    SmoothsAll.List.Add(items);
                    items = new ItemsSmooth();
                    continue;
                }
            }

        }

    }
}

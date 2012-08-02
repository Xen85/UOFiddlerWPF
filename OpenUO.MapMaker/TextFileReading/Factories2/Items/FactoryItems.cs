using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;
using OpenUO.MapMaker.Elements.Items.ItemText;

namespace OpenUO.MapMaker.TextFileReading.Factories2.Items
{
    public class FactoryItems : Factory
    {
        public Elements.Items.ItemsAll Items { get; set; }
        public FactoryItems(string location) : base(location)
        {
            Items = new Elements.Items.ItemsAll();
        }

        public override void Read()
        {
            var itemgroup = new ItemGroup();
            
            var items = new Elements.Items.Items();
            
            var item = new Item();

            foreach (string s in Strings)
            {
                if (s.StartsWith("// ")) continue;
                
                if(s.StartsWith("//"))
                {
                    items = new Elements.Items.Items();

                    items.Name = s.Replace("//", "");
                    continue;
                }
                
                if(string.IsNullOrEmpty(s)) continue;

                if (s.StartsWith("0x"))
                {
                    if(items.Color!=Color.Black)
                    {
                        Items.List.Add(items);
                    }
                    items.Color=ReadColorFromInt(s);
                    itemgroup = new ItemGroup();
                    Items.List.Add(items);
                    continue;
                }
                itemgroup = new ItemGroup();
                var str = s.Split('/');
                itemgroup.Name = str.Last();
                var s1 = str[0].Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();

                itemgroup.Percent = int.Parse(s1.First());

                s1.Remove(s1.First());
                
                for (int index = 0; index < s1.Count; index++)
                {
                    DivideEtImpera(index,s1,ref item, itemgroup);
                }

                items.List.Add(itemgroup);
            }
        }

        private void DivideEtImpera(int stringCounter, List<String> list, ref Item item, ItemGroup itemGroup)
        {
            switch (stringCounter % 5)
            {
                case 0:
                    {
                        item.Id.Value = Convert.ToInt16(list[stringCounter], 16);
                        break;
                    }
                case 1:
                    {
                        item.Z = sbyte.Parse(list[stringCounter]);
                        break;
                    }
                case 2:
                    {
                        item.Hue = Convert.ToInt16(list[stringCounter], 16);
                        break;
                    }
                case 3:
                    {
                        item.X = int.Parse(list[stringCounter]);
                        break;
                    }
                case 4:
                    {
                        item.Y = int.Parse(list[stringCounter]);
                        itemGroup.List.Add(item);
                        item = new Item();
                        break;
                    }
                default:
                    break;
            }
        }

    }
}

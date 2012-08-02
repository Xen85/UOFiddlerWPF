using System;
using System.Collections.Generic;
using OpenUO.MapMaker.Elements.BaseTypes.Base;

namespace OpenUO.MapMaker.Elements.BaseTypes.ComplexTypes
{
    [Serializable]
    public class ListLine
    {
        public List<ItemList> List { get; set; }
        public ItemList First { get { return List[0]; } set { List[0] = value; }}
        public ItemList Second { get { return List[1]; } set { List[1] = value; } }
        public ItemList Third { get { return List[2]; } set { List[2] = value; } }
        public ItemList Forth { get { return List[3]; } set { List[3] = value; } }
        public ListLine()
        {
            List = new List<ItemList>();

            for (int i = 0; i < 4; i++)
            {
                List.Add(new ItemList());
            }
        }

        public void AddElement(int direction, Id element)
        {
            List[direction].Add(element);
        }
    }
}

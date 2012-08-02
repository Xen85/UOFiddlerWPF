using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;

namespace OpenUO.MapMaker.Elements.BaseTypes.ComplexTypes
{
    [Serializable]
    public class ItemList
    {
        public List<Id> List { get; set; } 
        public ItemList()
        {
            List = new List<Id>();
        }

        public void Add(Id element)
        {
            List.Add(element);
        }
    }
}

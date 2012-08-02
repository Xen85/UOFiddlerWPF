using System;
using System.Collections.Generic;
using System.Drawing;
using OpenUO.MapMaker.Elements.BaseTypes.Base;

namespace OpenUO.MapMaker.Elements.ColorArea.Area
{
    [Serializable]
    public class Area : IEquatable<Area>
    {
        public IndexId Index { get; set; }
        public int Low { get; set; }
        public int Hight { get; set; }
        public String Name { get; set; }
        public Color Color { get; set; }

         

        public Area()
        {
            Color = Color.Black;
            Index = new IndexId();
            Low = 0;
            Hight = 0;
            Name = "";
        }

        #region Implementation of IComparable

        #endregion

        public int CompareTo(Area other)
        {
            if (other.Index == Index)
                return 0;
            else
            {
                return -1;
            }
        }

        public bool Equals(Area other)
        {
            if (other.Index == Index)
                return true;
            if (other.Color == Color)
                return true;
            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

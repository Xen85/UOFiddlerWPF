using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OpenUO.MapMaker.Elements.ColorArea.Area;

namespace OpenUO.MapMaker.Elements.ColorArea
{
    [Serializable]
    [XmlInclude(typeof(AreaCoast))]
    public sealed class ColorAreas
    {
        
        public List<Area.Area> List { get; set; }

        [NonSerialized] private Dictionary<Color, Area.Area> findfastcolor;
        [NonSerialized] private Dictionary<int, Area.Area> findfastid; 
 
        public ColorAreas()
        {
            List = new List<Area.Area>();
            findfastcolor = null;
            findfastid = null;
        }

        public IEnumerable<Color> AllColors()
        {
            return List.Select(area => area.Color);
        }

        public Area.Area FindByColor(Color color)
        {
            if(findfastcolor==null)
            {
                findfastcolor = new Dictionary<Color, Area.Area>();
                foreach (var area in List)
                {
                    try
                    {
                        findfastcolor.Add(area.Color, area);
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                }
            }
            
            Area.Area a;
            findfastcolor.TryGetValue(color, out a);
            return a;
        }
         
        public Area.Area FindByIndex(int index)
        {
            if(findfastid == null)
            {
                findfastid = new Dictionary<int, Area.Area>();
                foreach (var area in List)
                {
                    findfastid.Add(area.Index.Value,area);
                }
            }

            Area.Area a;

            findfastid.TryGetValue(index,out a);
            return a;
        }
    }
}

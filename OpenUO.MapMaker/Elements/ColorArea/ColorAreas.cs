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
    public sealed class ColorAreas : IContainerSet
    {
        
        public List<Area.Area> List { get; set; }

        [NonSerialized] private Dictionary<Color, Area.Area> _findfastcolor;
        [NonSerialized] private Dictionary<int, Area.Area> _findfastid;
        [NonSerialized] private Dictionary<Color, bool> _colordic;   
 
        public ColorAreas()
        {
            List = new List<Area.Area>();
            _findfastcolor = null;
            _findfastid = null;
            _colordic = null;
        }


        public Area.Area FindByColor(Color color)
        {
            Area.Area a;
            _findfastcolor.TryGetValue(color, out a);
            return a;
        }
         
        public Area.Area FindByIndex(int index)
        {
            Area.Area a;
            _findfastid.TryGetValue(index,out a);
            return a;
        }

        public void InitializeSeaches()
        {
            _findfastid = new Dictionary<int, Area.Area>();
            _colordic = new Dictionary<Color, bool>();
            _findfastcolor = new Dictionary<Color, Area.Area>();
            
            foreach (var area in List)
            {
                try
                {
                    _colordic.Add(area.Color, true);
                    _findfastcolor.Add(area.Color, area);
                }
                catch (Exception)
                {
                }
                _findfastid.Add(area.Index.Value, area);
                
            }
        }
    }
}

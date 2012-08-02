using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenUO.MapMaker.Elements.BaseTypes.Base;
using OpenUO.MapMaker.Elements.ColorArea.Mountains;

namespace OpenUO.MapMaker.Elements.ColorArea
{
    [Serializable]
    public class MountainsAreas : IContainerSet
    {
        public List<ColorMountains> List { get; set; }
        [NonSerialized] private Dictionary<Color, ColorMountains> _mountainsDic;
        [NonSerialized] private Dictionary<Id, ColorMountains> _idDictionary;
        [NonSerialized] private Dictionary<Color, bool> _colordic; 
 
        public MountainsAreas()
        {
            List = new List<ColorMountains>();
        }

        #region search methods

        public ColorMountains FindMountainByColor(Color color )
        {
            ColorMountains a;
            _mountainsDic.TryGetValue(color, out a);
            return a;
        }

        public ColorMountains FindMountainById(Id  id)
        {
            ColorMountains a;
            _idDictionary.TryGetValue(id, out a);
            return a;
        }


        public bool Contains(Color color)
        {
            bool a;
            _colordic.TryGetValue(color, out a);
            return a;
        }


        public void InitializeSeaches()
        {
            _colordic = new Dictionary<Color, bool>();
            _idDictionary = new Dictionary<Id, ColorMountains>();
            _mountainsDic = new Dictionary<Color, ColorMountains>();
         
            foreach (ColorMountains colorMountainse in List)
            {
                try
                {
                    _colordic.Add(colorMountainse.Color, true);
                    _mountainsDic.Add(colorMountainse.Color, colorMountainse);
                }
                catch
                {
                }
                _idDictionary.Add(colorMountainse.IndexMountainGroup, colorMountainse);
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenUO.MapMaker.Elements.BaseTypes.Base;
using OpenUO.MapMaker.Elements.ColorArea.Mountains;

namespace OpenUO.MapMaker.Elements.ColorArea
{
    [Serializable]
    public class MountainsAreas
    {
        public List<ColorMountains> List { get; set; }

        public MountainsAreas()
        {
            List = new List<ColorMountains>();
        }

        #region search methods

        public ColorMountains FindMountainByColor(Color color )
        {
            return List.FirstOrDefault(colorMountainse => color == colorMountainse.Color);
        }

        public ColorMountains FindTopMountainByColor(Color color)
        {
            return List.FirstOrDefault(colorMountainse => colorMountainse.ColorMountain == color);
        }

        public ColorMountains FindMountainById(Id  id)
        {
            return List.FirstOrDefault(colorMountainse => colorMountainse.IndexMountainGroup == id);
        }

        public ColorMountains FindTopMountainsById(Id id)
        {
            return List.FirstOrDefault(colormountaise => colormountaise.IndexGroupTop == id);
        }

        public IEnumerable<Color> AllColors()
        {
            return List.Select(colorMountainse => colorMountainse.Color);
        }

        #endregion
    }
}

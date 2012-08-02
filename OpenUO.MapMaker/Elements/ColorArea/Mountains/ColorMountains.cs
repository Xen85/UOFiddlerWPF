using System;
using System.Collections.Generic;
using System.Drawing;
using OpenUO.MapMaker.Elements.BaseTypes.Base;

namespace OpenUO.MapMaker.Elements.ColorArea.Mountains
{
    [Serializable]
    public class ColorMountains
    {
        /// <summary>
        /// Color in the bitmap
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// index of the group in ColorArea
        /// </summary>
        public IndexId IndexMountainGroup { get; set; }
        /// <summary>
        /// Circles of automatic raise
        /// </summary>
        public List<MountainsCircle> List { get; set; }
        /// <summary>
        /// Color of the mountains in the top
        /// </summary>
        public Color ColorMountain { get; set; }
        /// <summary>
        /// Index of what group is drawn in the top
        /// </summary>
        public IndexId IndexGroupTop { get; set; }
        /// <summary>
        /// If the automatic raise is off or on
        /// </summary>
        public bool ModeAutomatic { get; set; }

        public string Name { get; set; }
        
        public ColorMountains()
        {
            List = new List<MountainsCircle>();
            ModeAutomatic = false;
            Color = Color.Black;
            IndexGroupTop = new IndexId();
            ColorMountain = Color.Black;
            IndexMountainGroup = new IndexId();
            Name = "";

        }
    }
}

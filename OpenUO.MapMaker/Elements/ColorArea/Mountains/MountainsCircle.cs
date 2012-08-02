using System;

namespace OpenUO.MapMaker.Elements.ColorArea.Mountains
{
    [Serializable]
    public class MountainsCircle
    {
        public int From { get; set; }
        public int To { get; set; }

        public MountainsCircle()
        {
            From = 0;
            To = 0;
        }
    }
}

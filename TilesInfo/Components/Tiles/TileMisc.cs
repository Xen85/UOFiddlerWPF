using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Type = TilesInfo.Components.Enums.Type;

namespace TilesInfo.Components.Tiles
{
    [Serializable()]
    public class TileMisc : Tile
    {
        public TileMisc()
            :base()
        {
            Type = Type.Misc;
        }
    }
}

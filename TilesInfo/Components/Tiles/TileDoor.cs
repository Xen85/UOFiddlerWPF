using System;
using Type = TilesInfo.Components.Enums.Type;

namespace TilesInfo.Components.Tiles
{
    [Serializable()]
    public class TileDoor : Tile
    {
        public TileDoor()
            :base()
        {
            Type = Type.Doors;
        }
    }
}

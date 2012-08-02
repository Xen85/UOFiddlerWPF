using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TilesInfo.Components.Enums;
namespace TilesInfo.Components.Tiles
{
    [Serializable()]
    public class TileStairs : Tile
    {
        public PositionStairs PositionStair { get; set; }
        public Boolean IsMulti { get; set; }

        public TileStairs()
            :base()
        {
            IsMulti = false;
            PositionStair = PositionStairs.None;
        }

        public TileStairs(Boolean multi, PositionStairs pos)
            :this()
        {
            IsMulti = multi;
            PositionStair = pos;
        }

    }
}

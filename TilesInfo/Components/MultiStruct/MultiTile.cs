using System;

namespace TilesInfo.Components.MultiStruct
{
    [Serializable]
    public class MultiTile
    {
        public Tile Tile;
        public int OffsetX;
        public int OffsetY;
        public int Z;
        public int ID;
        public int Flag;

        public void SetTile(Tile tile)
        {
            Tile = tile;
            ID = tile.Id;
        }
    }
}
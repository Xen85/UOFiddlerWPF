using System;
using TilesInfo.Components.Enums;
using System.Runtime.Serialization;

namespace TilesInfo.Components.Tiles
{
    [Serializable()]
    [DataContract]
    public class TileWall : Tile
    {
        #region Fields
        [DataMember]
        public PositionWalls WallPos { get; set; }
        [DataMember]
        public PositionWallWindow PositionW { get; set; }
        #endregion

        #region ctor
        public TileWall()
        {
            WallPos = PositionWalls.None;
            PositionW = PositionWallWindow.None;
            Type = TilesInfo.Components.Enums.Type.Wall;
        }

        public TileWall(PositionWalls posWal, PositionWallWindow posWin)
            :this()
        {
            WallPos = posWal;
            PositionW = posWin;
        }
        #endregion

        #region props

        [DataMember]
        public override PositionTiles Position
        {
            get { return Pos; }
            set 
            { 
                base.Pos = value;
                WallPos = (PositionWalls)value;
                if(PositionW != PositionWallWindow.None)
                {
                    PositionW = value == PositionTiles.East ? PositionWallWindow.East : PositionWallWindow.South;
                }
            }
        }
        #endregion


        public void ChangePositionWall(int value)
        {
            switch (value)
            {
                case 1: // first south
                    {
                        WallPos = PositionWalls.South;
                        Position = PositionTiles.South;
                        break;
                    }
                case 2:
                    {
                        goto case 1;
                        break;
                    }
                case 3:
                    {
                        goto case 1;
                        break;

                    }
                case 4: //corner
                    { 
                        WallPos = PositionWalls.Corner;
                        Position = PositionTiles.Corner;
                        break;

                    }
                case 5: // first east
                    {
                        WallPos = PositionWalls.East;
                        Position = PositionTiles.East;
                        break;

                    }
                case 6:
                    {
                        goto case 5;
                        break;

                    }
                case 7:
                    {
                        goto case 5;
                        break;

                    }
                case 8: //post
                    {
                        WallPos = PositionWalls.Post;
                        Position = PositionTiles.Post;
                        break;

                    }
                case 9: // window S
                    {
                        PositionW = PositionWallWindow.South;
                        goto case 1;
                        break;

                    }
                case 10:
                    {
                        goto case 9;

                    }
                case 11: //window east
                    {
                        PositionW = PositionWallWindow.East;
                        goto case 4;
                        break;

                    }
                case 12:
                    {
                        goto case 11;
                        break;

                    }
                case 13:
                    {

                        goto case 9;
                    }
                case 14:
                    {
                        goto case 11;
                    }

            }
        }

    }
}

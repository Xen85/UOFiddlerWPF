using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TilesInfo.Components.Enums;
using Type = TilesInfo.Components.Enums.Type;

namespace TilesInfo.Components.Tiles
{
    [Serializable()]
    public class TileFloor : Tile
    {
        public PositionFloors PositionFloor { get; set; }

        public TileFloor()
            :base()
        {
            PositionFloor = PositionFloors.None;
            Type = Type.Floor;
            
        }

        public TileFloor(PositionFloors positionFloors)
            :this()
        {
            PositionFloor = positionFloors;
        }


        public void ChangeFloorPosition(int pos)
        {
            switch (pos)
            {
                case 1:
                    {
                        PositionFloor = PositionFloors.F1;
                        break;
                    }
                case 2:
                    {
                        PositionFloor = PositionFloors.F2;
                        break;
                    }
                case 3:
                    {
                        PositionFloor = PositionFloors.F3;
                        break;
                    }
                case 4:
                    {
                        PositionFloor = PositionFloors.F4;
                        break;
                    }
                case 5:
                    {
                        PositionFloor = PositionFloors.F5;
                        break;
                    }
                case 6:
                    {
                        PositionFloor = PositionFloors.F6;
                        break;
                    }
                case 7:
                    {
                        PositionFloor = PositionFloors.F7;
                        break;
                    }
                case 8:
                    {
                        PositionFloor = PositionFloors.F8;
                        break;
                    }
                case 9:
                    {
                        PositionFloor = PositionFloors.F9;
                        break;
                    }
                case 10:
                    {
                        PositionFloor = PositionFloors.F10;
                        break;
                    }
                case 11:
                    {
                        PositionFloor = PositionFloors.F11;
                        break;
                    }
                case 12:
                    {
                        PositionFloor = PositionFloors.F12;
                        break;
                    }
                case 13:
                    {
                        PositionFloor = PositionFloors.F13;
                        break;
                    }
                case 14:
                    {
                        PositionFloor = PositionFloors.F14;
                        break;
                    }
                case 15:
                    {
                        PositionFloor = PositionFloors.F15;
                        break;
                    }
                case 16:
                    {
                        PositionFloor = PositionFloors.F16;
                        break;
                    }
            }
        }
    }
}

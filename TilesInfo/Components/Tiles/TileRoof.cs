using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TilesInfo.Components.Enums;
using Type = TilesInfo.Components.Enums.Type;

namespace TilesInfo.Components.Tiles
{
    [Serializable()]
    public class TileRoof :Tile
    {
        public PositionRoof PosRoof { get; set; }


        
        public TileRoof()
            :base()
        {
            PosRoof = PositionRoof.None;
            Type = Type.Roofs;
        }

        public TileRoof(PositionRoof pos)
            :this()
        {
            Type = Type.Roofs;
            
            PosRoof = pos;
        }

        

        public void ChangeRoofPosition(int value)
        {
            switch (value)
            {
                case 1:
                    {
                        PosRoof = PositionRoof.North;
                        break;
                    }
                case 2:
                    {
                        PosRoof = PositionRoof.Eeast;
                        break;
                    }
                case 3:
                    {
                        PosRoof = PositionRoof.South;
                        break;
                    }
                case 4:
                    {
                        PosRoof = PositionRoof.West;
                        break;
                    }
                case 5:
                    {
                        PosRoof = PositionRoof.NSCrosspiece;
                        break;
                    }
                case 6:
                    {
                        PosRoof = PositionRoof.EWCrosspiece;
                        break;
                    }
                case 7:
                    {
                        PosRoof = PositionRoof.EDent;
                        break;
                    }
                case 8:
                    {
                        PosRoof = PositionRoof.SDent;
                        break;
                    }
                case 9:
                    {
                        PosRoof = PositionRoof.WDent;
                        break;
                    }
                case 10:
                    {
                        PosRoof = PositionRoof.NTPiece;
                        break;
                    }
                case 11:
                    {
                        PosRoof = PositionRoof.ETPiece;
                        break;
                    }
                case 12:
                    {
                        PosRoof = PositionRoof.STPiece;
                        break;
                    }
                case 13:
                    {
                        PosRoof = PositionRoof.WTPiece;
                        break;
                    }
                case 14:
                    {
                        PosRoof = PositionRoof.XPiece;
                        break;
                    }
                case 15:
                    {
                        PosRoof = PositionRoof.ExtraPiece;
                        break;
                    }

            }
        
        }


    }
}

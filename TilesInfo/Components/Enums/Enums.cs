using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TilesInfo.Components.Enums
{
    [Serializable]
    public enum Type : int
    {
        None,
        Misc,
        SuppInfo,
        Teleptrs,
        Roofs,
        Doors,
        Tile256,
        Tile1024,
        Floor,
        Wall
    }

    [Serializable]
    public enum PositionWalls : int
    {
        None,
        South,
        Corner,
        East,
        Post
    }

    [Serializable]
    public enum PositionWallWindow : int 
    {
        None,
        South,
        East
    }

    [Serializable]
    public enum PositionStairs : int
    {
        None,
        Block,
        North,
        East,
        South,
        West,
        Squared,
        Rounded,
        MultiNorth,
        MultiSouth,
        MultiWest,
        MultiEast
    }

    [Serializable]
    public enum PositionRoof : int
    {
        None,
        North,
        Eeast,
        South,
        West,
        NSCrosspiece,
        EWCrosspiece,
        NDent,
        EDent,
        SDent,
        WDent,
        NTPiece,
        ETPiece,
        STPiece,
        WTPiece,
        XPiece,
        ExtraPiece
    }

    [Serializable]
    public enum PositionTiles : int
    {
        None,
        South,
        Corner,
        East,
        Post
    }

    [Serializable]
    public enum PositionFloors : int
    {
        None,
        F1,
        F2,
        F3,
        F4,
        F5,
        F6,
        F7,
        F8,
        F9,
        F10,
        F11,
        F12,
        F13,
        F14,
        F15,
        F16
    }
}

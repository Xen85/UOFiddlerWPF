using System;
using System.Collections.Generic;
using System.Linq;
using TilesInfo.Components.Enums;
using TilesInfo.Components.Tiles;

namespace TilesInfo.Components.MultiStruct
{
    [Serializable]
    public class Multi
    {

        #region Fields

        public IList<MultiTile> MultiTiles;
        public IList<TileCategory> Categories;
        public IList<TileCategory> Walls;
        public IList<TileCategory> Roofs;
        public IList<TileCategory> Floors;
        public IList<TileCategory> Misc;
        
        #endregion

        #region Ctor

        public Multi()
        {
            MultiTiles = new List<MultiTile>();
            Categories = new List<TileCategory>();
            Floors = new List<TileCategory>();
            Walls = new List<TileCategory>();
            Misc = new List<TileCategory>();
            Roofs = new List<TileCategory>();

        }
        
        #endregion

        #region methods

        public void AddTile(int id, int x, int y, int z,int flag)
        {
            var multi = new MultiTile() {ID = id, OffsetX = x, OffsetY = y, Z = z,Flag=flag};
           
            SelectTileforMultiTile(multi);
            MultiTiles.Add(multi);
        }

        public override string ToString()
        {
            string result = "";
            foreach (MultiTile tile in MultiTiles)
                result =result + string.Format("{0} {1} {2} {3} {4}", tile.ID, tile.OffsetX, tile.OffsetY, tile.Z, tile.Flag)+'\n';
            return result;
        }

        public void WallSubstitue(TileCategory wallCat)
        {
            var wallTiles =
                from mt in MultiTiles
                where Walls.Contains(mt.Tile.GetStyle().GetCategory())
                select mt;
            foreach (var multiTile in wallTiles)
            {
                var wall = multiTile.Tile as TileWall;
                var walls = wallCat.FindByPosition(wall.Position);
                if(wall.PositionW!= PositionWallWindow.None)
                {
                    walls =
                        from w in walls
                        where ((TileWall) w).WallPos == wall.WallPos
                        select w;
                }
                multiTile.SetTile(walls.First());
            }
        }

        public void MiscSubstitue(TileCategory misc)
        {
            var miscTiles =
                from mt in MultiTiles
                where Misc.Contains(mt.Tile.GetStyle().GetCategory())
                select mt;
            foreach (var multiTile in miscTiles)
            {
                var misctile = multiTile.Tile;
                var miscs = misc.FindByPosition(misctile.Position);
                multiTile.SetTile(miscs.First());
            }
        }

        public void RoofsSubstitue(TileCategory roof)
        {
            var miscTiles =
                from mt in MultiTiles
                where Roofs.Contains(mt.Tile.GetStyle().GetCategory())
                select mt;
            foreach (var multiTile in miscTiles)
            {
                var rooftile = multiTile.Tile as TileRoof;
                var roofs = roof.FindByPosition(rooftile.PosRoof);
                multiTile.SetTile(roofs.First());
            }
        }

        public void SubStitueWallCat(TileCategory wallIn, TileCategory WallOut)
        {
            List<MultiTile> multi = new List<MultiTile>();
            foreach (var multiTile in MultiTiles.Where(multiTile => WallOut.FindTile(multiTile.ID)!=null))
            {
                multi.Add(multiTile);
            }

            var tiles = wallIn.AllTiles();

            foreach (var multitile in multi)
            {
                foreach (var tIn in from tile in tiles select tile as TileWall into tIn let tOut = multitile.Tile as TileWall where tIn.Position == tOut.Position && tIn.PositionW == tOut.PositionW select tIn)
                {
                    multitile.SetTile(tIn);
                }
            }

        }

        private void SelectTileforMultiTile(MultiTile multitile)
        {
            foreach (var tile in TilesInfo.TilesCategorySDKModule.Categories.SelectMany(categorylist => categorylist.Select(tileCategory => tileCategory.FindTile(multitile.ID)).Where(tile => tile != null)))
            {
                multitile.SetTile( tile);
                var cat = tile.GetStyle().GetCategory();
                if(!Categories.Contains(cat)) Categories.Add(cat);
                switch (tile.GetType().Name)
                {
                    case "TileWall":
                        {
                            if (!Walls.Contains(cat)) Walls.Add(cat);
                            break;
                        }
                    case "TileFloor":
                        {
                            if (!Floors.Contains(cat)) Floors.Add(cat);
                            break;
                        }
                    case "TileRoof":
                        {
                            if (!Roofs.Contains(cat)) Roofs.Add(cat);
                            break;
                        }
                    case "TileMisc":
                        {
                            if (!Misc.Contains(cat)) Misc.Add(cat);
                            break;
                        }
                    default:
                        return;
                        
                        
                }
                return;
            }
        }
        
        #endregion

    }
}

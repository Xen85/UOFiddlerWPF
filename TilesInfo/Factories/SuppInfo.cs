using System;
using System.Collections.Generic;
using System.IO;
using OpenUO.Ultima;
using TilesInfo.Components;
using TilesInfo.Components.Enums;
using TilesInfo.Interfaces;
using System.Linq;

namespace TilesInfo.Factories
{
    public class SuppInfo
    {
       

        private InstallLocation _location;
        private Dictionary<int, PositionTiles> Positions; 
        private const string South = @"\";
        private const string corner = @"v";
        private const string East = "./";
        private const string Post = "o";
        public SuppInfo(InstallLocation location)
        {
            _location = location;
            Positions = new Dictionary<int, PositionTiles>();
        }

        public void Populate()
        {
            var lines = File.ReadAllLines(_location.GetPath("suppinfo.txt"));
            for (int i = 2; i < lines.Length; i++)
            {
                var info = lines[i].Split('\t');
                if(!Positions.Keys.Contains(int.Parse(info[1])))
                Positions.Add(Int32.Parse(info[1]),GetPositionTile(info[0]));
            }
        }



        private PositionTiles GetPositionTile(string resolve)
        {
            switch (resolve)
            {
                case South:
                    {
                        return PositionTiles.South;
                    }
                case East:
                    {
                        return PositionTiles.East;
                    }
                case corner:
                    {
                        return PositionTiles.Corner;
                    }
                case Post:
                    {
                        return PositionTiles.Post;
                    }
                default:
                    return PositionTiles.None;
            }
        }


        public void PositionCheck(IList<TileCategory> categories)
        {
            foreach (var tile in from tileCategory in categories from style in tileCategory.Styles from tile in style.Tiles select tile)
            {
                PositionTiles pos;
                Positions.TryGetValue(tile.Id, out pos);
                tile.Position = pos;
            }
        }
    }
}

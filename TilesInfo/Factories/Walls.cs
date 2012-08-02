using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenUO.Ultima;
using TilesInfo.Components;
using TilesInfo.Components.Tiles;
using TilesInfo.Interfaces;

namespace TilesInfo.Factories
{

    public class Walls : Factory , IFactory 
    {
        #region Implementation of IFactory

        public Walls(InstallLocation location) : base(location)
        {
        }

        

        public override void Populate()
        {
        
            var txtFileLines = File.ReadAllLines(Install.GetPath("walls.txt"));
            var typeNames = txtFileLines[1].Split(Separators);
            TileCategory category = null;
            for (int i = 2; i < txtFileLines.Length; i++)
            {
                var infos = txtFileLines[i].Split('\t');

                if (infos[1] == "0")
                {
                    category = new TileCategory(Int32.Parse(infos[2])) {Name = infos.Last()};
                    Categories.Add(category);
                }
                var style = new TileStyle();
                category.AddStyle(style);
                style.Name = infos.Last();
                style.Id = Int32.Parse(infos[1]);
                for (int j = 3; j < typeNames.Length - 2; j++)
                {
                    if (infos[j] != "0")
                    {
                        var tile = new TileWall { Id = short.Parse(infos[j]) };
                        style.AddTile(tile);
                    }
                }
                
            }
            TilesCategorySDKModule.Supp.PositionCheck(Categories);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenUO.Ultima;
using TilesInfo.Components;
using TilesInfo.Components.Enums;
using TilesInfo.Components.Tiles;
using TilesInfo.Interfaces;
using System.IO;
using Tile = TilesInfo.Components.Tile;

namespace TilesInfo.Factories
{
    public class Floors : Factory , IFactory
    {

        public Floors(InstallLocation location) : base(location)
        {
           
        }

        public override void Populate()
        {
            var txtFileLines = File.ReadAllLines(Install.GetPath("floors.txt"));
            var typeNames = txtFileLines[1].Split(Separators);

            for (int i = 2; i < txtFileLines.Length; i++)
            {
                var infos = txtFileLines[i].Split('\t');
                var category = new TileCategory();
                category.Name = infos.Last();
                
                var style = new TileStyle();
                category.AddStyle(style);

                for (int j = 1; j < typeNames.Length-2; j++)
                {
                    if(infos[j]!="0")
                    {
                        var tile = new TileFloor {Id = short.Parse(infos[j])};
                        style.AddTile(tile);
                        tile.ChangeFloorPosition(j);
                    }
                }
                Categories.Add(category);
            }
        }
    }
}

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
    public class Roofs : Factory ,IFactory
    {
        public Roofs(InstallLocation location) : base(location)
        {
        }

        public override void Populate()
        {
            var txtFileLines = File.ReadAllLines(Install.GetPath("roof.txt"));
            var typeNames = txtFileLines[1].Split(Separators);
            TileCategory category = null;
            for (int i = 2; i < txtFileLines.Length; i++)
            {
                var infos = txtFileLines[i].Split('\t');

                if (infos[1] == "0")
                {
                    category = new TileCategory(Int32.Parse(infos[2]));
                    category.Name = infos.Last();
                    Categories.Add(category);
                }
                var style = new TileStyle();
                category.AddStyle(style);
                style.Name = infos.Last();
                style.Index = Int32.Parse(infos[1]);
                for (int j = 3; j < typeNames.Length - 2; j++)
                {
                    if (infos[j] != "0")
                    {
                        var tile = new TileRoof { Id = short.Parse(infos[j]) };
                        style.AddTile(tile);
                       tile.ChangeRoofPosition(j-2);
                    }
                }
                
            }
        }
    }
}

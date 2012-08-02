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
    public class Doors : Factory , IFactory
    {

        public Doors(InstallLocation location) : base(location)
        {
        }

        public List<TileCategory> Categories
        {
            get { return Categories; }
        }

        public override void Populate()
        {
            var txtFileLines = File.ReadAllLines(Install.GetPath("doors.txt"));
            var typeNames = txtFileLines[1].Split(Separators);

            for (int i = 2; i < txtFileLines.Length; i++)
            {
                var infos = txtFileLines[i].Split('\t');
                var category = new TileCategory();
                category.Name = infos.Last();

                var style = new TileStyle();
                category.AddStyle(style);

                for (int j = 1; j < typeNames.Length - 2; j++)
                {
                    if (infos[j] != "0")
                    {
                        var tile = new TileDoor { Id = short.Parse(infos[j]) };
                        style.Tiles.Add(tile);
                    }
                }
                Categories.Add(category);

            }
            TilesCategorySDKModule.Supp.PositionCheck(Categories);
        }
    }
}

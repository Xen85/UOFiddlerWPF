using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TilesInfo.Components;
using TilesInfo.Components.MultiStruct;
using TilesInfo.Interfaces;

namespace TilesInfo.Factories
{
    public class TxtFile : IFactory 
    {
        private readonly string _location = "";
        private  Multi _multi;

        public TxtFile(string location)
        {
            _location = location;
            _multi = new Multi();
        }

        public IList<TileCategory> Categories
        {
            get { return _multi.Categories; }
        }

        public IList<TileCategory> Walls
        {
            get { return _multi.Walls; }
        }

        public void Populate()
        {
            var lines = File.ReadAllLines(_location);

            foreach (var line in lines)
            {
                var strings = line.Split(' ');
                _multi.AddTile(int.Parse(strings[0]), int.Parse(strings[1]), int.Parse(strings[2]), int.Parse(strings[3]), int.Parse(strings[4]));
            }
        }

        public void SubstitueWallCat(TileCategory InWalls, TileCategory OutWalls)
        {
            _multi.SubStitueWallCat(InWalls,OutWalls);
        }


        public override string ToString()
        {
            return _multi.ToString();
        }
            
    }
}

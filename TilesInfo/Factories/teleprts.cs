using System;
using System.Collections.Generic;
using OpenUO.Ultima;
using TilesInfo.Interfaces;

namespace TilesInfo.Factories
{
    public class Teleprts : Factory,  IFactory
    {
        public Teleprts(InstallLocation location) : base(location)
        {
        }

        public List<Components.TileCategory> Categories
        {
            get { throw new NotImplementedException(); }
        }

        public void Populate()
        {
            throw new NotImplementedException();
        }
    }
}

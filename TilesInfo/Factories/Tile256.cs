using System;
using System.Collections.Generic;
using OpenUO.Ultima;
using TilesInfo.Components;
using TilesInfo.Interfaces;

namespace TilesInfo.Factories
{
    public class Tile256 : Factory , IFactory
    {
        #region Implementation of Translator

        public Tile256(InstallLocation location) : base(location)
        {
        }

        public List<TileCategory> Categories
        {
            get { throw new NotImplementedException(); }
        }

        public void Populate()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

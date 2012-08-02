using System;
using System.Collections.Generic;
using OpenUO.Ultima;
using TilesInfo.Components;
using TilesInfo.Interfaces;

namespace TilesInfo.Factories
{
    public class Tile1024 : Factory , IFactory
    {
        #region Implementation of Translator

        public Tile1024(InstallLocation location) : base(location)
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

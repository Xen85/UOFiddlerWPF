using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OpenUO.Ultima;
using TilesInfo.Components;
using TilesInfo.Components.Tiles;
using TilesInfo.Interfaces;

namespace TilesInfo.Factories
{
    public abstract class Factory : IFactory 
    {
        protected InstallLocation Install;
        protected IList<TileCategory> _categories;
        public static readonly char[] Separators = { '\t', ' ' };
        public Factory(InstallLocation location)
        {
            Install = location;
            _categories = new List<TileCategory>();
        }

        #region Implementation of IFactory

        public IList<TileCategory> Categories
        {
            get { return _categories; }
        }

        public virtual void Populate()
        {
            
        }

        #endregion
    }
}

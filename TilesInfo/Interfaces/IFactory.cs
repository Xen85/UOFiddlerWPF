using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TilesInfo.Components;

namespace TilesInfo.Interfaces
{
    public interface IFactory
    {
        IList<TileCategory> Categories { get; }
        void Populate();
        
    }
}

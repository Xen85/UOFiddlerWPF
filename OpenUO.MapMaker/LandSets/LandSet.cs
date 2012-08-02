using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;
using OpenUO.MapMaker.Elements.ColorArea;
using OpenUO.MapMaker.Elements.ColorArea.Area;
using OpenUO.MapMaker.Elements.ColorArea.Mountains;
using OpenUO.MapMaker.Elements.Items;
using OpenUO.MapMaker.Elements.Items.ItemCoast;
using OpenUO.MapMaker.Elements.Items.ItemsSmooth;
using OpenUO.MapMaker.Elements.Textures;

namespace OpenUO.MapMaker.LandSets
{
    public class LandSet
    {
        #region Personal ID
        
        public Id Id { get; set; }

        public string Name { get; set; }
        
        public Color Color { get; set; }

        #endregion

        #region Area Color Props

        public Area ColorArea { get; set; }

        public AreaCoast Coast { get; set; }

        public ColorMountains Montains { get; set; }

        #endregion

        #region Items Area Props

        public ItemsCoasts ItemsInCoasts { get; set; }

        public ItemsSmooth ItemsSmooth { get; set; }

        public Items Items { get; set; }

        #endregion

        #region Textures Area

        public Textures Textures { get; set; }

        public IEnumerable<Elements.Textures.TextureCliff.TextureCliff> FromCliffs { get; set; }

        public IEnumerable<Elements.Textures.TextureCliff.TextureCliff> ToCliffs { get; set; }

        public IEnumerable<Elements.Textures.TextureCliff.TextureCliff> ContainsCliffs { get; set; }

        public IEnumerable<Elements.Textures.TextureSmooth.TextureSmooth> TexturesSmoothFrom { get; set; }

        public IEnumerable<Elements.Textures.TextureSmooth.TextureSmooth> TextureSmoothsTo { get; set; }

        #endregion

    }
}

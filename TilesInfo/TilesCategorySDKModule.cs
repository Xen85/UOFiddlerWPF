using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using OpenUO.Ultima;
using TilesInfo.Components;
using TilesInfo.Factories;
using Tile = TilesInfo.Components.Tile;

namespace TilesInfo
{
    public class TilesCategorySDKModule
    {
        #region fields
        private char[] separator = new[]{'-'};
        private char[] separator2 = new[] {' '};
        #endregion

        #region static fields
        public static InstallLocation Install;
        public static IList<IList<TileCategory>> Categories;
        #endregion

        #region public fields
        public IList<Factory> Factories; 
        public static SuppInfo Supp;
        public Walls Walls;
        public Misc Misc;
        public Roofs Roofs;
        public Floors Floors;
        public TxtFile Txt;
        #endregion

        #region props

        public IList<TileCategory> WallsCat { get { return Categories[0]; } }
        public IList<TileCategory> MiscCat { get { return Categories[1]; } }
        public IList<TileCategory> RoofCat { get { return Categories[2]; } }
        public IList<TileCategory> FloorsCat { get { return Categories[3]; } }
        public IList<TileCategory> Token { get { return Categories[4]; } } 
        public TileData TileData { get; set; }
        public Boolean CheckFromTxt { get; set; }
        public List<Tile> TmpTileList { get; set; }
        public List<TileStyle> TmpStyleList { get; set; }
        
        #endregion

        #region ctor
        public TilesCategorySDKModule(InstallLocation install)
        {
            
            Install = install;
            Factories = new List<Factory>();
            Supp = new SuppInfo(install);
            Supp.Populate();
            Walls = new Walls(install);
            Misc = new Misc(install);
            Roofs = new Roofs(install);
            Floors = new Floors(install);
            Factories.Add(Walls);
            Factories.Add(Misc);
            Factories.Add(Roofs);
            Factories.Add(Floors);
            Categories = new List<IList<TileCategory>>();
            TileData = new TileData(install);
            CheckFromTxt = true;
            TmpStyleList = new List<TileStyle>();
            TmpTileList = new List<Tile>();


        }
        #endregion

        #region methods

        public void Populate()
        {
            if(CheckFromTxt)
            {
                foreach (var factory in Factories)
                {

                    factory.Populate();
                }
            }
            foreach (var fact in Factories)
            {
                Categories.Add(fact.Categories);
            }


            var walls = TileData.ItemTable.Where(itemData => itemData.Flags.HasFlag(TileFlag.Wall) && itemData.Height == 20 && !itemData.Name.Contains("arc")&&!itemData.Flags.HasFlag(TileFlag.Window)&& !itemData.Flags.HasFlag(TileFlag.Door)).ToList();
            var windows = TileData.ItemTable.Where(itemData => itemData.Flags.HasFlag(TileFlag.Wall) && itemData.Height == 20 && itemData.Flags.HasFlag(TileFlag.Window)).ToList();
            var halfWalls = TileData.ItemTable.Where(itemData => itemData.Flags.HasFlag(TileFlag.Wall) && itemData.Height == 10 && !itemData.Flags.HasFlag(TileFlag.Window)).ToList();
            var quarterWalls = TileData.ItemTable.Where(itemData => itemData.Flags.HasFlag(TileFlag.Wall) && itemData.Height == 5 && !itemData.Flags.HasFlag(TileFlag.Window)).ToList();
            var archs = TileData.ItemTable.Where(itemData => itemData.Flags.HasFlag(TileFlag.Wall) && itemData.Name.Contains("arc")).ToList();
            var roof = TileData.ItemTable.Where(itemData => itemData.Flags.HasFlag(TileFlag.Roof)).ToList();
            
            var wallCategory = new TileCategory(){Name = "wall"};
            var windowCategory = new TileCategory() {Name = "window"};
            var halfCategory = new TileCategory() {Name = "half"};
            var quarterCategory = new TileCategory() {Name = "quarter"};
            var arcsCategory = new TileCategory() { Name = "arch" };
            var roofCategory = new TileCategory() {Name = "roof"};

            var style = new TileStyle();
            
            var lists = Misc.Categories.Union(Walls.Categories).ToList();
            
            if(!CheckFromTxt)
                lists.Clear();

            var listcat = new List<TileCategory>();

            
            FullEmptyCategoriesTxTChecked(lists, walls, wallCategory);
            FullEmptyCategoriesTxTChecked(lists, quarterWalls, quarterCategory);
            FullEmptyCategoriesTxTChecked(lists, halfWalls, halfCategory);
            FullEmptyCategoriesTxTChecked(lists, windows, windowCategory);
            FullEmptyCategoriesTxTChecked(lists, archs, arcsCategory);
            FullEmptyCategoriesTxTChecked(lists,roof,roofCategory);
            
            foreach (var s in wallCategory.Styles)
            {
                var category = new TileCategory() {Name = s.Name};
                category.AddStyle(s);
                
                var half = Selector(halfCategory, s);
                var quarter = Selector(quarterCategory, s);
                var window = Selector(windowCategory, s);
                var arch = Selector(arcsCategory, s);
                var r = Selector(roofCategory, s);
                
                if(half!= null)
                    category.AddStyle(half);
                if(quarter != null)
                    category.AddStyle(quarter);
                if(window!=null)
                    category.AddStyle(window);
                if(arch!=null)
                    category.AddStyle(arch);
                if(r!=null)
                    category.AddStyle(r);
               
                listcat.Add(category);
                }

            listcat.Add(RemoveDuplicates(listcat,windowCategory));
            listcat.Add(RemoveDuplicates(listcat, roofCategory));
            listcat.Add(RemoveDuplicates(listcat, halfCategory));
            listcat.Add(RemoveDuplicates(listcat, quarterCategory));
            listcat.Add(RemoveDuplicates(listcat, arcsCategory));
            
            Categories.Add(listcat);
        }
               
        public void TakeFromTXTFile(string locationFile)
        {
            Txt = new TxtFile(locationFile);
            Txt.Populate();
        }
         
        public void Save(string where)
        {
            using (var file = new FileStream(where, FileMode.OpenOrCreate))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(file, Categories);
            }
        }

        public void Load(string where)
        {
            var binaryformatter = new BinaryFormatter();

            using(var file = new FileStream(where,FileMode.Open))
            {
                Categories.Clear();
                Categories = (IList<IList<TileCategory>>)binaryformatter.Deserialize(file);
            }
        }

        private void FullEmptyCategoriesTxTChecked(IEnumerable<TileCategory> list, IEnumerable<ItemData> datalist, TileCategory tileCategory)
        {
            var style = new TileStyle();
            foreach (ItemData itemData in datalist)
            {
                Tile tile = null;
                int number = itemData.Id;
                
                var tiles = from cat in list
                            let t = cat.FindTile(number)
                            where t != null
                            select t;
                tile = tiles.FirstOrDefault();
                if (tile == null)
                {
                    string name = string.Format("{0}-{1}", tileCategory.Name,
                                                    itemData.Name.Replace(tileCategory.Name, "").Split(separator2,
                                                                                                       StringSplitOptions
                                                                                                           .
                                                                                                           RemoveEmptyEntries)
                                                        .FirstOrDefault());
                   
                    if (string.IsNullOrEmpty(style.Name) || style.Name != name)
                    {
                        if (style.Tiles.Count > 0 && tileCategory.FindStyleByName(style.Name) == null)
                        {
                           tileCategory.AddStyle(style);
                        }

                        
                        var st2 = tileCategory.FindStyleByName(name);
                        if (st2 == null) style = new TileStyle {Name = name};
                        else style = st2;
                    }
                    style.AddTile(new Tile() {Id = number, Name = itemData.Name});
                }
            }
            tileCategory.AddStyle(style);
        }

        private TileStyle Selector (TileCategory tileCategory, TileStyle s)
        {
            var style = from sh in tileCategory.Styles
                       where sh.Name.Split(separator, StringSplitOptions.RemoveEmptyEntries).Last() == s.Name.Split(separator, StringSplitOptions.RemoveEmptyEntries).Last()
                       select sh;
            return style.FirstOrDefault();
        }

        private List<TileStyle> StylesSelector(List<TileCategory> listcat,TileCategory category )
        {
            return (from cat in listcat
                    from s in category.Styles
                    where cat.FindStyleByName(s.Name) != null
                    select s).ToList();
        }

        private TileCategory RemoveDuplicates(List<TileCategory> listcat,TileCategory cat )
        {
            var styles = StylesSelector(listcat, cat);

            foreach (TileStyle tileStyle in styles)
            {
                cat.Styles.Remove(tileStyle);
            }
            return cat;
        }

        #endregion
    }
}

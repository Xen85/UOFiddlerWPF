using System;
using System.Collections.Generic;
using System.Linq;
using TilesInfo.Components.Enums;
using TilesInfo.Components.Tiles;
using System.Runtime.Serialization;


namespace TilesInfo.Components
{
    [Serializable()]
    [DataContract]
    public class TileStyle
    {
        #region fields
        private TileCategory _myCategory = new TileCategory();
        private IList<Tile> _list = new List<Tile>();
        private string _name = "";
        #endregion

        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public string Name { get { return _name; } set { _name = value ?? ""; } }
        

        [DataMember]
        public IList<Tile> Tiles
        {
            get { return _list; }
            set { _list = value ?? new List<Tile>(); }
        }

        public TileStyle()
        {
            Name = "";
            Id = -1;
            Tiles= new List<Tile>();
        }

        public TileStyle(int number)
            :this()
        {
            Id = number;
        }

        public Tile FindTile(int id)
        {

            var tile =
                from t in Tiles
                where t.Id == id
                select t;

            return tile.FirstOrDefault();
        }

        public TileCategory GetCategory()
        {
            return _myCategory;
        }

        public void SetCategory(TileCategory category)
        {
            _myCategory = category;
        }

        public void AddTile(Tile tile)
        {
            Tiles.Add(tile);
            tile.SetStyle(this);
        }

        public IEnumerable<Tile> FindTileByPosition(PositionTiles position)
        {
            return Tiles.Where(tile => tile.Position == position);
        }

        public IEnumerable<TileRoof> FindTileByPosition(PositionRoof position)
        {
            if (Tiles.First() is TileRoof)
                return from tile in Tiles where ((TileRoof)tile).PosRoof == position select (TileRoof)tile;
                return null;
        }

        public void RemoveTile(Tile t)
        {
            if (t == null)
                return;
            t.SetStyle(new TileStyle());
            Tiles.Remove(t);
        }
    }
}

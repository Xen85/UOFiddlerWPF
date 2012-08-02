using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TilesInfo.Components.Enums;
using TilesInfo.Components.Tiles;
using System.Runtime.Serialization;

namespace TilesInfo.Components
{
    [Serializable()]
    [DataContract]
    public class TileCategory
    {
        public TileCategory()
        {
            Name = "";
            Id = -1;
            Styles = new List<TileStyle>();
        }

        public TileCategory(int id)
            :this()
        {
            Id = id;
        }

        public Tile FindTile(int id)
        {
            return Styles.Select(tileStyle => tileStyle.FindTile(id)).Where(tile => tile != null).FirstOrDefault();
        }

        public void AddStyle(TileStyle style)
        {
            Styles.Add(style);
            style.SetCategory(this);
        }

        public IEnumerable<Tile> FindByPosition(PositionTiles pos)
        {
            IEnumerable<Tile> list = new List<Tile>();
            return Styles.Aggregate(list, (current, tileStyle) => current.Union(tileStyle.FindTileByPosition(pos)));
        }

        public IEnumerable<TileRoof> FindByPosition(PositionRoof pos)
        {
            var list = new List<TileRoof>();
            foreach (var tileStyle in Styles)
            {
                list.Union(tileStyle.FindTileByPosition(pos));
            }
            return list;
        }

        public IEnumerable<Tile> AllTiles()
        {
            IEnumerable<Tile> list = new List<Tile>();
            return Styles.Aggregate(list, (current, style) => current.Union(style.Tiles));
        }

        public TileStyle FindStyleByName(string name)
        {
            return Styles.Where(tileStyle => name == tileStyle.Name).FirstOrDefault();
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public IList<TileStyle> Styles { get; set; }
    }


    
}

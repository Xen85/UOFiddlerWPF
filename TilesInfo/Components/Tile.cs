using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TilesInfo.Components.Enums;
using Type = TilesInfo.Components.Enums.Type;
using System.Runtime.Serialization;

namespace TilesInfo.Components
{
    [Serializable()]
    [DataContract]
    public class Tile : IEquatable<Tile>, IEquatable<int>
    {
        #region Fileds
        protected int _Id = -1;
        protected PositionTiles Pos = 0;
        [DataMember]
        public String Name { get; set; }
        private TileStyle _myStyle = new TileStyle();
        [DataMember]
        public Type Type { get; set; }

        #endregion

        #region ctor
        public Tile()
        {
            Type = Type.None;
            Name = "";
            Pos = PositionTiles.None;
        }
        #endregion

        #region props
        [DataMember]
        public virtual PositionTiles Position
        {
            get { return Pos; }
            set { Pos = value; }
        }
        [DataMember]
        public int Id
        {
            get { return _Id; }
            set{ChangeId(value);}
        }

        #endregion
        
        #region methods

        public void ChangeId(int id)
        {
            _Id = id;
        }
        
        public void SetStyle(TileStyle style)
        {
            _myStyle = style;
        }

        public TileStyle GetStyle()
        {
            return _myStyle;
        }
        

        #endregion

        #region Implementation of IEquatable<Tile>

        public bool Equals(Tile other)
        {
            return Id == other.Id;
        }

        #endregion

        #region Implementation of IEquatable<int>

        public bool Equals(int other)
        {
            return Id == other;
        }

        #endregion
    }
}

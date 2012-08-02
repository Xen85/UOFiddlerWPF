using System;
using System.Collections.Generic;
using OpenUO.MapMaker.Elements.BaseTypes.Base;
using OpenUO.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using EdgeDirection = OpenUO.MapMaker.Elements.BaseTypes.ComplexTypes.Enum.EdgeDirection;

namespace OpenUO.MapMaker.Elements.BaseTypes.ComplexTypes
{
    [Serializable]
    public class Transition
    {
        public List<ListLine> Lines { get; set; }
  
        public Transition()
        {
            Lines = new List<ListLine>();

            for (int i = 0; i < 3; i++)
            {
                Lines.Add(new ListLine());
            }
        }

        #region Lists
        /// <summary>
        /// Pointer to the right Linear List
        /// </summary>
        public ListLine Line { get { return Lines[(int)LineType.Line]; } }

        #region LinearTexures
        /// <summary>
        /// 
        /// </summary>
        public ItemList LineNorth { get { return Line.List[(int)LinearDirection.North]; } }
        /// <summary>
        /// 
        /// </summary>
        public ItemList LineEast { get { return Line.List[(int)LinearDirection.East]; } }
        /// <summary>
        /// 
        /// </summary>
        public ItemList LineWest { get { return Line.List[(int)LinearDirection.West]; } }
        /// <summary>
        /// 
        /// </summary>
        public ItemList LineSouth { get { return Line.List[(int)LinearDirection.South]; } }
        #endregion

        /// <summary>
        /// Pointer to the right Big edge List
        /// </summary>
        public ListLine Border { get { return Lines[(int)LineType.Border]; } }

        #region BigEdge Lists
        /// <summary>
        /// 
        /// </summary>
        public ItemList BorderNorthEast { get { return Border.List[(int)EdgeDirection.NortEast]; } }
        /// <summary>
        /// 
        /// </summary>
        public ItemList BorderNorthWest { get { return Border.List[(int)EdgeDirection.NorthWest]; } }
        /// <summary>
        /// 
        /// </summary>
        public ItemList BorderSouthEast { get { return Border.List[(int)EdgeDirection.SouthEast]; } }
        /// <summary>
        /// 
        /// </summary>
        public ItemList BorderSouthWest { get { return Border.List[(int)EdgeDirection.SouthWest]; } }
        #endregion

        /// <summary>
        /// Pointer to the right Little Edge List
        /// </summary>
        public ListLine Edge { get { return Lines[(int)LineType.Edge]; } }

        #region little edge Lists

        /// <summary>
        /// 
        /// </summary>
        public ItemList EdgeNorthWest { get { return Edge.List[(int)EdgeDirection.SouthWest]; } }

        /// <summary>
        /// 
        /// </summary>
        public ItemList EdgeNorthEast { get { return Edge.List[(int)EdgeDirection.NortEast]; } }

        /// <summary>
        /// 
        /// </summary>
        public ItemList EdgeSouthEast { get { return Edge.List[(int)EdgeDirection.SouthEast]; } }

        /// <summary>
        /// 
        /// </summary>
        public ItemList EdgeSouthWest { get { return Edge.List[(int)EdgeDirection.SouthWest]; } }

        #endregion

        #endregion

        #region methods
        public void AddElement(LineType line, int direction, Id element)
        {
            Lines[(int)line].AddElement(direction,element);
        }
        #endregion
    }
}

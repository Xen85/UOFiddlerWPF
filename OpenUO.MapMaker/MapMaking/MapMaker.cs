using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using OpenUO.MapMaker.Elements.BaseTypes.Base;
using OpenUO.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using OpenUO.MapMaker.Elements.ColorArea;
using OpenUO.MapMaker.Elements.Items;
using OpenUO.MapMaker.Elements.Items.ItemText;
using OpenUO.MapMaker.Elements.Textures;
using OpenUO.MapMaker.Elements.Textures.TextureCliff;
using OpenUO.MapMaker.Elements.Textures.TextureSmooth;

namespace OpenUO.MapMaker.MapMaking
{
    public class MapMaker
    {
        public static Random Random { get; set; }
        public int MinX = 2;
        public int MinY = 2;
        private readonly int _stride;
        private int[] _directions;

        #region props
        #region scripts
        
        #region scripts for areas
        public ColorAreas ColorAreas { get; set; }
        public ColorAreas ColorAreasCoast { get; set; }
        public MountainsAreas ColorMountainsAreas { get; set; }
        #endregion

        #region scripts for Items
        public ItemsAll Items { get; set; }
        public CoastsAll ItemsCoasts { get; set; }
        public SmoothsAll ItemsSmooth { get; set; }
        #endregion

        #region scripts for Textures
        public TextureArea TextureAreas { get; set; }
        public SmoothTextures TxtureSmooth { get; set; }
        public Cliffs Cliffs { get; set; }
        #endregion
        
        #endregion

        #region Matrix for making

        /// <summary>
        /// cache of the bitmap map file
        /// </summary>
        public Color[] BitmapMap { get; set; }
        /// <summary>
        /// cache of the bitmap Z file
        /// </summary>
        public Color[] BitmapMapZ { get; set; }

        /// <summary>
        /// temp map used for mountains and smooth
        /// </summary>
        private readonly int[] _MapOcc;

        /// <summary>
        /// Calculated altitude of the map
        /// </summary>
        private readonly int[] _MapAlt;

        /// <summary>
        /// Id Texture of the map
        /// </summary>
        private readonly int[] _MapID;

        /// <summary>
        /// items of the map
        /// </summary>
        private readonly List<Item>[] _AddItemMap;

        ///// <summary>
        ///// temp copy of the map
        ///// </summary>
        //private readonly Color[] _Tmp;

        #endregion

        /// <summary>
        /// directory where you're going to write your mul files
        /// </summary>
        public string MulDirectory { get; set; }

        /// <summary>
        /// index of the map that you're going to make
        /// </summary>
        public int mapIndex { get; set; }

        /// <summary>
        /// max x of your map
        /// </summary>
        private readonly int _X;

        /// <summary>
        /// max y of your map
        /// </summary>
        private readonly int _Y;

        
        /// <summary>
        /// if you want to process the Z automatically or not
        /// </summary>
        public Boolean AutomaticZMode { get; set; } 

        #endregion

        #region ctor
        /// <summary>
        /// Class costructor
        /// </summary>
        /// <param name="map">map cached previusly</param>
        /// <param name="alt">map altitude cached</param>
        /// <param name="x">max x of the map</param>
        /// <param name="y">max y of the map</param>
        /// <param name="index">index of the map</param>
        public MapMaker(Color[] map, Color[] alt, int x, int y,int index)
        {
           
            BitmapMap = map;
            BitmapMapZ = alt;
            var x1 = x + 10;
            var y1 = y + 10;
            var lenght = x1*y1;
            #region InitArrays
            _MapOcc = new int[lenght];
            _MapOcc.Initialize();
            
            _MapAlt = new int[lenght];
            _MapAlt.Initialize();

            _MapID = new int[x1*y1];
            _MapID.Initialize();

            _AddItemMap = new List<Item>[lenght];
            _AddItemMap.Initialize();
            
            //_Tmp = new Color[lenght];
            //_Tmp.Initialize();

            #endregion

            _X = x;
            _Y = y;

            MulDirectory = "";
            mapIndex = index;
            _stride = _X - 1;
            Random = new Random(DateTime.Now.Millisecond);

            AutomaticZMode = true;
        }
        
        #endregion

        #region Methods

        #region Make
        /// <summary>
        /// main method to build the map
        /// </summary>
        public void Bmp2Map()
        {

            #region initialize data search
            ColorAreas.InitializeSeaches();
            ColorAreasCoast.InitializeSeaches();
            ColorMountainsAreas.InitializeSeaches();

            Items.InitializeSeaches();
            ItemsCoasts.InitializeSeaches();
            ItemsSmooth.InitializeSeaches();

            TextureAreas.InitializeSeaches();
            TxtureSmooth.InitializeSeaches();
            Cliffs.InitializeSeaches();
            #endregion

            BMP2MUL();


            Mountain();

            for (var x = MinX; x < _X-1; x++)
                for (var y = MinY; y < _Y-1; y++)
                {
                    //MakeCoastCheck(x, y);
                    _directions = MakeIndexesDirections(x, y, 1);
                    MakeCoast();
                    SmoothTilesCheck(x, y);
                    MakeCliffs(x, y);
                    SmoothItemCheck(x, y);
                }

            if(AutomaticZMode)
                ProcessZ(1);
            else
            {
                ProcessZ(0);
            }

            SetItem();
            
            WriteStatics();

            WriteMUL();
            
        }

        #endregion

        #region MapInit
        
        /// <summary>
        /// I Init the array of the maps files
        /// </summary>
        private void BMP2MUL()
        {
            int x, y;
            //rrggbb

            for (x = 0; x < _X; x++)
                for (y = 0; y < _Y; y++)
                {
                    var location = CalculateZone(x, y);
                    _MapID[location] = 168; // set Default-Texture (water)
                    _MapAlt[location] = -5;
                    
                    var area = ColorAreas.FindByColor(BitmapMap[location]);
                    var coast = ColorAreasCoast.FindByColor(BitmapMap[location]);

                    if (coast == null && area != null)
                    {
                        _MapID[location] = RandomTexture(area.Index);
                        _MapAlt[location] = Random.Next(area.Low, area.Hight);
                        continue;
                    }


                    if (coast != null)
                    {
                        _MapID[location] = RandomTexture(coast.Index);
                        _MapAlt[location] = Random.Next(coast.Low,coast.Hight);
                        continue;
                    }
                }
        }

        /// <summary>
        /// it's used to process the map automatically
        /// </summary>
        /// <param name="mode">mode of how you want to process the map 0 for following the map, 1 to calculate automatically</param>
        private void ProcessZ(int mode)
        {
            int x, y;

            for (x = MinX; x < _X; x++)
            {
                for (y = MinY; y < _Y; y++)
                {
                    var location = CalculateZone(x, y); 
                    if (mode == 0)
                    {
                        _MapAlt[location] = BitmapMapZ[location].B-128;
                    }
                    else
                    {
                        if (BitmapMapZ[location] != Color.Black)
                        {
                            _MapAlt[location] += BitmapMapZ[location].R-128;

                            var mnt = ColorMountainsAreas.FindMountainByColor(BitmapMap[location]);
                            if (mnt != null)
                                {
                                    if (mnt.ModeAutomatic)
                                    {
                                        _MapAlt[location] -= BitmapMapZ[location].R-128;
                                        break;
                                    }
                                    if (_MapAlt[location] > 127 && BitmapMapZ[location].R > 0)
                                        _MapAlt[location] = Random.Next(120, 125);
                                    break;
                                }
                            }
                            if (_MapAlt[location] > 127)
                            {
                                _MapAlt[location] = Random.Next(120, 125);
                            }
                    }
                }
            }
        }

        #endregion

        #region smoots

        #region mountains
        
        /// <summary>
        /// method to init the mountains textures
        /// </summary>
        private void Mountain()
        {
            int chk = 1;
            //rrggbb
            
            for (int x = 0; x < _X; x++)
            {
                for (int y = 0; y < _Y; y++)
                {
                    var location = CalculateZone(x, y);
                    if (ColorMountainsAreas.Contains(BitmapMap[location]))
                        _MapOcc[location] = 20;
                }
            }

            for (int x = MinX; x < _X; x++)
                for (int y = MinY; y < _Y; y++)
                {

                    var location = CalculateZone(x, y);
                    chk = 1;
                    var mountset = ColorMountainsAreas.FindMountainByColor(BitmapMap[CalculateZone(x,y)]);
                    if (mountset == null) continue;

                    _MapID[location] = RandomTexture(mountset.IndexMountainGroup);
                    _MapAlt[location] = Random.Next(mountset.List.First().From, mountset.List.First().To);
                    chk = mountset.IndexMountainGroup.Value;
                    if (!mountset.ModeAutomatic) continue;
                        
                    for (int index = 0; index < mountset.List.Count; index++)
                    {
                        var i = index*2;
                        var directions = MakeIndexesDirections(x, y, i);
                        var cirlce = mountset.List[index];
                        if (_MapOcc[directions[(int)Directions.North]] != 0 && _MapOcc[directions[(int)Directions.NorthEast]] != 0 && _MapOcc[directions[(int)Directions.East]] != 0 &&
                            _MapOcc[directions[(int)Directions.SouthEast]] != 0 && _MapOcc[directions[(int)Directions.South]] != 0 && _MapOcc[directions[(int)Directions.SouthWest]] != 0 &&
                            _MapOcc[directions[(int)Directions.West]] != 0 && _MapOcc[directions[(int)Directions.NorthWest]] != 0)
                        {
                            _MapAlt[location] = Random.Next(cirlce.From, cirlce.To);
                            if (_MapAlt[location] > 127)
                                _MapAlt[location] = Random.Next(120, 125);
                            if (mountset.ColorMountain != Color.Black && (_MapAlt[location] > 115 || i > 8))
                                _MapOcc[location] = chk;
                        }
                    }
                }

            for (int x = MinX; x < _X; x++)
                for (int y = MinY; y < _Y; y++)
                {
                    var location = CalculateZone(x, y);
                    if (_MapOcc[location] == 20)
                        _MapOcc[location] = 0;
                    if (_MapOcc[location] <= 0) continue;
                    
                    var mountset = ColorMountainsAreas.FindMountainById(new Id() {Value = _MapOcc[location]});
                    BitmapMap[location] = mountset.ColorMountain;
                    _MapID[location] = RandomTexture(mountset.IndexMountainGroup);
                    _MapOcc[location] = 0;
                }
        }

        #endregion

        #region SmoothTextures

        /// <summary>
        /// transitions from a kind of terrain to anotherkind
        /// </summary>
        /// <param name="x">location of the map</param>
        /// <param name="y">location of the map</param>
        private void SmoothTilesCheck(int x, int y)
        {
            var listSmooth = TxtureSmooth.FindFromByColor(BitmapMap[CalculateZone(x,y)]);
            if(listSmooth.Count()== 0)
                return;
            var location = CalculateZone(x, y);
            Color A = BitmapMap[location];
            int special = 0;
            int z = 0;

            if (_MapOcc[location] == 0)
            {
                // x = nicht def.

                //Border
                //xB
                //Ax
                int x1 = x + 1;
                int y1 = y - 1;
                if (BitmapMap[CalculateZone(x1, y1)] != A)
                {

                    var smoothT = Smooth(listSmooth, x1, y1);

                    _MapID[location] = RandomFromList(smoothT.Border.Forth.List);
                    special = 2;
                    z = _MapAlt[_directions[(int)Directions.NorthEast]] + _MapAlt[_directions[(int)Directions.SouthWest]];
                }
                x1 = x - 1;
                y1 = y -1;
                //Bx
                //xA
                if (BitmapMap[CalculateZone(x1, y1)] != A)
                {
                    var smoothT = Smooth(listSmooth, x1, y1);
                    _MapID[location] = RandomFromList(smoothT.Border.Third.List);
                    special = 1;
                    z = _MapAlt[_directions[(int)Directions.NorthWest]] + _MapAlt[_directions[(int)Directions.SouthEast]];
                }
                //GA
                //BG
                x1 = x - 1;
                y1 = y + 1;
                if (BitmapMap[CalculateZone(x1, y1)] != A)
                {
                    var smoothT = Smooth(listSmooth, x1, y1);
                    //???? controllare, possibile errore
                    _MapID[location] = RandomFromList(smoothT.Border.Second.List);
                    special = 2;
                    z = _MapAlt[_directions[(int)Directions.SouthWest]] + _MapAlt[_directions[(int)Directions.NorthEast]];
                }
                //Ax
                //xB
                x1 = x + 1;
                y1 = y + 1;
                if (BitmapMap[CalculateZone(x1, y1)] != A)
                {
                    var smoothT = Smooth(listSmooth, x1, y1);
                    _MapID[location] = RandomFromList(smoothT.Border.First.List);
                    special = 2;
                    z = _MapAlt[_directions[(int)Directions.SouthEast]] + _MapAlt[_directions[(int)Directions.NorthWest]];
                }

                //Line
                // B
                //xAx
                y1 = y - 1;
                if (BitmapMap[CalculateZone(x, y1)] != A)
                {
                    var smoothT = Smooth(listSmooth, x, y1);

                    _MapID[location] = RandomFromList(smoothT.Line.Third.List);
                    special = 1;
                    z = _MapAlt[_directions[(int)Directions.North]] + _MapAlt[_directions[(int)Directions.South]];
                }
                //xAx
                // B
                y1 = y + 1;
                if (BitmapMap[CalculateZone(x, y1)] != A)
                {
                    var smoothT = Smooth(listSmooth, x, y1);
                    _MapID[location] = RandomFromList(smoothT.Line.First.List);
                    special = 2;
                    z = _MapAlt[_directions[(int)Directions.South]] + _MapAlt[_directions[(int)Directions.North]];
                }
                //x
                //AB
                //x
                x1 = x + 1;
                if (BitmapMap[CalculateZone(x1, y)] != A)
                {
                    var smoothT = Smooth(listSmooth, x1, y);
                    _MapID[location] = RandomFromList(smoothT.Line.Forth.List);
                    special = 2;
                    z = _MapAlt[_directions[(int)Directions.East]] + _MapAlt[_directions[(int)Directions.West]];
                }
                // x
                //BA
                // x
                x1 = x - 1;
                if (BitmapMap[CalculateZone(x1, y)] != A)
                {
                    var smoothT = Smooth(listSmooth, x1, y);
                    _MapID[location] = RandomFromList(smoothT.Line.Second.List);
                    special = 1;
                    z = _MapAlt[_directions[(int)Directions.West]] + _MapAlt[_directions[(int)Directions.East]];
                }

                //Edge
                //B
                //AB
                x1 = x + 1;
                y1 = y - 1;
                if (BitmapMap[CalculateZone(x1, y)] != A && BitmapMap[CalculateZone(x, y1)] != A)
                {
                    var smoothT = Smooth(listSmooth, x, y1);
                    _MapID[location] = RandomFromList(smoothT.Edge.Forth.List);
                    special = 2;
                    z = _MapAlt[_directions[(int)Directions.NorthEast]] + _MapAlt[_directions[(int)Directions.SouthWest]];
                }
                // B
                //BA
                y1 = y - 1;
                x1 = x - 1;
                if (BitmapMap[CalculateZone(x1, y)] != A && BitmapMap[CalculateZone(x, y1)] != A)
                {
                    var smoothT = Smooth(listSmooth, x, y1);
                    _MapID[CalculateZone(x,y)] = RandomFromList(smoothT.Edge.Third.List);
                    special = 1;
                    z = _MapAlt[_directions[(int)Directions.NorthWest]] + _MapAlt[_directions[(int)Directions.SouthEast]];
                }
                //BA
                // B
                x1 = x - 1;
                y1 = y + 1;
                if (BitmapMap[CalculateZone(x1, y)] != A && BitmapMap[CalculateZone(x, y1)] != A)
                {
                    var smoothT = Smooth(listSmooth, x, y1);
                    _MapID[CalculateZone(x,y)] = RandomFromList(smoothT.Edge.Second.List);
                    special = 2;
                    z = _MapAlt[_directions[(int)Directions.SouthWest]] + _MapAlt[_directions[(int)Directions.NorthEast]];
                }
                //AB
                //B
                x1 = x + 1;
                y1 = y + 1;
                if (BitmapMap[CalculateZone(x1, y)] != A && BitmapMap[CalculateZone(x, y1)] != A)
                {
                    var smoothT = Smooth(listSmooth, x, y1);
                    _MapID[location] = RandomFromList(smoothT.Edge.First.List);
                    special = 2;
                    z = _MapAlt[_directions[(int)Directions.SouthEast]] + _MapAlt[_directions[(int)Directions.NorthWest]];
                }
                if (special > 0)
                    _MapOcc[location] = 1;

                if (BitmapMapZ[location] == Color.Black)							
                {
                    _MapAlt[location] = z / 2;
                }
                
            }

            

        }

        /// <summary>
        /// utility find function
        /// </summary>
        /// <param name="listsmooth">list of all smooths</param>
        /// <param name="x">coord x</param>
        /// <param name="y">coord y</param>
        /// <returns></returns>
        private TextureSmooth Smooth(IEnumerable<TextureSmooth> listsmooth ,int x, int y)
        {
            return listsmooth.FirstOrDefault(s=>s.ColorTo== BitmapMap[CalculateZone(x,y)]) ??
                                  listsmooth.First();
        }
        #endregion

        #endregion

        #region Coasts

        #region coast
        
        ///// <summary>
        ///// function to use if you didn't make a good coast bmp
        ///// </summary>
        //void CoastBMP()
        //{
        //    int x, y;
        //    Color A, B;
        //    int z = 2;

        //        for (x = Math.Max(2, MinX); x < _X; x++)
        //            for (y = Math.Max(2, MinY); y < _Y; y++)
        //            {
        //                A = ColorAreasCoast.List[0].Color;
        //                B = ColorAreasCoast.List[1].Color;
        //                if (BitmapMap[CalculateZone(x,y)] == A)
        //                {
        //                    //Seiten
        //                    if (BitmapMap[CalculateZone(x, y - z)] != A && BitmapMap[_directions[(int)Directions.West]] == A && BitmapMap[_directions[(int)Directions.East]] == A) _Tmp[CalculateZone(x, y)] = B;
        //                    if (BitmapMap[CalculateZone(x, y + z)] != A && BitmapMap[_directions[(int)Directions.West]] == A && BitmapMap[_directions[(int)Directions.East]] == A) _Tmp[CalculateZone(x, y)] = B;
        //                    if (BitmapMap[CalculateZone(x + z, y)] != A && BitmapMap[_directions[(int)Directions.North]] == A && BitmapMap[_directions[(int)Directions.South]] == A) _Tmp[CalculateZone(x, y)] = B;
        //                    if (BitmapMap[CalculateZone(x - z, y)] != A && BitmapMap[_directions[(int)Directions.North]] == A && BitmapMap[_directions[(int)Directions.South]] == A) _Tmp[CalculateZone(x, y)] = B;
        //                    //Kanten
        //                    if (BitmapMap[CalculateZone(x + z, y - z)] != A && BitmapMap[_directions[(int)Directions.East]] == A && BitmapMap[_directions[(int)Directions.North]] == A) _Tmp[CalculateZone(x, y)] = B;
        //                    if (BitmapMap[CalculateZone(x - z, y - z)] != A && BitmapMap[_directions[(int)Directions.West]] == A && BitmapMap[_directions[(int)Directions.North]] == A) _Tmp[CalculateZone(x, y)] = B;
        //                    if (BitmapMap[CalculateZone(x - z, y + z)] != A && BitmapMap[_directions[(int)Directions.West]] == A && BitmapMap[_directions[(int)Directions.South]] == A) _Tmp[CalculateZone(x, y)] = B;
        //                    if (BitmapMap[CalculateZone(x + z, y + z)] != A && BitmapMap[_directions[(int)Directions.East]] == A && BitmapMap[_directions[(int)Directions.South]] == A) _Tmp[CalculateZone(x, y)] = B;
        //                    //ECKEN
        //                    if (BitmapMap[CalculateZone(x + z, y)] != A && BitmapMap[CalculateZone(x, y - z)] != A) _Tmp[CalculateZone(x, y)] = B;
        //                    if (BitmapMap[CalculateZone(x - z, y)] != A && BitmapMap[CalculateZone(x, y - z)] != A) _Tmp[CalculateZone(x, y)] = B;
        //                    if (BitmapMap[CalculateZone(x - z, y)] != A && BitmapMap[CalculateZone(x, y + z)] != A) _Tmp[CalculateZone(x, y)] = B;
        //                    if (BitmapMap[CalculateZone(x + z, y)] != A && BitmapMap[CalculateZone(x, y + z)] != A) _Tmp[CalculateZone(x, y)] = B;
        //                }
        //            }


        //        for (x = Math.Max(2, MinX); x < _X; x++)
        //            for (y = Math.Max(2, MinY); y < _Y; y++)
        //                if (_Tmp[CalculateZone(x,y)] != Color.Black)
        //                    BitmapMap[CalculateZone(x,y)] = _Tmp[CalculateZone(x,y)];
            
            
        //}

        /// <summary>
        /// Make Coast method, it search if the given parameter is a coast or not
        /// </summary>
        private void MakeCoast()
        {
            Color Water = BitmapMap[_directions[(int)Directions.Location]];
            int ID = 0;
            if(!ItemsCoasts.FindCoastByColor(Water))
                return;
            

            foreach (var i in ItemsCoasts.List.Where(i => i.Coast.Color == Water))
            {
                Boolean found = false;
                //  L 
                // WxW
                if (BitmapMap[_directions[(int)Directions.North]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.West]] == Water && BitmapMap[_directions[(int)Directions.East]] == Water)
                {
                    //num 2 
                    _MapID[_directions[(int)Directions.Location]] = RandomFromList(i.Ground.Lines[2 / 4].List[2 % 4].List);
                    ID = RandomFromList(i.Coast.Lines[2/4].List[2%4].List);
                    //MapAlt(x,y)=RandomNum(-4, 2);
                    _MapAlt[_directions[(int)Directions.Location]] = -15;
                    _MapOcc[_directions[(int)Directions.Location]] = 1;
                    found = true;
                }
                // WxW
                //  L 
                if (BitmapMap[_directions[(int)Directions.South]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.West]] == Water && BitmapMap[_directions[(int)Directions.East]] == Water)
                {
                    //num 0
                    _MapID[_directions[(int)Directions.Location]] = RandomFromList(i.Ground.Lines[0 / 4].List[0 % 4].List);
                    ID = RandomFromList(i.Coast.Lines[0 / 4].List[0 % 4].List);
                    _MapAlt[_directions[(int)Directions.Location]] = -15;
                    found = true;
                }
                // W
                // xL 
                // W
                if (BitmapMap[_directions[(int)Directions.East]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.North]] == Water && BitmapMap[_directions[(int)Directions.South]] == Water)
                {
                    //3
                    _MapID[_directions[(int)Directions.Location]] = RandomFromList(i.Ground.Lines[3 / 4].List[3 % 4].List);
                    ID = RandomFromList(i.Coast.Lines[3 / 4].List[3 % 4].List);
                    _MapAlt[_directions[(int)Directions.Location]] = -15;
                    found = true;
                }
                //  W
                // Lx
                //  W
                if (BitmapMap[_directions[(int)Directions.West]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.North]] == Water && BitmapMap[_directions[(int)Directions.South]] == Water)
                {
                    //1
                    _MapID[_directions[(int)Directions.Location]] = RandomFromList(i.Ground.Lines[1 / 4].List[1 % 4].List);
                    ID = RandomFromList(i.Coast.Lines[1 / 4].List[1 % 4].List);
                    //if (neu.spezial != 2)
                    //    MapAlt(x, y) = RandomNum(-4, 2);
                    _MapAlt[_directions[(int)Directions.Location]] = -15;
                    _MapOcc[_directions[(int)Directions.Location]] = 1;
                    found = true;
                }

                //Kanten
                // WL
                // xW
                if (BitmapMap[_directions[(int)Directions.NorthEast]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.East]] == Water && BitmapMap[_directions[(int)Directions.North]] == Water)
                {
                    //7
                    _MapID[_directions[(int)Directions.Location]] = RandomFromList(i.Ground.Lines[7 / 4].List[7 % 4].List);
                    ID = RandomFromList(i.Coast.Lines[7 / 4].List[7 % 4].List);
                    _MapAlt[_directions[(int)Directions.Location]] = -15;
                    found = true;
                }
                // LW
                // Wx
                if (BitmapMap[_directions[(int)Directions.NorthWest]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.West]] == Water && BitmapMap[_directions[(int)Directions.North]] == Water)
                {
                    //6
                    _MapID[_directions[(int)Directions.Location]] = RandomFromList(i.Ground.Lines[6 / 4].List[6 % 4].List);
                    ID = RandomFromList(i.Coast.Lines[6 / 4].List[6 % 4].List);
                    //if (neu.spezial != 2)
                    //    MapAlt(x, y) = RandomNum(-2, 2);
                    _MapAlt[_directions[(int)Directions.Location]] = -15;
                    _MapOcc[_directions[(int)Directions.Location]] = 1;
                    found = true;
                }
                // Wx
                // LW
                if (BitmapMap[_directions[(int)Directions.SouthWest]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.West]] == Water && BitmapMap[_directions[(int)Directions.South]] == Water)
                {
                    //5
                    _MapID[_directions[(int)Directions.Location]] = RandomFromList(i.Ground.Lines[5 / 4].List[5 % 4].List);
                    ID = RandomFromList(i.Coast.Lines[5 / 4].List[5 % 4].List);
                    _MapAlt[_directions[(int)Directions.Location]] = -15;
                    found = true;
                }
                // xW
                // WL
                if (BitmapMap[_directions[(int)Directions.SouthEast]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.East]] == Water && BitmapMap[_directions[(int)Directions.South]] == Water)
                {
                    //4
                    _MapID[_directions[(int)Directions.Location]] = RandomFromList(i.Ground.Lines[4 / 4].List[4 % 4].List);
                    ID = RandomFromList(i.Coast.Lines[4 / 4].List[4 % 4].List);
                    _MapAlt[_directions[(int)Directions.Location]] = -15;
                    found = true;
                }
                //Ecken
                // LL
                // xL
                if (BitmapMap[_directions[(int)Directions.NorthEast]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.East]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.North]] == i.Ground.Color)
                {
                    //11
                    _MapID[_directions[(int)Directions.Location]] = RandomFromList(i.Ground.Lines[11 / 4].List[11 % 4].List);
                    ID = RandomFromList(i.Coast.Lines[11 / 4].List[11 % 4].List); 
                    //if (neu.spezial != 2)
                    //    MapAlt(x, y) = RandomNum(-3, 0);
                    _MapAlt[_directions[(int)Directions.Location]] = -15;
                    _MapOcc[_directions[(int)Directions.Location]] = 1;
                    found = true;
                }
                // LL
                // Lx
                if (BitmapMap[_directions[(int)Directions.NorthWest]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.West]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.North]] == i.Ground.Color)
                {
                    //10
                    _MapID[_directions[(int)Directions.Location]] = RandomFromList(i.Ground.Lines[10 / 4].List[10 % 4].List);
                    ID = RandomFromList(i.Coast.Lines[10 / 4].List[10 % 4].List); 
                    //if (neu.spezial != 2)
                    //    MapAlt(x, y) = RandomNum(-3, 0);
                    _MapAlt[_directions[(int)Directions.Location]] = -15;
                    _MapOcc[_directions[(int)Directions.Location]] = 1;
                    found = true;
                }
                // Lx
                // LL
                if (BitmapMap[_directions[(int)Directions.SouthWest]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.West]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.South]] == i.Ground.Color)
                {
                    //9
                    _MapID[_directions[(int)Directions.Location]] = RandomFromList(i.Ground.Lines[9 / 4].List[9 % 4].List);
                    ID = RandomFromList(i.Coast.Lines[9 / 4].List[9 % 4].List);
                    //if (neu.spezial != 2)
                    //if (neu.spezial != 2)
                    //    MapAlt(x, y) = RandomNum(-3, 0);
                    _MapAlt[_directions[(int)Directions.Location]] = -15;
                    _MapOcc[_directions[(int)Directions.Location]] = 1;
                    found = true;
                }
                // xL
                // LL
                if (BitmapMap[_directions[(int)Directions.SouthEast]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.East]] == i.Ground.Color && BitmapMap[_directions[(int)Directions.South]] == i.Ground.Color)
                {
                    //8
                    _MapID[_directions[(int)Directions.Location]] = RandomFromList(i.Ground.Lines[8 / 4].List[8 % 4].List);
                    ID = RandomFromList(i.Coast.Lines[8 / 4].List[8 % 4].List);
                    _MapAlt[_directions[(int)Directions.Location]] = -9;
                    found = true;
                }




                if (ID != 0)
                {
                    const int off = 0;
                    //if (off > 1)
                    //    off = 1;

                    // first revision i know of (ghoulsblade,28.07.2010) : http://zwischenwelt.org/trac/irisserver/browser/tools/uotc/src/coast.cpp?rev=4
                    // uotc: additem	*Add[XBORDER+5][YBORDER+5];
                    var item = new Item() {Id = new ItemID() {Value = ID}};
                    if (_AddItemMap[_directions[(int)Directions.Location]] == null) _AddItemMap[_directions[(int)Directions.Location]] = new List<Item>();
                    _AddItemMap[_directions[(int)Directions.Location]].Add(item); // uotc : additem
                    var coast = ColorAreasCoast.FindByColor(Water);
                    if (coast == null)
                        return;

                    //SiENcE mod
                    if (!AutomaticZMode)
                    {
                        item.Z = _MapAlt[CalculateZone(1, 1)];		
                    }
                    else
                    {
                        item.Z = _MapAlt[_directions[(int)Directions.Location]] + Random.Next(coast.Low, coast.Hight);		// Zuweisung der Coastitemshöhe über die Farbe
                    }
                        
                }
            }
            
        }
        
        #endregion

        #region Cliffs

        /// <summary>
        /// method to make cliff
        /// </summary>
        /// <param name="x">x param</param>
        /// <param name="y">y param</param>
        private void MakeCliffs(int x, int y)
        {
            var location = CalculateZone(x, y);
            if (BitmapMap[location] != Cliffs.Color) return;				
            _MapAlt[location] = 0;									


            //**********************
            //*       Seiten       *
            //**********************

            //  ? 
            // CXC
            //  ? 
            if (BitmapMap[_directions[(int)Directions.West]] == Cliffs.Color && BitmapMap[_directions[(int)Directions.East]] == Cliffs.Color)
            {
                SetCliff(x, y, x, y - 1, x, y + 1, (CliffDirections)0);
            }

            //  C
            // ?X?
            //  C
            if (BitmapMap[_directions[(int)Directions.North]] == Cliffs.Color && BitmapMap[_directions[(int)Directions.South]] == Cliffs.Color)
            {
                SetCliff(x, y, x - 1, y, x + 1, y, (CliffDirections)1);
            }


            //**********************
            //* Anfang und Ende    *
            //**********************

            //  ! 
            // ?X?
            //  C
            if (BitmapMap[_directions[(int)Directions.South]] == Cliffs.Color && BitmapMap[_directions[(int)Directions.North]] != Cliffs.Color)
            {
                SetCliff(x, y, x - 1, y, x + 1, y, (CliffDirections)2);
            }

            //  ? 
            // CX!
            //  ?
            if (BitmapMap[_directions[(int)Directions.West]] == Cliffs.Color && BitmapMap[_directions[(int)Directions.East]] != Cliffs.Color)
            {
                SetCliff(x, y, x, y - 1, x, y + 1, (CliffDirections)3);
            }


            //  C 
            // ?X?
            //  !
            if (BitmapMap[_directions[(int)Directions.North]] == Cliffs.Color && BitmapMap[_directions[(int)Directions.South]] != Cliffs.Color)
            {
                SetCliff(x, y, x - 1, y, x + 1, y, (CliffDirections)4);
            }


            //  ? 
            // !XC
            //  ?
            if (BitmapMap[_directions[(int)Directions.East]] == Cliffs.Color && BitmapMap[_directions[(int)Directions.West]] != Cliffs.Color)
            {
                SetCliff(x, y, x, y + 1, x, y + 1, (CliffDirections)5);
            }

            //**********************
            //* Rundungen          *
            //**********************

            //  C 
            // CX
            //   ?
            if (BitmapMap[_directions[(int)Directions.West]] == Cliffs.Color && BitmapMap[_directions[(int)Directions.North]] == Cliffs.Color)
            {
                SetCliff(x, y, x + 1, y + 1, -500, 0, (CliffDirections)6);
            }

            //  C 
            //  XC
            // ?
            if (BitmapMap[_directions[(int)Directions.East]] == Cliffs.Color && BitmapMap[_directions[(int)Directions.North]] == Cliffs.Color)
            {
                SetCliff(x, y, x - 1, y + 1, -500, 0, (CliffDirections)7);
            }

            // ? 
            //  XC
            //  C
            if (BitmapMap[_directions[(int)Directions.East]] == Cliffs.Color && BitmapMap[_directions[(int)Directions.South]] == Cliffs.Color)
            {
                SetCliff(x, y, x - 1, y - 1, -500, 0, (CliffDirections)8);
            }

            //   ?
            // CX
            //  C
            if (BitmapMap[_directions[(int)Directions.West]] == Cliffs.Color && BitmapMap[_directions[(int)Directions.South]] == Cliffs.Color)
            {
                SetCliff(x, y, x + 1, y - 1, -500, 0, (CliffDirections)9);
            }
        }

        /// <summary>
        /// method to handle the dirty work
        /// </summary>
        /// <param name="x">x param</param>
        /// <param name="y">y param</param>
        /// <param name="x1">x1 direction changed</param>
        /// <param name="y1">y1 direction changed</param>
        /// <param name="x2">x2 direction changed</param>
        /// <param name="y2">y2 direction changed</param>
        /// <param name="directions">direction thatn you need</param>
        private void SetCliff(int x, int y, int x1, int y1, int x2, int y2, CliffDirections directions)
        {
            TextureCliff cliff = null;

            if (x2 != -500)
            {
                cliff = Cliffs.FindFromByColor(BitmapMap[CalculateZone(x1, y1)]).FirstOrDefault(
                    c => c.Directions == directions);
            }
            else
            {
                cliff =
                    Cliffs.FindFromByColor(BitmapMap[CalculateZone(x1, y1)]).FirstOrDefault(
                        c => c.Colors[1] == BitmapMap[CalculateZone(x2, y2)] && c.Directions == directions) ??
                    Cliffs.FindFromByColor(BitmapMap[CalculateZone(x1, y1)]).FirstOrDefault(
                        c => c.Directions == directions);
            }

            if (cliff != null)
            {
                _MapID[CalculateZone(x,y)] = RandomFromList(cliff.List);
            }
        }

        #endregion

        #endregion

        #region Items
        
        /// <summary>
        /// function to make the translations with items
        /// </summary>
        /// <param name="x">x coord</param>
        /// <param name="y">y coord</param>
        void SmoothItemCheck(int x, int y)
        {
            //int special = 0;
            var location = CalculateZone(x, y);
            Color A;
            int zlev = 0;
            int buf = 0;
            Item item = new Item();
            var smoothItem = ItemsSmooth.FindFromByColor(BitmapMap[location]);
            
            if (smoothItem == null)
                return;

            if ((_AddItemMap[location] == null ||_AddItemMap[location].Count == 0) && _MapOcc[location] == 0)
            {
                //Border
                //GB
                //xG
                if (BitmapMap[_directions[(int)Directions.NorthEast]] != smoothItem.ColorFrom)
                {
                    //7
                    item = new Item {Id = {Value = RandomFromList(smoothItem.Border.List[7%4].List)}};
                }
                //BG
                //Gx
                if (BitmapMap[_directions[(int)Directions.NorthWest]] != smoothItem.ColorFrom)
                {
                    //6
                    item = new Item {Id = {Value = RandomFromList(smoothItem.Border.List[6%4].List)}};
                    
                }
                //Gx
                //BG
                if (BitmapMap[_directions[(int)Directions.SouthWest]] != smoothItem.ColorFrom)
                {
                    //5
                    item = new Item {Id = {Value = RandomFromList(smoothItem.Border.List[5%4].List)}};
                }
                //xG
                //GB
                if (BitmapMap[_directions[(int)Directions.SouthEast]] != smoothItem.ColorFrom)
                {
                    //4
                    item = new Item {Id = {Value = RandomFromList(smoothItem.Border.List[4%4].List)}};
                }

                //Line
                // B
                //GxG
                if (BitmapMap[_directions[(int)Directions.North]] != smoothItem.ColorFrom)
                {
                    //2
                    item = new Item {Id = {Value = RandomFromList(smoothItem.Line.List[2%4].List)}};
                }
                //GxG
                // B
                if (BitmapMap[_directions[(int)Directions.South]] != smoothItem.ColorFrom)
                {
                    //0
                    item = new Item {Id = {Value = RandomFromList(smoothItem.Line.List[0%4].List)}};
                }
                //G
                //xB
                //G
                if (BitmapMap[_directions[(int)Directions.East]] != smoothItem.ColorFrom)
                {
                    //3
                    item = new Item { Id = { Value = RandomFromList(smoothItem.Line.List[3 % 4].List) } };
                }
                // G
                //Bx
                // G
                if (BitmapMap[_directions[(int)Directions.West]] != smoothItem.ColorFrom)
                {
                    //1
                    item = new Item {Id = {Value = RandomFromList(smoothItem.Line.List[1%4].List)}};
                }

                //Edge
                //B
                //xB
                if (BitmapMap[_directions[(int)Directions.East]] != smoothItem.ColorFrom && BitmapMap[_directions[(int)Directions.North]] != smoothItem.ColorFrom)
                {
                    //11
                    item = new Item {Id = {Value = RandomFromList(smoothItem.Edge.List[11%4].List)}};
                }
                // B
                //Bx
                if (BitmapMap[_directions[(int)Directions.West]] != smoothItem.ColorFrom && BitmapMap[_directions[(int)Directions.North]] != smoothItem.ColorFrom)
                {
                    //10
                    item = new Item { Id = { Value = RandomFromList(smoothItem.Edge.List[10 % 4].List) } };
                }
                //Bx
                // B
                if (BitmapMap[_directions[(int)Directions.West]] != smoothItem.ColorFrom && BitmapMap[_directions[(int)Directions.South]] != smoothItem.ColorFrom)
                {
                    //9
                    item = new Item { Id = { Value = RandomFromList(smoothItem.Edge.List[9 % 4].List) } };
                }
                //xB
                //B
                if (BitmapMap[_directions[(int)Directions.East]] != smoothItem.ColorFrom && BitmapMap[_directions[(int)Directions.South]] != smoothItem.ColorFrom)
                {
                    //8
                    item = new Item { Id = { Value = RandomFromList(smoothItem.Edge.List[8 % 4].List) } };
                }

                if (item.Id.Value == 0)
                    item.Id.Value = smoothItem.Border.List[5%4].List[0].Value;


                var coast = ColorAreasCoast.List.FirstOrDefault(c => c.Color == BitmapMap[CalculateZone(x,y)]);

                if (coast != null)
                {
                    zlev = Random.Next(coast.Low, coast.Hight);
                    item.Z = _MapAlt[location] + zlev;
                }

                if(_AddItemMap[location] == null)
                    _AddItemMap[location] = new List<Item>();

                _AddItemMap[location].Add(item);
            }
        }

        /// <summary>
        /// method to set items in the map
        /// </summary>
        void SetItem()
            {
                int x, y, z = 0;

                for (x = MinX; x < _X; x++)
                    for (y = MinY; y < _Y; y++)
                    {
                        var location = CalculateZone(x, y);
                        if (_MapOcc[location] == 0) 
                        {
                            var itemgroups = Items.SearchByColor(BitmapMap[location]);
                            if (itemgroups != null)
                            {
                                    var group = itemgroups.List[Random.Next(0, itemgroups.List.Count)];
                                    var random = Random.Next(0, 100);
                                    if (random > group.Percent) continue;

                                    var tmp_item = group.List.First();
                                    if (group.List.Count > 1)
                                    {
                                        z = tmp_item.Z;
                                    }

                                    foreach (Item item in group.List )
                                    {
                                        var locationshift = CalculateZone(x + item.X, y + item.Y);
                                        if (_AddItemMap[locationshift] == null)
                                            _AddItemMap[locationshift] = new List<Item>();
                                        var itemclone = new Item()
                                                            {
                                                                Hue = item.Hue,
                                                                Id = new ItemID(){Value = item.Id.Value},
                                                                X = item.X,
                                                                Y = item.Y,
                                                                Z = item.Z
                                                            };
                                        _AddItemMap[locationshift].Add(itemclone);

                                        if (tmp_item == item)
                                        {
                                            itemclone.Z = (_MapAlt[locationshift] +
                                                      _MapAlt[CalculateZone(x + item.X + 1, y + item.Y)] +
                                                      _MapAlt[CalculateZone(x + item.X, y + item.Y + 1)] +
                                                      _MapAlt[CalculateZone(x + item.X + 1, y + item.Y + 1)]) / 4 + item.Z;
                                        }
                                        else
                                        {
                                            itemclone.Z = (_MapAlt[CalculateZone(x + tmp_item.X, y + tmp_item.Y)] +
                                                      _MapAlt[CalculateZone(x + tmp_item.X + 1, y + tmp_item.Y)] +
                                                      _MapAlt[CalculateZone(x + tmp_item.X, y + tmp_item.Y + 1)] +
                                                      _MapAlt[CalculateZone(x + tmp_item.X + 1, y + tmp_item.Y + 1)]) / 4 + tmp_item.Z + z;
                                            
                                        }
                                    }
                            }
                        }
                    }
            }

        #endregion

        #region Mul Handers

        /// <summary>
        /// method to write statics
        /// </summary>
        private void WriteStatics()
        {
            int blockx, blocky, x, y , items;
            short color;
            byte x2, y2;
            sbyte waterlevel = -120;
            Int32 length = 0,start;

            var staidx = new FileStream(Path.Combine(MulDirectory,string.Format("staidx{0}.mul", mapIndex)),FileMode.OpenOrCreate);
	        var statics = new FileStream(Path.Combine(MulDirectory,string.Format("statics{0}.mul", mapIndex)),FileMode.OpenOrCreate);
            var statics0 = new BinaryWriter(statics);
            var staidx0 = new BinaryWriter(staidx);
	        
            items = 0;
	        start = 0;
            
            using (staidx)
            {
                using (statics)
                {
                    using (statics0)
                    {
                        using(staidx0)
                        {
                            for (blockx = 0; blockx < (_X / 8); ++blockx)
                            {
                                for (blocky = 0; blocky < (_Y/8); ++blocky)
                                {
                                    length = 0;
                                    for (y = (8*blocky); y < (8*(blocky + 1)); y++)
                                    {
                                        for (x = (8*blockx); x < (8*(blockx + 1)); x++)
                                        {
                                            x2 = (byte) (x%8);
                                            y2 = (byte) (y%8);

                                            if (_AddItemMap[CalculateZone(x,y)] != null)
                                            {
                                                foreach (var item in _AddItemMap[CalculateZone(x,y)])
                                                {
                                                    statics0.Write((ushort) item.Id.Value);
                                                    statics0.Write((byte) x2);
                                                    statics0.Write((byte) y2);
                                                    statics0.Write((sbyte) item.Z);
                                                    statics0.Write((Int16) item.Hue);
                                                    length += 7;
                                                    items++;
                                                }
                                            }
                                        }
                                        
                                    }
                                    
                                    staidx0.Write(start);
                                    start += length;
                                    staidx0.Write(length);
                                    staidx0.Write((Int32)1);
                                    
                                    
                                }
                            }
                            statics0.Flush();
                            staidx0.Flush();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// method to write map.mul
        /// </summary>
        void WriteMUL()
        {
            int blockx, blocky, x, y;
            int empty = 0;
            int grey = 0x0244;

            var mapmul = new FileStream(Path.Combine(MulDirectory,"map"+mapIndex+".mul"),FileMode.OpenOrCreate);
            var memmap = new MemoryStream();
            var map0 = new BinaryWriter(memmap);
            using (memmap)
            {
                using (map0)
                {
                    for (blockx = 0; blockx < (_X/8); ++blockx)
                    {
                        for (blocky = 0; blocky < (_Y/8); ++blocky)
                        {
                            map0.Write((int)1);//header
                            for (y = (8*blocky); y < (8*(blocky + 1)); y++)
                            {
                                for (x = (8*blockx); x < (8*(blockx + 1)); x++)
                                {
                                    var id = _MapID[CalculateZone(x,y)];
                                    var z = _MapAlt[CalculateZone(x,y)];
                                    if ((id < 0) || (id >= 0x4000))
                                        id = 0;
                                    if (z < -128)
                                        z = -128;
                                    if (z > 127)
                                        z = 127;
                                        
                                    map0.Write((short)id);//writes tid
                                    map0.Write((sbyte)z);//writes Z
                                }
                            }
                        }
                    }
                    map0.Flush();
                    using (mapmul)
                    {
                        memmap.WriteTo(mapmul);
                    }
                }
                
            }
        }
        
        #endregion

        #region utility methods

        /// <summary>
        /// it randomly choose a texture between the ColorArea choosed
        /// </summary>
        /// <param name="id">id of the ColorArea that you're choosing</param>
        /// <returns>return a texture from the specified id</returns>
        private int RandomTexture(Id id)
            {
                var texture = TextureAreas.FindByIndex(id);
                if (texture != null)
                {
                    int number = Random.Next(0, texture.List.Count - 1);
                    return texture.List[number].Value;
                }
                return 0;
            }

        /// <summary>
        /// it takes a random member from a list of id
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private int RandomFromList(List<Id> list)
            {
                if (list.Count == 0)
                    return 0;
                int number = Random.Next(0, list.Count - 1);
                return list[number].Value;
            }
        
        /// <summary>
        /// it takes a random member in a list of TextureId
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private int RandomFromList(List<TextureID> list)
            {
                if (list.Count == 0)
                    return 0;
                int number = Random.Next(0, list.Count - 1);
                return list[number].Value;
            }
        
        /// <summary>
        /// coordinate for x and y in linear matrix
        /// </summary>
        /// <param name="x">x coord</param>
        /// <param name="y">y coord</param>
        /// <returns></returns>
        private int CalculateZone(int x, int y)
        {
            return (y*_stride)+x;
        }

        /// <summary>
        /// array of precalculated x and y in a linear matrix
        /// </summary>
        /// <param name="x">x coord</param>
        /// <param name="y">y coord</param>
        /// <param name="shift">shifting parameter</param>
        /// <returns>array of params</returns>
        public int[] MakeIndexesDirections(int x, int y, int shift)
        {
            var array = new int[9];
            array[(int) Directions.Location] = CalculateZone(x, y);
            array[(int) Directions.North] = CalculateZone(x, y - shift);
            array[(int) Directions.South] = CalculateZone(x, y + shift);
            array[(int) Directions.East] = CalculateZone(x + shift, y);
            array[(int) Directions.West] = CalculateZone(x - shift, y);
            array[(int) Directions.NorthEast] = CalculateZone(x + shift, y - shift);
            array[(int) Directions.NorthWest] = CalculateZone(x - shift, y - shift);
            array[(int) Directions.SouthEast] = CalculateZone(x + shift, y - shift);
            array[(int) Directions.SouthWest] = CalculateZone(x - shift, y + shift);
            
            return array;
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Directions for array
    /// </summary>
    public enum Directions
    {
        Location,
        North,
        South,
        East,
        West,
        NorthWest,
        NorthEast,
        SouthWest,
        SouthEast
    }
}

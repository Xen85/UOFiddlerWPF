using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using MiscUtil.Conversion;
using MiscUtil.IO;
using OpenUO.MapMaker.Elements.ColorArea;
using OpenUO.MapMaker.Elements.Items;
using OpenUO.MapMaker.Elements.Textures;
using OpenUO.MapMaker.TextFileReading;
using OpenUO.MapMaker.TextFileReading.Factories2.Colors;
using OpenUO.MapMaker.TextFileReading.Factories2.Items;
using OpenUO.MapMaker.TextFileReading.Factories2.Textures;

namespace OpenUO.MapMaker
{
    public class MakeMapSDK
    {
        #region props
        #region Datas

        public ColorAreas ColorArea { get; set; }

        public ColorAreas ColorCoast { get; set; }

        public MountainsAreas ColorMountains { get; set; }

        public ItemsAll Items { get; set; }

        public CoastsAll ItemsCoasts { get; set; }

        public SmoothsAll Smooths { get; set; }

        public TextureArea TextureArea { get; set; }

        public SmoothTextures SmoothTextures { get; set; }

        public Cliffs Cliffs { get; set; }

        #endregion

        #region factories

        public List<Factory> Factories { get; set; }

        public FactoryColorArea FactoryColor { get; set; }

        public FactoryCoast FactoryCoast { get; set; }

        public FactoryMountains FactoryMountains { get; set; }

        public FactoryItems FactoryItems { get; set; }

        public FactoryItemCoasts FactoryItemCoasts { get; set; }

        public FactorySmoothItems FactorySmoothItems { get; set; }

        public FactoryTextureArea FactoryTextureArea { get; set; }

        public FactoryTextureSmooth FactoryTextureSmooth { get; set; }

        public FactoryCliff FactoryCliff { get; set; }

        #endregion

        #region BitmapLocations

        public string BitmapLocationMap { get; set; }

        public string BitmapLocationMapZ { get; set; }

        #endregion

        #region Bitmap and Maps reader

        public BitmapReader BitMap { get; set; }

        public BitmapReader BitMapZ { get; set; }

        #endregion

        #endregion

        #region Ctor
        public MakeMapSDK(string directory)
        {
            #region initialize props

            ColorArea = new ColorAreas();
            ColorMountains = new MountainsAreas();

            Items = new ItemsAll();
            ItemsCoasts = new CoastsAll();
            Smooths = new SmoothsAll();


            TextureArea = new TextureArea();
            SmoothTextures = new SmoothTextures();
            Cliffs = new Cliffs();

            #endregion

            #region initialize Factories

            Factories = new List<Factory>();

            FactoryColor = new FactoryColorArea(Path.Combine(directory, "color_area.txt"));

            Factories.Add(FactoryColor);

            FactoryCoast = new FactoryCoast(Path.Combine(directory, "color_coast.txt"));

            Factories.Add(FactoryCoast);

            FactoryMountains = new FactoryMountains(Path.Combine(directory, "color_mntn.txt"));

            Factories.Add(FactoryMountains);

            FactoryItems = new FactoryItems(Path.Combine(directory, "items.txt"));

            Factories.Add(FactoryItems);

            FactoryItemCoasts = new FactoryItemCoasts(Path.Combine(directory, "ite_tex_coast.txt"));

            Factories.Add(FactoryItemCoasts);

            FactorySmoothItems = new FactorySmoothItems(Path.Combine(directory, "items_smooth.txt"));

            Factories.Add(FactorySmoothItems);

            FactoryTextureArea = new FactoryTextureArea(Path.Combine(directory, "texture_area.txt"));

            Factories.Add(FactoryTextureArea);

            FactoryTextureSmooth = new FactoryTextureSmooth(Path.Combine(directory, "texture_smooth.txt"));

            Factories.Add(FactoryTextureSmooth);

            FactoryCliff = new FactoryCliff(Path.Combine(directory, "texture_cliff.txt"));

            Factories.Add(FactoryCliff);

            #endregion

        }
        
        public MakeMapSDK()
        {
            #region initialize props

            ColorArea = new ColorAreas();
            ColorMountains = new MountainsAreas();
            ColorCoast = new ColorAreas();

            Items = new ItemsAll();
            ItemsCoasts = new CoastsAll();
            Smooths = new SmoothsAll();


            TextureArea = new TextureArea();
            SmoothTextures = new SmoothTextures();
            Cliffs = new Cliffs();

            #endregion
        }
        #endregion
        
        #region factories
        
        public void Populate()
        {
            #region colorread
            FactoryColor.Read();
            ColorArea = FactoryColor.Areas;


            FactoryCoast.Read();
            ColorCoast = FactoryCoast.Area;


            FactoryMountains.Read();
            ColorMountains = FactoryMountains.Mountains;
            #endregion

            #region items

            FactoryItems.Read();
            Items = FactoryItems.Items;

            FactoryItemCoasts.Read();
            ItemsCoasts = FactoryItemCoasts.CoastsAll;

            FactorySmoothItems.Read();
            Smooths = FactorySmoothItems.SmoothsAll;

            #endregion

            #region Textures

            FactoryTextureArea.Read();
            TextureArea = FactoryTextureArea.Textures;

            FactoryTextureSmooth.Read();
            SmoothTextures = FactoryTextureSmooth.Smooth;

            FactoryCliff.Read();
            Cliffs = FactoryCliff.Cliffs;

            #endregion
        }
        
        #endregion

        #region MapMaking

        public void MapMake(string directory, string bitmaplocation, string bitmapZLocation,int x, int y, int index, bool automaticz)
        {
            var bitmapMap = new BitmapReader(bitmaplocation, false).BitmapColors;
            var bitmapZ = new BitmapReader(bitmapZLocation, true).BitmapColors;

            var mulmaker = new MapMaking.MapMaker(bitmapMap, bitmapZ, x, y, index)
                               {
                                   ColorAreas = ColorArea,
                                   ColorAreasCoast = ColorCoast,
                                   ColorMountainsAreas = ColorMountains,
                                   Items = Items,
                                   ItemsSmooth = Smooths,
                                   ItemsCoasts = ItemsCoasts,
                                   TextureAreas = TextureArea,
                                   TxtureSmooth = SmoothTextures,
                                   Cliffs = Cliffs,
                                   AutomaticZMode = automaticz,
                                   MulDirectory = directory
                               };



            mulmaker.Bmp2Map();
        }
            
        #endregion

        #region ACO Making

        public void MakeACO(string folder)
        {
            MemoryStream memory = new MemoryStream();
            EndianBinaryWriter bwriterWriter = new EndianBinaryWriter(EndianBitConverter.Big,memory);
            
            const UInt16 separator = 0;
            UInt16 sectionCounter = 0;

            var colorlist = ColorArea.List.Select(c=>c.Color).ToList();
            
            UInt16 numberOfColors = UInt16.Parse(colorlist.Count.ToString());
            sectionCounter++;
            
            using (memory)
            {
                using (bwriterWriter)
                {
                    bwriterWriter.Write(sectionCounter);
                   
                    bwriterWriter.Write(numberOfColors); // write the number of colors

                    foreach (Color color in colorlist)
                    {
                        ColorStructureWriter(bwriterWriter, color);
                    }
                    sectionCounter++;

                    bwriterWriter.Write(sectionCounter);

                    bwriterWriter.Write(numberOfColors);

                    var encoding = new UnicodeEncoding(true,true,true);

                    foreach (var color in colorlist)
                    {
                        ColorStructureWriter(bwriterWriter,color);
                        
                        var tmpcol = ColorArea.List.FirstOrDefault(c => c.Color == color);
                        var bytes = (encoding.GetBytes(tmpcol.Name));
                        bwriterWriter.Write((ushort)tmpcol.Name.Length+1);
                        bwriterWriter.Write(bytes);
                        bwriterWriter.Write((ushort)0);
                    }
                    bwriterWriter.Flush();

                    using (FileStream output = new FileStream(Path.Combine(folder, "palette.ACO"), FileMode.Create))
                    {
                        memory.WriteTo(output);
                    }
                }

                
            }

        }
        
        private void ColorStructureWriter(EndianBinaryWriter writer, Color color)
        {
            writer.Write((ushort)0);

            writer.Write(color.R);
            writer.Write(color.R);

            writer.Write(color.G);
            writer.Write(color.G);

            writer.Write(color.B);
            writer.Write(color.B);

            writer.Write(color.A); //alfa
            writer.Write(color.A);
        }
        
        #endregion

        #region Save/Load functions

        public void SaveBinary(string folder)
        {
            var objects = new List<object>
                              {
                                  ColorArea,
                                  ColorCoast,
                                  ColorMountains,
                                  Items,
                                  ItemsCoasts,
                                  Smooths,
                                  TextureArea,
                                  SmoothTextures,
                                  Cliffs
                              };

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            using (ms)
            {
                bf.Serialize(ms, objects);
                using (var stream = new FileStream(Path.Combine(folder,"mapmaker.bin"),FileMode.Create))
                {
                    ms.WriteTo(stream);
                }
            }
        }

        public void LoadBinary(string folder)
        {

            BinaryFormatter bf = new BinaryFormatter();
            List<object> objectfrom;
            using(var strema = new FileStream(Path.Combine(folder, "mapmaker.bin"),FileMode.Open))
            {
                objectfrom = (List<object>)bf.Deserialize(strema);
            }

            ColorArea = (ColorAreas)objectfrom[0];
            ColorCoast = (ColorAreas) objectfrom[1];
            ColorMountains = (MountainsAreas)objectfrom[2];

            Items = (ItemsAll) objectfrom[3];
            ItemsCoasts = (CoastsAll)objectfrom[4];
            Smooths = (SmoothsAll) objectfrom[5];

            TextureArea = (TextureArea) objectfrom[6];
            SmoothTextures = (SmoothTextures) objectfrom[7];
            Cliffs = (Cliffs) objectfrom[8];
        }
        
        #endregion
    }
}

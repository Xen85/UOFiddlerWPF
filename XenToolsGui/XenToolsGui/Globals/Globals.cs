using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using OpenUO.Core.Patterns;
using OpenUO.MapMaker;
using OpenUO.Ultima;
using TilesInfo;
using System.IO;

namespace XenToolsGui.Globals
{
    public static class Globals
    {
        private static InstallLocation _install;

        private static string _InstallDir = "";
        //TODO Event for reinit
        private static bool innerInit;
        public static IoCContainer Container;

        public static TilesCategorySDKModule SdkTiles;
        private static List<string> List;

        public static string Installdirectory
        {
            get
            {
                return _InstallDir;
            }
            set
            {
                _install = new InstallLocation(value);
                _InstallDir = value;
            }
        }

        public static InstallLocation InstallLocation { get { return _install; } set { _install = value; Init(); } }

        public static ArtworkFactory ArtworkFactory;

        public static TexmapFactory TexmapFactory;

        public static MakeMapSDK MapMaperSdk;

        public static string SaveTileFileLocation = "";

        public static string SaveDirLocation = "";

        public static string ScriptFolderLocation = "";

        public static string LoadDirLocation = "";

        public static void Init()
        {
            Container = new IoCContainer();
            if (InstallLocation == null)
                InstallLocation = InstallationLocator.Locate().FirstOrDefault();
            if (InstallLocation != null)
            {
                SdkTiles = new TilesCategorySDKModule(InstallLocation);
                SdkTiles.Populate();
            }
            if (!innerInit)
                InnerInit();

        }

        private static void InnerInit()
        {
            MapMaperSdk = new MakeMapSDK();
            Container.RegisterModule<UltimaSDKCoreModule>();
            Container.RegisterModule<OpenUO.Ultima.PresentationFramework.UltimaSDKImageSourceModule>();
            ArtworkFactory = new ArtworkFactory(InstallLocation, Container);
            TexmapFactory = new TexmapFactory(InstallLocation, Container);
            innerInit = true;
        }

        public static void SaveOptions()
        {
            List = new List<string>();

            List.Add(SaveTileFileLocation);
            List.Add(SaveDirLocation);
            List.Add(Installdirectory);

            MemoryStream mem = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(List<String>));
            XmlWriter xmlWriter = new XmlTextWriter(mem, new UTF8Encoding());
            serializer.Serialize(mem, List);

            FileStream fileStream = new FileStream(SaveDirLocation + @"\config.xml", FileMode.Create);
            mem.WriteTo(fileStream);
            mem.Close();
            fileStream.Close();
        }

        public static void LoadOptions(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<String>));

            using (FileStream fs = new FileStream(file, FileMode.Open))
            {
                XmlReader xmlReader = new XmlTextReader(fs);
                List = (List<String>)serializer.Deserialize(xmlReader);
            }

            SaveTileFileLocation = List[0];
            SaveDirLocation = List[1];
            Installdirectory = List[2];
        }
    }
}

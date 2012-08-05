using System.Linq;
using OpenUO.Core.Patterns;
using OpenUO.MapMaker;
using OpenUO.Ultima;
using TilesInfo;

namespace XenToolsGui.Globals
{
    public static class Globals
    {
        private static InstallLocation _install;

        private static string _InstallDir; 
        //TODO Event for reinit
        private static bool innerInit;
        public static IoCContainer Container; 
       
        public static TilesCategorySDKModule SdkTiles;

        public static string Installdirectory { 
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

        public static string SaveTileFileLocation;

        public static string SaveDirLocation;

        public static string ScriptFolderLocation;

        public static string LoadDirLocation;

        public static void Init()
        {
            Container = new IoCContainer();
            if(InstallLocation==null)
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
        
    }
}

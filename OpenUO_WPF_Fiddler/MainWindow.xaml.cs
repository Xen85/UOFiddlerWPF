using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenUO.Core.Patterns;
using OpenUO.Ultima;
using TilesInfo;
using TilesInfo.Components;
using TilesInfo.Factories;
using OpenUO.Ultima;
using OpenUO.Core;
namespace OpenUO_WPF_Fiddler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static TilesInfo.TilesCategorySDKModule SDK;
        public static InstallLocation InstallLocation;
        public static IoCContainer Container;
        public static ArtworkFactory FactoryArt;
        public static TexmapFactory FactoryTex;
        
        
        public MainWindow()
        {
            InitializeComponent();
            Container = new IoCContainer();
            InstallLocation = InstallationLocator.Locate().First();
            SDK = new TilesCategorySDKModule(InstallLocation);
            Container.RegisterModule<UltimaSDKCoreModule>();
            Container.RegisterModule<OpenUO.Ultima.PresentationFramework.UltimaSDKImageSourceModule>();
            FactoryArt = new ArtworkFactory(InstallLocation, Container);
            FactoryTex = new TexmapFactory(InstallLocation,Container);
            DataContext = SDK;
        }

        private void ButtonData_Click(object sender, RoutedEventArgs e)
        {
            new DataView().Show();
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            string s = null;

            using(var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.RootFolder = Environment.SpecialFolder.Desktop;
                
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    s = dialog.SelectedPath;
                }
            }
            textBoxFolder.Text = s;

            
        }

        private void buttonMultiChange_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonMapCreator_Click(object sender, RoutedEventArgs e)
        {
            new MapCreator().Show();
        }

        private void buttonLoadFolder_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxFolder.Text))
            {
                try
                {
                    InstallLocation = new InstallLocation(textBoxFolder.Text);
                    SDK = new TilesCategorySDKModule(InstallLocation);
                    Container = new IoCContainer();
                    Container.RegisterModule<UltimaSDKCoreModule>();
                    Container.RegisterModule<OpenUO.Ultima.PresentationFramework.UltimaSDKImageSourceModule>();
                    FactoryArt = new ArtworkFactory(InstallLocation, Container);
                    FactoryTex = new TexmapFactory(InstallLocation,Container);
                }
                catch (Exception)
                {

                }

            }
        }

    }
}

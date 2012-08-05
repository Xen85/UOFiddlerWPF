using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TilesInfo.Components;
using XenToolsGui.TileViewer.Types;

namespace XenToolsGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Latest _latestLeft = Latest.None;
        private Latest _latestRight = Latest.None;

        public MainWindow()
        {
            InitializeComponent();
            Globals.Globals.Init();
        }
        #region options
        private void FileItemSelectOptions_Click(object sender, RoutedEventArgs e)
        {
            new OptionWindow().Show();
        }
        #endregion


        #region buttons Item Data Tab

        #region Left

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (_latestLeft != Latest.Wall)
            {
                SuperGridTiles.DataContext = Globals.Globals.SdkTiles.WallsCat;
                SuperGridTiles.DataGridCategories.Items.Refresh();
                _latestLeft = Latest.Wall;
            }

        }

        private void ButtonFloats_Click(object sender, RoutedEventArgs e)
        {
            if(_latestLeft!=Latest.Floor)
            {
                SuperGridTiles.DataContext = Globals.Globals.SdkTiles.FloorsCat;
                SuperGridTiles.DataGridCategories.Items.Refresh();
                _latestLeft = Latest.Floor;
            }
        }

        private void buttonRoof_Click(object sender, RoutedEventArgs e)
        {
            if (_latestLeft != Latest.Roof)
            {
                SuperGridTiles.DataContext = Globals.Globals.SdkTiles.RoofCat;
                SuperGridTiles.DataGridCategories.Items.Refresh();
                _latestLeft = Latest.Roof;
            }
        }

        private void buttonMisc_Click(object sender, RoutedEventArgs e)
        {
            if (_latestLeft != Latest.Misc)
            {
                SuperGridTiles.DataContext = Globals.Globals.SdkTiles.MiscCat;
                SuperGridTiles.DataGridCategories.Items.Refresh();
                _latestLeft = Latest.Misc;
            }
        }
        private void buttonTokenLeft_Click(object sender, RoutedEventArgs e)
        {
            if (_latestLeft != Latest.Token)
            {
                SuperGridTiles.DataContext = Globals.Globals.SdkTiles.Token;
                SuperGridTiles.DataGridCategories.Items.Refresh();
                _latestLeft = Latest.Token;
            }
        }
        #endregion

        #region right

        private void buttonWallsRight_Click(object sender, RoutedEventArgs e)
        {
            if (_latestRight != Latest.Wall)
            {
                SuperGridLosedTiles.DataContext = Globals.Globals.SdkTiles.WallsCat;
                SuperGridLosedTiles.DataGridCategories.Items.Refresh();
                _latestRight = Latest.Wall;
            }
        }


        private void ButtonRoofsRight_Click(object sender, RoutedEventArgs e)
        {
            if (_latestRight != Latest.Roof)
            {
                SuperGridLosedTiles.DataContext = Globals.Globals.SdkTiles.RoofCat;
                SuperGridLosedTiles.DataGridCategories.Items.Refresh();
                _latestRight = Latest.Roof;
            }
        }


        private void buttonFloatsRight_Click(object sender, RoutedEventArgs e)
        {
            if (_latestRight != Latest.Floor)
            {
                SuperGridLosedTiles.DataContext = Globals.Globals.SdkTiles.FloorsCat;
                SuperGridLosedTiles.DataGridCategories.Items.Refresh();
                _latestRight = Latest.Floor;
            }
        }

        private void buttonMiscRight_Click(object sender, RoutedEventArgs e)
        {
            if (_latestRight != Latest.Misc)
            {
                SuperGridLosedTiles.DataContext = Globals.Globals.SdkTiles.MiscCat;
                SuperGridLosedTiles.DataGridCategories.Items.Refresh();
                _latestRight = Latest.Misc;
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (_latestRight != Latest.Token)
            {
                SuperGridLosedTiles.DataContext = Globals.Globals.SdkTiles.Token;
                SuperGridLosedTiles.DataGridCategories.Items.Refresh();
                _latestRight = Latest.Token;
            }
        }
        
        #endregion

        
        #region Utility Buttons
        
        private void AddTiles_Click_1(object sender, RoutedEventArgs e)
        {
            var  foundList = Globals.Helpers.FindElementsOfType(SuperGridTiles.DataGridCategories,typeof(DataGrid));
            if (foundList == null)
                return;
            List<DataGrid> dataList = foundList.OfType<DataGrid>().Select(frameworkElement => frameworkElement as DataGrid).ToList();
            foreach (var frameworkElement in dataList)
            {
                var list = frameworkElement.SelectedItems;
                foreach (var VARIABLE in list)
                {
                    if (VARIABLE is Tile)
                        Globals.Globals.SdkTiles.TmpTileList.Add((Tile)VARIABLE);
                }
                frameworkElement.SelectedItems.Clear();
            }
           

            if (dataGridTmpTiles.ItemsSource != Globals.Globals.SdkTiles.TmpTileList)
            dataGridTmpTiles.ItemsSource = Globals.Globals.SdkTiles.TmpTileList;
            dataGridTmpTiles.Items.Refresh();
        }

        #endregion

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {

        }

        #endregion











    }
    public enum Latest
    {
        None,
        Wall,
        Floor,
        Roof,
        Misc,
        Token
    }
}

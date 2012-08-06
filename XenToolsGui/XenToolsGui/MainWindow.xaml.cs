using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
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
            DataContext = Globals.Globals.MapMaperSdk;
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
            var media = SuperGridTiles.DataGridCategories as System.Windows.Media.Visual;
            if (media == null)
                return;
            var foundList = Globals.Helpers.FindElementsOfType(media, typeof(DataGrid));
            if (foundList == null)
                return;

            List<DataGrid> dataList = foundList.OfType<DataGrid>().Select(frameworkElement => frameworkElement).ToList();
            media = null;
            if (dataList.Count == 0)
                return;
            TileStyle style = null;
            var found = false;
            foreach (var dataGrid in dataList)
            {
                foreach (TileStyle obj in dataGrid.SelectedItems.OfType<TileStyle>())
                {
                    style = obj as TileStyle;
                    found = true;
                }
                if(found)
                    break;
            }
            if (style == null)
                return;
            foreach (var tile in Globals.Globals.SdkTiles.TmpTileList)
            {
                style.AddTile(tile);
            }
            Globals.Globals.SdkTiles.TmpTileList.Clear();
            AllGridRefresh();
        }
        
        private void AddTileTmpClick(object sender, RoutedEventArgs e)
        {
            var media = SuperGridLosedTiles.DataGridCategories as System.Windows.Media.Visual;
            if (media == null)
                return;
            var foundList = Globals.Helpers.FindElementsOfType(media, typeof(DataGrid));
            if (foundList == null)
                return;
            List<DataGrid> dataList = foundList.OfType<DataGrid>().Select(frameworkElement => frameworkElement as DataGrid).ToList();
            foreach (var frameworkElement in dataList)
            {
                var list = frameworkElement.SelectedItems;
                foreach (Tile VARIABLE in list.OfType<Tile>())
                {
                    Globals.Globals.SdkTiles.TmpTileList.Add(VARIABLE);
                }
                frameworkElement.SelectedItems.Clear();
            }


            if (dataGridTmpTiles.ItemsSource != Globals.Globals.SdkTiles.TmpTileList)
                dataGridTmpTiles.ItemsSource = Globals.Globals.SdkTiles.TmpTileList;
            AllGridRefresh();
        }

        private void AllGridRefresh()
        {
            var foundList = Globals.Helpers.FindElementsOfType(this, typeof(DataGrid));
            List<DataGrid> dataList = foundList.OfType<DataGrid>().Select(frameworkElement => frameworkElement).ToList();
            foreach (var dataGrid in dataList)
            {
                try
                {
                    dataGrid.Items.Refresh();
                }
                catch(Exception)
                {
                    
                }
            }
        }

        private void ButtonAddStyles_Click(object sender, RoutedEventArgs e)
        {
            var category = SuperGridTiles.DataGridCategories.SelectedItem as TileCategory;
            foreach (var style in Globals.Globals.SdkTiles.TmpStyleList)
            {
                category.AddStyle(style);
            }

            Globals.Globals.SdkTiles.TmpStyleList.Clear();
            AllGridRefresh();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var media = SuperGridLosedTiles.DataGridCategories as System.Windows.Media.Visual;
            if (media == null)
                return;
            var foundList = Globals.Helpers.FindElementsOfType(media, typeof(DataGrid));
            if (foundList == null)
                return;
            List<DataGrid> dataList = foundList.OfType<DataGrid>().Select(frameworkElement => frameworkElement as DataGrid).ToList();
            foreach (var frameworkElement in dataList)
            {
                var list = frameworkElement.SelectedItems;
                foreach (TileStyle VARIABLE in list.OfType<TileStyle>())
                {
                    Globals.Globals.SdkTiles.TmpStyleList.Add(VARIABLE);
                }
                frameworkElement.SelectedItems.Clear();
            }


            if (dataGridTmpStyles.ItemsSource != Globals.Globals.SdkTiles.TmpStyleList)
                dataGridTmpStyles.ItemsSource = Globals.Globals.SdkTiles.TmpStyleList;
            AllGridRefresh();
        }

        #endregion

        #region Main Window buttons
        
        private void buttonSaveTileData_Click(object sender, RoutedEventArgs e)
        {

            if(!string.IsNullOrEmpty(Globals.Globals.SaveDirLocation))
                try
                {
                    Globals.Globals.SdkTiles.Save(Globals.Globals.SaveDirLocation);
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message.ToString(), "Error in saving file",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                }
            else
            {
                MessageBox.Show("you have to enter a save folder in options menu", "Error Save Directory doesn't exist",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonLoadTileData_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Binary files *.bin|*.bin";
            dlg.DefaultExt = "*.bin";
            var res= dlg.ShowDialog();
            if(res == true)
            if (!string.IsNullOrEmpty(dlg.FileName))
                try
                {
                    Globals.Globals.SdkTiles.Load(dlg.FileName);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message.ToString(CultureInfo.InstalledUICulture), "FileLoading Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                }
            else
            {
                MessageBox.Show("you have to insert a File", "File Loading Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Make Map
        
        #region main Buttons

        private void ButtonLoadScript_Click(object sender, RoutedEventArgs e)
        {

        }
        
        #endregion
        
        #endregion




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

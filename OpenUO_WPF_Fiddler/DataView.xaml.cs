using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using OpenUO.Ultima;
using TilesInfo.Components;
using TilesInfo.Components.Tiles;
using Tile = TilesInfo.Components.Tile;
using Type = TilesInfo.Components.Enums.Type;

namespace OpenUO_WPF_Fiddler
{
    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    
    public partial class DataView : Window
    {

        private readonly List<TilesInfo.Components.Enums.Type> _types; 
        public DataView()
        {
            InitializeComponent();
            MainWindow.SDK.Populate();
            _types = new List<TilesInfo.Components.Enums.Type>();
            for (int i = 0; i < 10; i++)
            {
                _types.Add((TilesInfo.Components.Enums.Type)i);
            }
            ComboEnum.ItemsSource = _types;
            ComboEnum.SelectedItem = _types.First();
            ListData.Items.Clear();
            DataContext = MainWindow.SDK;
            ListData.ItemsSource = MainWindow.SDK.TileData.ItemTable.ToList();
        }

        #region generate buttons

        private void ButtonRoof_Click(object sender, RoutedEventArgs e)
        {
            DataCategory.ItemsSource = MainWindow.SDK.RoofCat;
            DataRefresh();
        }

        private void FloorButton_Click(object sender, RoutedEventArgs e)
        {
            DataCategory.ItemsSource = MainWindow.SDK.FloorsCat;
            DataRefresh();
        }

        private void ButtonMisc_Click(object sender, RoutedEventArgs e)
        {
            DataCategory.ItemsSource = MainWindow.SDK.MiscCat;
            DataRefresh();
        }

        private void ButtonWall_Click(object sender, RoutedEventArgs e)
        {
            DataCategory.ItemsSource = MainWindow.SDK.WallsCat;
            DataRefresh();
        }
        
        #endregion

        #region button add

        private void ButtonAddStyle_Click(object sender, RoutedEventArgs e)
        {
            var category = DataCategory.SelectedItem as TileCategory;
            var styles = DataStyle.ItemsSource as List<TileStyle>;
            var st = new TileStyle();
            category.AddStyle(st);
            DataStyle.Items.Refresh();
        }

        private void ButtonAddTile_Click(object sender, RoutedEventArgs e)
        {
            var category = DataCategory.SelectedItem as TileCategory;
            var id = ListData.SelectedIndex;
            var style = DataStyle.SelectedItem as TileStyle;
            if(style == null)
            {
                style = new TileStyle();
                category.AddStyle(style);
            }
            if (ComboEnum.SelectedItem == null)
                return;
            switch ((TilesInfo.Components.Enums.Type)ComboEnum.SelectedItem)
            {
                case Type.Wall:
                    {
                        var tile = new TileWall();
                        tile.Id = (short)id;
                      style.AddTile(tile);

                        break;
                    }
                case Type.Roofs:
                    {
                        var tile = new TileRoof();
                        tile.Id = (short)id;
                        style.AddTile(tile);
                        break;
                    }
                case Type.Floor:
                    {
                        var tile = new TileFloor();
                        tile.Id = (short)id;
                        style.AddTile(tile);
                        break;
                    }
                case Type.Misc:
                    {
                        var tile = new TileMisc();
                        tile.Id = (short)id;
                        style.AddTile(tile); 
                        break;
                    }
                default:
                    {
                        var tile = new Tile();
                        tile.Id = (short)id;
                        style.AddTile(tile);
                        break;
                    }
            }

            DataRefresh();
        }

        private void ButtonCategory_Click(object sender, RoutedEventArgs e)
        {
            var list = DataCategory.ItemsSource as List<TileCategory>;
            if (list != null) list.Add(new TileCategory());
            else
            {
                list = new List<TileCategory>();
                list.Add(new TileCategory());
                DataCategory.Items.Clear();
                DataCategory.ItemsSource = list;
            }
            

            DataCategory.Items.Refresh();

        }
        
        #endregion

        #region button save and load

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var selectfile = new Microsoft.Win32.SaveFileDialog() { Filter = "Bin Files (.bin)|*.bin|All Files (*.*)|*.*", FilterIndex = 1 };
            selectfile.ShowDialog();
            string _filesave = null;
            if (!string.IsNullOrEmpty(selectfile.FileName))
                _filesave = selectfile.FileName;

            if (string.IsNullOrEmpty(_filesave))
                return;
            
            MainWindow.SDK.Save(_filesave);
            DataRefresh();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var selectfile = new Microsoft.Win32.OpenFileDialog() { Filter = "Bin Files (.bin)|*.bin|All Files (*.*)|*.*", FilterIndex = 1 };
            selectfile.ShowDialog();
            string fileload;
            if (!string.IsNullOrEmpty(selectfile.FileName))
                fileload = selectfile.FileName;
            fileload = selectfile.FileName;

            if (string.IsNullOrEmpty(fileload))
                return;
            MainWindow.SDK.Load(fileload);
            
        }
        
        #endregion

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DataCategory.ItemsSource = MainWindow.SDK.Token;
            DataRefresh();
        }

        private void ButtonStyleList_Click(object sender, RoutedEventArgs e)
        {
            if (DataStyle.SelectedItem == null)
                return;
            MainWindow.SDK.TmpStyleList.Add(DataStyle.SelectedItem as TileStyle);
            DataRefresh();
        }

        private void ButtonImportFromTStyleList_Click(object sender, RoutedEventArgs e)
        {
            var cat = DataCategory.SelectedItem as TileCategory;
            foreach (var st in MainWindow.SDK.TmpStyleList)
            {
                cat.AddStyle(st);
            }
            MainWindow.SDK.TmpStyleList.Clear();
            DataRefresh();
        }

        private void ButtonAddTList_Click(object sender, RoutedEventArgs e)
        {
            if(DataTile.SelectedItem == null)
                return;
            
            MainWindow.SDK.TmpTileList.Add(DataTile.SelectedItem as Tile);
            DataRefresh();

        }

        private void ButtonImportFromTList_Click(object sender, RoutedEventArgs e)
        {
            var style = DataStyle.SelectedItem as TileStyle;
            foreach (var tile in MainWindow.SDK.TmpTileList)
            {
                style.AddTile(tile);
            }
            DataRefresh();
        }

        private void DataRefresh()
        {
            DataTile.Items.Refresh();
            DataStyle.Items.Refresh();
            DataCategory.Items.Refresh();
            DataTileTmp.Items.Refresh();
            DataStyleTmp.Items.Refresh();
        }
    }

}
